"""
Fable .X Bulk Model Importer for Blender 4.0+
==============================================
Batch import DirectX .X text format models from Fable: The Lost Chapters

Features:
- Imports entire folders of .X files
- Handles both static meshes (buildings, props) and skinned meshes (characters, creatures)
- Automatically detects and creates armatures when bones are present
- Preserves UVs, normals, and material references
- Creates collections per model for organization
- Progress reporting in console

Usage:
1. In Blender, go to Scripting workspace
2. Paste this script
3. Configure the settings at the bottom:
   - Set IMPORT_PATH to a folder or single file
   - Adjust SCALE as needed
4. Press Alt+P to run

Tested on: Blender 4.0, 4.1, 4.2, 4.3, 5.0
"""

import bpy
import bmesh
import re
import os
from mathutils import Matrix, Vector
from pathlib import Path


# ============================================================
# CONFIGURATION
# ============================================================

# Set to True to disable custom normals (fixes Blender 5.0 crash)
APPLY_CUSTOM_NORMALS = False

# Set to True to orient models for Unreal Engine export (X-forward, Z-up)
# Applies -90° X rotation and +90° Z rotation
ORIENT_FOR_UNREAL = True

# Set to True to search subfolders recursively
RECURSIVE_SEARCH = True

# Set to False to skip armature/skeleton import (useful if re-rigging with AutoRigPro)
IMPORT_ARMATURE = False

# Set to True to auto-load textures from same folder as .X files
AUTO_LOAD_TEXTURES = True

# Set to True to convert DDS textures to PNG (for Mixamo compatibility)
CONVERT_DDS_TO_PNG = True

# Set to True to clean up mesh for export (fixes normal issues in Mixamo/UE)
# Removes doubles, clears custom split normals, recalculates normals
CLEANUP_MESH_FOR_EXPORT = True

# Set to True to flip all face normals (try this if model appears inside-out)
FLIP_NORMALS = False

# Set to True to auto-join all mesh segments into one mesh (for multi-part models like Hero)
# Also welds vertices at seams between segments
AUTO_JOIN_MESHES = True


def find_texture_in_folder(texture_name, folder):
    """Find a texture file in the specified folder"""
    if not texture_name or not folder:
        return None
    
    base_name = os.path.splitext(texture_name)[0].lower()
    folder = Path(folder)
    
    # If converting DDS to PNG, always prefer PNG
    if CONVERT_DDS_TO_PNG:
        extensions = ['.png', '.PNG']
        for ext in extensions:
            path = folder / f"{base_name}{ext}"
            if path.exists():
                return str(path)
    
    # Check for various extensions, in order of preference
    extensions = ['.png', '.tga', '.dds', '.jpg', '.jpeg', '.bmp']
    
    for ext in extensions:
        # Try lowercase
        path = folder / f"{base_name}{ext}"
        if path.exists():
            return str(path)
        
        # Try uppercase extension
        path = folder / f"{base_name}{ext.upper()}"
        if path.exists():
            return str(path)
    
    # Also try original filename as-is
    path = folder / texture_name
    if path.exists():
        return str(path)
    
    return None


def load_and_convert_texture(texture_path):
    """Load a texture, converting DDS to PNG if enabled"""
    if not texture_path:
        return None
    
    img_name = os.path.basename(texture_path)
    
    # Handle DDS conversion
    if CONVERT_DDS_TO_PNG and texture_path.lower().endswith('.dds'):
        png_path = os.path.splitext(texture_path)[0] + '.png'
        png_name = os.path.basename(png_path)
        
        # Check if PNG already loaded in Blender
        if png_name in bpy.data.images:
            return bpy.data.images[png_name]
        
        # Check if PNG file already exists on disk
        if os.path.exists(png_path):
            return bpy.data.images.load(png_path)
        
        # Convert DDS to PNG
        try:
            img = bpy.data.images.load(texture_path)
            
            # Create new image with same dimensions
            width, height = img.size
            new_img = bpy.data.images.new(
                name=png_name,
                width=width,
                height=height,
                alpha=True
            )
            
            # Copy pixels
            new_img.pixels[:] = img.pixels[:]
            new_img.filepath_raw = png_path
            new_img.file_format = 'PNG'
            new_img.save()
            
            # Clean up original DDS from Blender
            bpy.data.images.remove(img)
            
            print(f"    Converted: {img_name} -> {png_name}")
            return new_img
        except Exception as e:
            print(f"    Warning: Could not convert {img_name} to PNG: {e}")
            # Fall back to loading DDS directly
            try:
                return bpy.data.images.load(texture_path)
            except:
                return None
    
    # Non-DDS: check if already loaded
    if img_name in bpy.data.images:
        return bpy.data.images[img_name]
    
    # Load directly
    return bpy.data.images.load(texture_path)


# ============================================================
# PARSER
# ============================================================

def parse_fable_x_file(filepath):
    """
    Parse a Fable .X file and extract all mesh and bone data.
    """
    print(f"  Parsing: {os.path.basename(filepath)}")
    
    with open(filepath, 'r', encoding='utf-8', errors='replace') as f:
        content = f.read()
    
    content = content.replace('\r\n', '\n').replace('\r', '\n')
    
    result = {
        'name': os.path.splitext(os.path.basename(filepath))[0],
        'meshes': [],
        'bones': {}
    }
    
    # =========================================
    # PARSE MESHES
    # =========================================
    
    frame_pattern = r'Frame\s+([\w-]+)\s*\{\s*\n?\s*FrameTransformMatrix\s*\{[^}]+\}\s*\n?\s*Mesh\s*\{'
    
    for match in re.finditer(frame_pattern, content, re.DOTALL):
        frame_name = match.group(1)
        
        # Skip template/system frames
        skip_names = ['frame', 'mesh', 'template', 'movement', 'scene-root', 
                      'orphan_helpers', 'hdmy_', 'hpnt_', 'bone_offset_matrix']
        if any(skip in frame_name.lower() for skip in skip_names):
            continue
        
        # Also skip if it's the main container frame (matches the filename pattern)
        if frame_name.startswith('MESH_') and frame_name == result['name']:
            continue
        
        mesh_start = match.end()
        
        # Find mesh block
        depth = 1
        pos = mesh_start
        while pos < len(content) and depth > 0:
            if content[pos] == '{':
                depth += 1
            elif content[pos] == '}':
                depth -= 1
            pos += 1
        
        mesh_block = content[mesh_start:pos-1]
        mesh_data = parse_mesh_block(mesh_block, frame_name)
        
        if mesh_data and mesh_data['vertices']:
            result['meshes'].append(mesh_data)
    
    # =========================================
    # PARSE BONES (only if skinned mesh)
    # =========================================
    
    # Check if there are any SkinWeights
    if 'SkinWeights' in content:
        # Parse bone hierarchy
        bone_frames = {}
        lines = content.split('\n')
        brace_depth = 0
        parent_at_depth = {}
        
        for i, line in enumerate(lines):
            open_braces = line.count('{')
            close_braces = line.count('}')
            
            match = re.search(r'Frame\s+(Bip[\w-]+|Movement|Sub_movement_dummy)\s*\{', line)
            if match:
                bone_name = match.group(1)
                parent = parent_at_depth.get(brace_depth)
                bone_frames[bone_name] = {'depth': brace_depth + 1, 'parent': parent, 'line': i}
                parent_at_depth[brace_depth + 1] = bone_name
            
            brace_depth += open_braces - close_braces
            keys_to_remove = [d for d in parent_at_depth if d > brace_depth]
            for k in keys_to_remove:
                del parent_at_depth[k]
        
        # Get matrices
        bone_pattern = r'Frame\s+(Bip[\w-]+|Movement|Sub_movement_dummy)\s*\{\s*\n?\s*(?:Frame\s+BONE_OFFSET_MATRIX[^}]+\}[^F]*)?FrameTransformMatrix\s*\{\s*([\d\s.,;e+-]+)\s*\}'
        
        for match in re.finditer(bone_pattern, content, re.IGNORECASE | re.DOTALL):
            bone_name = match.group(1)
            matrix_str = match.group(2)
            
            values = re.findall(r'[-+]?\d*\.?\d+(?:e[-+]?\d+)?', matrix_str)
            if len(values) >= 16:
                floats = [float(v) for v in values[:16]]
                matrix = Matrix((
                    (floats[0], floats[4], floats[8], floats[12]),
                    (floats[1], floats[5], floats[9], floats[13]),
                    (floats[2], floats[6], floats[10], floats[14]),
                    (floats[3], floats[7], floats[11], floats[15])
                ))
            else:
                matrix = Matrix.Identity(4)
            
            parent = bone_frames.get(bone_name, {}).get('parent')
            result['bones'][bone_name] = {
                'name': bone_name,
                'matrix': matrix,
                'parent': parent
            }
    
    mesh_count = len(result['meshes'])
    bone_count = len(result['bones'])
    print(f"    Found {mesh_count} mesh(es), {bone_count} bone(s)")
    
    return result


def parse_mesh_block(content, name):
    """Parse the contents of a Mesh { } block"""
    mesh = {
        'name': name,
        'vertices': [],
        'faces': [],
        'normals': [],
        'uvs': [],
        'texture': None,
        'skin_weights': []
    }
    
    # ---- VERTICES ----
    vert_match = re.match(r'\s*(\d+);\s*(.*?);;', content, re.DOTALL)
    if vert_match:
        expected = int(vert_match.group(1))
        vert_data = vert_match.group(2)
        
        for v in re.finditer(r'([-+]?\d*\.?\d+);([-+]?\d*\.?\d+);([-+]?\d*\.?\d+);?', vert_data):
            mesh['vertices'].append((float(v.group(1)), float(v.group(2)), float(v.group(3))))
        
        remaining = content[vert_match.end():]
    else:
        return mesh
    
    # ---- FACES ----
    face_match = re.match(r'\s*(\d+);\s*(.*?);;', remaining, re.DOTALL)
    if face_match:
        face_data = face_match.group(2)
        for f in re.finditer(r'3;(\d+),(\d+),(\d+);?', face_data):
            mesh['faces'].append((int(f.group(1)), int(f.group(2)), int(f.group(3))))
        remaining = remaining[face_match.end():]
    
    # ---- NORMALS ----
    normal_match = re.search(r'MeshNormals\s*\{\s*(\d+);\s*(.*?);;', remaining, re.DOTALL)
    if normal_match:
        normal_data = normal_match.group(2)
        for n in re.finditer(r'([-+]?\d*\.?\d+);([-+]?\d*\.?\d+);([-+]?\d*\.?\d+);?', normal_data):
            mesh['normals'].append((float(n.group(1)), float(n.group(2)), float(n.group(3))))
    
    # ---- UVS ----
    uv_match = re.search(r'MeshTextureCoords\s*\{\s*(\d+);\s*(.*?);;', remaining, re.DOTALL)
    if uv_match:
        uv_data = uv_match.group(2)
        for u in re.finditer(r'([-+]?\d*\.?\d+);([-+]?\d*\.?\d+);?', uv_data):
            mesh['uvs'].append((float(u.group(1)), float(u.group(2))))
    
    # ---- TEXTURE ----
    tex_match = re.search(r'TextureFilename\s*\{\s*"([^"]+)"', remaining)
    if tex_match:
        mesh['texture'] = tex_match.group(1)
    
    # ---- SKIN WEIGHTS ----
    skin_start_pattern = r'SkinWeights\s*\{\s*\n?\s*"([^"]+)";\s*\n?\s*(\d+);'
    
    for skin_match in re.finditer(skin_start_pattern, remaining):
        bone_name = skin_match.group(1)
        count = int(skin_match.group(2))
        
        block_start = skin_match.end()
        block_end = remaining.find('}', block_start)
        if block_end == -1:
            continue
        
        block_content = remaining[block_start:block_end]
        sections = block_content.split(';')
        if len(sections) < 2:
            continue
        
        idx_str = sections[0]
        indices = []
        for x in idx_str.split(','):
            x = x.strip()
            if x.isdigit():
                indices.append(int(x))
        
        wt_str = sections[1]
        weights = []
        for w in wt_str.split(','):
            w = w.strip()
            if w:
                try:
                    weights.append(float(w))
                except ValueError:
                    pass
        
        if indices and weights and len(indices) == count:
            mesh['skin_weights'].append({
                'bone': bone_name,
                'indices': indices,
                'weights': weights[:len(indices)]
            })
    
    return mesh


# ============================================================
# BLENDER CREATION FUNCTIONS
# ============================================================

def create_blender_mesh(mesh_data, scale=0.01, source_folder=None):
    """Create a Blender mesh object from parsed data"""
    name = mesh_data['name']
    verts = mesh_data['vertices']
    faces = mesh_data['faces']
    
    if not verts or not faces:
        return None
    
    mesh = bpy.data.meshes.new(name)
    obj = bpy.data.objects.new(name, mesh)
    bpy.context.collection.objects.link(obj)
    
    # Transform vertices
    blender_verts = []
    for v in verts:
        if ORIENT_FOR_UNREAL:
            # DirectX Y-up -> Unreal (X-forward, Z-up)
            # Equivalent to: Blender conversion + rotate -90° X + rotate +90° Z
            x = -v[1] * scale
            y = v[0] * scale
            z = v[2] * scale
        else:
            # DirectX Y-up -> Blender Z-up
            x = v[0] * scale
            y = -v[2] * scale
            z = v[1] * scale
        blender_verts.append((x, y, z))
    
    mesh.from_pydata(blender_verts, [], faces)
    mesh.update()
    
    # Apply UVs FIRST (before any cleanup that might affect vertex indices)
    if mesh_data['uvs'] and len(mesh_data['uvs']) >= len(verts):
        mesh.uv_layers.new(name='UVMap')
        uv_layer = mesh.uv_layers.active.data
        
        for poly in mesh.polygons:
            for loop_idx in poly.loop_indices:
                vert_idx = mesh.loops[loop_idx].vertex_index
                if vert_idx < len(mesh_data['uvs']):
                    u, v = mesh_data['uvs'][vert_idx]
                    # u = u % 1.0  # REMOVED: Wrapping destroyed tiled textures
                    # v = 1.0 - (v % 1.0) # REMOVED: Wrapping destroyed tiled textures
                    v = 1.0 - v
                    uv_layer[loop_idx].uv = (u, v)
    
    # Mesh cleanup for export compatibility
    if CLEANUP_MESH_FOR_EXPORT:
        bm = bmesh.new()
        bm.from_mesh(mesh)
        
        # Remove loose vertices (but NOT doubles - that destroys UV seams)
        loose_verts = [v for v in bm.verts if not v.link_faces]
        if loose_verts:
            bmesh.ops.delete(bm, geom=loose_verts, context='VERTS')
        
        # Recalculate normals to point outward
        bmesh.ops.recalc_face_normals(bm, faces=bm.faces)
        
        # Flip normals if requested
        if FLIP_NORMALS:
            bmesh.ops.reverse_faces(bm, faces=bm.faces)
        
        bm.to_mesh(mesh)
        bm.free()
        
        # Clear any custom split normals data (causes issues in some exporters)
        try:
            if mesh.has_custom_normals:
                mesh.free_normals_split()
        except:
            pass
        
        # Clear sharp edges for clean export
        for edge in mesh.edges:
            edge.use_edge_sharp = False
        
        # Use smooth shading for better normals
        for poly in mesh.polygons:
            poly.use_smooth = True
        
        mesh.update()
    else:
        # Basic normal recalculation
        bm = bmesh.new()
        bm.from_mesh(mesh)
        bmesh.ops.recalc_face_normals(bm, faces=bm.faces)
        if FLIP_NORMALS:
            bmesh.ops.reverse_faces(bm, faces=bm.faces)
        bm.to_mesh(mesh)
        bm.free()
        mesh.update()
    
    # Custom normals (disabled by default for Blender 5.0, and skipped if cleanup enabled)
    if APPLY_CUSTOM_NORMALS and not CLEANUP_MESH_FOR_EXPORT and mesh_data['normals'] and len(mesh_data['normals']) >= len(verts):
        try:
            if ORIENT_FOR_UNREAL:
                blender_normals = [(-n[1], n[0], n[2]) for n in mesh_data['normals']]
            else:
                blender_normals = [(n[0], -n[2], n[1]) for n in mesh_data['normals']]
            mesh.calc_normals_split()
            loop_normals = []
            for poly in mesh.polygons:
                for loop_idx in poly.loop_indices:
                    vert_idx = mesh.loops[loop_idx].vertex_index
                    if vert_idx < len(blender_normals):
                        loop_normals.append(blender_normals[vert_idx])
                    else:
                        loop_normals.append((0, 0, 1))
            if loop_normals:
                mesh.normals_split_custom_set(loop_normals)
        except Exception as e:
            pass
    
    # Create material
    tex_name = mesh_data.get('texture', '')
    mat_name = tex_name.replace('.dds', '').replace('.tga', '') if tex_name else f"Mat_{name}"
    
    mat = bpy.data.materials.new(name=mat_name)
    try:
        mat.use_nodes = True
    except:
        pass
    
    obj.data.materials.append(mat)
    
    nodes = mat.node_tree.nodes
    links = mat.node_tree.links
    principled = nodes.get('Principled BSDF')
    
    if principled:
        principled.inputs['Base Color'].default_value = (0.6, 0.6, 0.6, 1.0)
        principled.inputs['Roughness'].default_value = 1.0
        
        if tex_name:
            tex_node = nodes.new('ShaderNodeTexImage')
            tex_node.location = (-400, 300)
            
            # Try to find and load the texture from source folder
            texture_path = None
            if AUTO_LOAD_TEXTURES and source_folder:
                texture_path = find_texture_in_folder(tex_name, source_folder)
            
            if texture_path:
                try:
                    # Load texture (with optional DDS to PNG conversion)
                    tex_node.image = load_and_convert_texture(texture_path)
                    
                    # Connect to shader
                    links.new(tex_node.outputs['Color'], principled.inputs['Base Color'])
                    
                    # Handle alpha
                    if 'alpha' in name.lower():
                        links.new(tex_node.outputs['Alpha'], principled.inputs['Alpha'])
                        mat.blend_method = 'BLEND'
                    
                    tex_node.label = tex_name
                except Exception as e:
                    tex_node.label = f"FAILED: {tex_name}"
            else:
                tex_node.label = f"NOT FOUND: {tex_name}"
            
            tex_node.name = f"TEX_{tex_name}"
            
            if 'alpha' in name.lower():
                mat.blend_method = 'BLEND'
    
    return obj


def create_blender_armature(bones_data, scale=0.01):
    """Create a Blender armature from parsed bone data"""
    if not bones_data:
        return None
    
    armature = bpy.data.armatures.new('Skeleton')
    arm_obj = bpy.data.objects.new('Skeleton', armature)
    bpy.context.collection.objects.link(arm_obj)
    
    # Compute world matrices
    world_matrices = {}
    
    def get_world_matrix(bone_name):
        if bone_name in world_matrices:
            return world_matrices[bone_name]
        if bone_name not in bones_data:
            return Matrix.Identity(4)
        
        bone = bones_data[bone_name]
        local_mat = bone['matrix']
        parent_name = bone.get('parent')
        
        if parent_name and parent_name in bones_data:
            parent_world = get_world_matrix(parent_name)
            world_mat = parent_world @ local_mat
        else:
            world_mat = local_mat
        
        world_matrices[bone_name] = world_mat
        return world_mat
    
    for bone_name in bones_data:
        get_world_matrix(bone_name)
    
    # Create bones
    bpy.context.view_layer.objects.active = arm_obj
    bpy.ops.object.mode_set(mode='EDIT')
    
    edit_bones = {}
    
    for bone_name, bone_data in bones_data.items():
        edit_bone = armature.edit_bones.new(bone_name)
        
        world_mat = world_matrices.get(bone_name, Matrix.Identity(4))
        pos = world_mat.to_translation() * scale
        
        if ORIENT_FOR_UNREAL:
            # DirectX Y-up -> Unreal orientation
            head = Vector((-pos.y, pos.x, pos.z))
        else:
            # DirectX Y-up -> Blender Z-up
            head = Vector((pos.x, -pos.z, pos.y))
        edit_bone.head = head
        
        bone_dir = world_mat.to_3x3() @ Vector((0, 1, 0))
        if ORIENT_FOR_UNREAL:
            bone_dir_transformed = Vector((-bone_dir.y, bone_dir.x, bone_dir.z)).normalized()
        else:
            bone_dir_transformed = Vector((bone_dir.x, -bone_dir.z, bone_dir.y)).normalized()
        edit_bone.tail = head + bone_dir_transformed * 2.0 * scale
        
        edit_bones[bone_name] = edit_bone
    
    # Parent bones
    for bone_name, bone_data in bones_data.items():
        parent = bone_data.get('parent')
        if parent and parent in edit_bones:
            edit_bones[bone_name].parent = edit_bones[parent]
    
    bpy.ops.object.mode_set(mode='OBJECT')
    
    return arm_obj


def apply_skin_weights(mesh_obj, mesh_data, armature_obj):
    """Apply skin weights to mesh"""
    if not mesh_data.get('skin_weights') or not armature_obj:
        return
    
    mesh_obj.parent = armature_obj
    
    mod = mesh_obj.modifiers.new(name='Armature', type='ARMATURE')
    mod.object = armature_obj
    
    for skin in mesh_data['skin_weights']:
        bone_name = skin['bone']
        indices = skin['indices']
        weights = skin['weights']
        
        vg = mesh_obj.vertex_groups.new(name=bone_name)
        
        for idx, wt in zip(indices, weights):
            if idx < len(mesh_obj.data.vertices) and wt > 0.0:
                vg.add([idx], wt, 'ADD')


# ============================================================
# MAIN IMPORT FUNCTIONS
# ============================================================

def import_single_file(filepath, scale=0.01, create_collection=True):
    """Import a single .X file"""
    if not os.path.exists(filepath):
        print(f"  ERROR: File not found: {filepath}")
        return None
    
    # Parse
    data = parse_fable_x_file(filepath)
    
    if not data['meshes']:
        print(f"  WARNING: No meshes found in {filepath}")
        return None
    
    # Create collection
    if create_collection:
        collection = bpy.data.collections.new(data['name'])
        bpy.context.scene.collection.children.link(collection)
        layer_col = bpy.context.view_layer.layer_collection.children.get(data['name'])
        if layer_col:
            bpy.context.view_layer.active_layer_collection = layer_col
    
    # Create armature if bones exist and armature import is enabled
    armature = None
    if IMPORT_ARMATURE and data['bones']:
        armature = create_blender_armature(data['bones'], scale)
    
    # Get source folder for texture lookup
    source_folder = os.path.dirname(filepath)
    
    # Create meshes
    mesh_objects = []
    for mesh_data in data['meshes']:
        obj = create_blender_mesh(mesh_data, scale, source_folder)
        if obj:
            mesh_objects.append((obj, mesh_data))
    
    # Apply skin weights only if armature was created
    if armature:
        for obj, mesh_data in mesh_objects:
            apply_skin_weights(obj, mesh_data, armature)
    
    # Auto-join meshes if enabled and there are multiple meshes
    final_meshes = [obj for obj, _ in mesh_objects]
    if AUTO_JOIN_MESHES and len(final_meshes) > 1:
        # Deselect all
        bpy.ops.object.select_all(action='DESELECT')
        
        # Select all mesh objects
        for obj in final_meshes:
            obj.select_set(True)
        
        # Set the first mesh as active
        bpy.context.view_layer.objects.active = final_meshes[0]
        
        # Join all selected meshes
        bpy.ops.object.join()
        
        # Get the joined mesh
        joined_obj = bpy.context.active_object
        joined_obj.name = data['name']
        
        # Weld vertices at seams (merge by distance)
        bpy.ops.object.mode_set(mode='EDIT')
        bpy.ops.mesh.select_all(action='SELECT')
        bpy.ops.mesh.remove_doubles(threshold=0.001)  # Small threshold to only merge seam verts
        bpy.ops.mesh.normals_make_consistent(inside=False)
        bpy.ops.object.mode_set(mode='OBJECT')
        
        # Apply smooth shading
        for poly in joined_obj.data.polygons:
            poly.use_smooth = True
        
        joined_obj.data.update()
        
        final_meshes = [joined_obj]
        print(f"    Joined {len(mesh_objects)} meshes, welded seams")
    
    return {
        'name': data['name'],
        'meshes': final_meshes,
        'armature': armature
    }


def import_fable_x_bulk(path, scale=0.01, file_filter="*.x"):
    """
    Bulk import Fable .X files
    
    Args:
        path: Either a folder path or a single file path
        scale: Scale factor (0.01 recommended for Fable)
        file_filter: Glob pattern for files (default "*.x")
    
    Returns:
        List of import results
    """
    print("\n" + "=" * 70)
    print("FABLE .X BULK IMPORTER")
    print("=" * 70)
    
    path = Path(path)
    
    # Determine files to import
    if path.is_file():
        files = [path]
    elif path.is_dir():
        files = []
        if RECURSIVE_SEARCH:
            # Walk through all subdirectories
            for root, dirs, filenames in os.walk(path):
                for filename in filenames:
                    if filename.lower().endswith('.x'):
                        files.append(Path(root) / filename)
        else:
            # Single directory only
            for filename in os.listdir(path):
                if filename.lower().endswith('.x'):
                    files.append(path / filename)
        files = sorted(files)
    else:
        print(f"ERROR: Path not found: {path}")
        return []
    
    if not files:
        print(f"No .X files found in: {path}")
        if not RECURSIVE_SEARCH:
            print("  (Tip: Set RECURSIVE_SEARCH = True to search subfolders)")
        return []
    
    search_mode = "recursive" if RECURSIVE_SEARCH else "single folder"
    print(f"Found {len(files)} file(s) to import ({search_mode} search)\n")
    
    results = []
    successful = 0
    failed = 0
    
    for i, filepath in enumerate(files, 1):
        print(f"[{i}/{len(files)}] {filepath.name}")
        
        try:
            result = import_single_file(str(filepath), scale, create_collection=True)
            if result:
                results.append(result)
                mesh_count = len(result['meshes'])
                if not IMPORT_ARMATURE:
                    arm_status = "skipped"
                else:
                    arm_status = "yes" if result['armature'] else "no"
                print(f"    ✓ Imported: {mesh_count} mesh(es), armature: {arm_status}")
                successful += 1
            else:
                failed += 1
        except Exception as e:
            print(f"    ✗ FAILED: {e}")
            failed += 1
    
    # Summary
    print("\n" + "=" * 70)
    print(f"IMPORT COMPLETE")
    print(f"  Successful: {successful}")
    print(f"  Failed: {failed}")
    print(f"  Total meshes created: {sum(len(r['meshes']) for r in results)}")
    print(f"  Auto-load textures: {'enabled' if AUTO_LOAD_TEXTURES else 'disabled'}")
    print("=" * 70 + "\n")
    
    return results


# ============================================================
# OPTIONAL: BLENDER OPERATOR FOR UI
# ============================================================

class IMPORT_OT_fable_x_bulk(bpy.types.Operator):
    """Import Fable .X model files"""
    bl_idname = "import_scene.fable_x_bulk"
    bl_label = "Import Fable .X"
    bl_options = {'REGISTER', 'UNDO'}
    
    directory: bpy.props.StringProperty(subtype='DIR_PATH')
    files: bpy.props.CollectionProperty(type=bpy.types.OperatorFileListElement)
    
    scale: bpy.props.FloatProperty(
        name="Scale",
        default=0.01,
        min=0.0001,
        max=10.0,
        description="Scale factor for imported models"
    )
    
    def execute(self, context):
        if self.files:
            # Multiple files selected
            for f in self.files:
                filepath = os.path.join(self.directory, f.name)
                import_single_file(filepath, self.scale)
        else:
            # Directory import
            import_fable_x_bulk(self.directory, self.scale)
        
        return {'FINISHED'}
    
    def invoke(self, context, event):
        context.window_manager.fileselect_add(self)
        return {'RUNNING_MODAL'}


def menu_func_import(self, context):
    self.layout.operator(IMPORT_OT_fable_x_bulk.bl_idname, text="Fable .X (.x)")


def register():
    bpy.utils.register_class(IMPORT_OT_fable_x_bulk)
    bpy.types.TOPBAR_MT_file_import.append(menu_func_import)


def unregister():
    bpy.utils.unregister_class(IMPORT_OT_fable_x_bulk)
    bpy.types.TOPBAR_MT_file_import.remove(menu_func_import)


# ============================================================
# RUN THE IMPORT
# ============================================================

if __name__ == "__main__":
    
    # ========================================
    # CONFIGURATION - Edit these settings
    # ========================================
    
    # Set to a FOLDER to import all .X files in that folder
    # Or set to a single FILE to import just that file
    IMPORT_PATH = r"D:\Documents\Fable\TLC\BanditKing"
    
    # Scale factor - Fable models are large, 0.01 works well
    SCALE = 0.01
    
    # File filter (for folder imports)
    FILE_FILTER = "*.x"
    
    # ========================================
    # Run the import
    # ========================================
    
    # Optional: Register the operator for File > Import menu
    # register()
    
    # Run bulk import
    results = import_fable_x_bulk(IMPORT_PATH, SCALE, FILE_FILTER)
    
    # Select all imported objects
    bpy.ops.object.select_all(action='DESELECT')
    for result in results:
        for mesh in result['meshes']:
            mesh.select_set(True)
        if result['armature']:
            result['armature'].select_set(True)
