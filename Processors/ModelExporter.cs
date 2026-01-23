using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using FableMod.Gfx.Integration;

namespace ChocolateBox
{
    public enum ExportFormat
    {
        X,
        OBJ,
        GLTF
    }

    public static class ModelExporter
    {
        private struct PrimitiveInfo
        {
            public int indexStart;
            public int indexCount;
            public string textureName;
        }

        private const float DefaultScale = 0.01f;

        public static void Export(GfxModelLOD lod, string directory, string fileName, ExportFormat format)
        {
            string xPath = Path.Combine(directory, fileName + ".X");
            
            // Always export to .X first as a base or if requested
            lod.ExportX(xPath);

            if (format == ExportFormat.X)
                return;

            try
            {
                if (format == ExportFormat.OBJ)
                {
                    ConvertToObj(xPath, Path.Combine(directory, fileName + ".obj"));
                }
                else if (format == ExportFormat.GLTF)
                {
                    ConvertToGltf(xPath, Path.Combine(directory, fileName + ".gltf"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DEBUG_LOG] Error converting model: " + ex.Message);
            }
        }

        private class XFileTokenReader
        {
            private string[] tokens;
            private int index = 0;

            public XFileTokenReader(string content)
            {
                // Split by whitespace and common separators used in .X files
                tokens = content.Split(new[] { ' ', '\t', '\n', '\r', ',', ';', '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            }

            public string Next() => index < tokens.Length ? tokens[index++] : null;
            public bool HasNext() => index < tokens.Length;

            public bool SkipTo(string target)
            {
                while (HasNext())
                {
                    if (Next() == target) return true;
                }
                return false;
            }

            public bool TryParseFloat(out float result)
            {
                string t = Next();
                return float.TryParse(t, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result);
            }

            public bool TryParseInt(out int result)
            {
                string t = Next();
                return int.TryParse(t, out result);
            }
        }

        private static float WrapUV(float v)
        {
            return v - (float)Math.Floor(v);
        }

        private static void ConvertToObj(string xPath, string objPath)
        {
            if (!File.Exists(xPath)) return;

            string mtlPath = Path.ChangeExtension(objPath, ".mtl");
            StringBuilder objContent = new StringBuilder();
            StringBuilder mtlContent = new StringBuilder();
            
            objContent.AppendLine($"mtllib {Path.GetFileName(mtlPath)}");
            objContent.AppendLine("# Exported from ChocolateBox");
            
            string content = File.ReadAllText(xPath);
            int vertexOffset = 0;
            int uvOffset = 0;
            HashSet<string> definedMaterials = new HashSet<string>();

            int searchPos = 0;
            int meshCount = 0;
            while (true)
            {
                int meshIdx = content.IndexOf("Mesh ", searchPos);
                if (meshIdx == -1)
                {
                    meshIdx = content.IndexOf("Mesh\r\n", searchPos);
                    if (meshIdx == -1)
                    {
                        meshIdx = content.IndexOf("Mesh\n", searchPos);
                        if (meshIdx == -1) break;
                    }
                }

                // Check if it's "template Mesh"
                if (meshIdx >= 9 && content.Substring(meshIdx - 9, 9) == "template ")
                {
                    searchPos = meshIdx + 5;
                    continue;
                }

                int blockStart = content.IndexOf("{", meshIdx);
                if (blockStart == -1) break;
                
                int depth = 1;
                int blockEnd = -1;
                for (int i = blockStart + 1; i < content.Length; i++)
                {
                    if (content[i] == '{') depth++;
                    else if (content[i] == '}')
                    {
                        depth--;
                        if (depth == 0)
                        {
                            blockEnd = i;
                            break;
                        }
                    }
                }

                if (blockEnd == -1) break;
                searchPos = blockEnd + 1;

                string meshBlock = content.Substring(blockStart + 1, blockEnd - blockStart - 1);
                XFileTokenReader reader = new XFileTokenReader(meshBlock);

                if (reader.TryParseInt(out int vertexCount))
                {
                    meshCount++;
                    string currentTexture = null;
                    int matListIdx = meshBlock.IndexOf("MeshMaterialList");
                    if (matListIdx != -1)
                    {
                        int texFileIdx = meshBlock.IndexOf("TextureFilename", matListIdx);
                        if (texFileIdx != -1)
                        {
                            int quoteStart = meshBlock.IndexOf("\"", texFileIdx);
                            int quoteEnd = meshBlock.IndexOf("\"", quoteStart + 1);
                            if (quoteStart != -1 && quoteEnd != -1)
                            {
                                currentTexture = meshBlock.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
                                currentTexture = Path.GetFileName(currentTexture);
                                if (currentTexture.ToLower().EndsWith(".tga") || currentTexture.ToLower().EndsWith(".bmp") || currentTexture.ToLower().EndsWith(".jpg") || currentTexture.ToLower().EndsWith(".png"))
                                    currentTexture = Path.ChangeExtension(currentTexture, ".dds");
                                else if (!currentTexture.ToLower().EndsWith(".dds"))
                                    currentTexture += ".dds";
                            }
                        }
                    }

                    string matName = !string.IsNullOrEmpty(currentTexture) ? Path.GetFileNameWithoutExtension(currentTexture) : $"Material_{meshCount}";
                    if (!string.IsNullOrEmpty(currentTexture) && !definedMaterials.Contains(matName))
                    {
                        mtlContent.AppendLine($"newmtl {matName}");
                        mtlContent.AppendLine("Ka 1.000 1.000 1.000");
                        mtlContent.AppendLine("Kd 1.000 1.000 1.000");
                        mtlContent.AppendLine("Ks 0.000 0.000 0.000");
                        mtlContent.AppendLine($"map_Kd {currentTexture}");
                        mtlContent.AppendLine();
                        definedMaterials.Add(matName);
                    }

                    objContent.AppendLine($"g Mesh_{meshCount}");
                    objContent.AppendLine($"usemtl {matName}");

                    for (int j = 0; j < vertexCount; j++)
                    {
                        if (reader.TryParseFloat(out float rawX) && 
                            reader.TryParseFloat(out float rawY) && 
                            reader.TryParseFloat(out float rawZ))
                        {
                            float x = -rawY * DefaultScale;
                            float y = rawX * DefaultScale;
                            float z = rawZ * DefaultScale;
                            objContent.AppendLine($"v {x.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)} {y.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)} {z.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)}");
                        }
                    }

                    int uvCountRead = 0;
                    int uvIdx = meshBlock.IndexOf("MeshTextureCoords");
                    if (uvIdx != -1)
                    {
                        int uvBlockStart = meshBlock.IndexOf("{", uvIdx);
                        if (uvBlockStart != -1)
                        {
                            XFileTokenReader uvReader = new XFileTokenReader(meshBlock.Substring(uvBlockStart + 1));
                            if (uvReader.TryParseInt(out int nUvs))
                            {
                                for (int j = 0; j < vertexCount; j++)
                                {
                                    if (j < nUvs && uvReader.TryParseFloat(out float u) && uvReader.TryParseFloat(out float v))
                                    {
                                        float uFinal = u;
                                        float vFinal = 1.0f - v;
                                        objContent.AppendLine($"vt {uFinal.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)} {vFinal.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)}");
                                        uvCountRead++;
                                    }
                                    else if (j >= nUvs)
                                    {
                                        objContent.AppendLine("vt 0.000000 0.000000");
                                        uvCountRead++;
                                    }
                                }
                            }
                        }
                    }

                    if (reader.TryParseInt(out int faceCount))
                    {
                        for (int j = 0; j < faceCount; j++)
                        {
                            if (reader.TryParseInt(out int nIndices))
                            {
                                List<int> indices = new List<int>();
                                for (int k = 0; k < nIndices; k++)
                                {
                                    if (reader.TryParseInt(out int idx))
                                        indices.Add(idx);
                                }
                                
                                if (indices.Count >= 3)
                                {
                                    objContent.Append("f");
                                    foreach (int idx in indices)
                                    {
                                        int vIdx = idx + 1 + vertexOffset;
                                        if (uvCountRead > 0)
                                        {
                                            int vtIdx = idx + 1 + uvOffset;
                                            objContent.Append($" {vIdx}/{vtIdx}");
                                        }
                                        else
                                        {
                                            objContent.Append($" {vIdx}");
                                        }
                                    }
                                    objContent.AppendLine();
                                }
                            }
                        }
                    }
                    vertexOffset += vertexCount;
                    uvOffset += uvCountRead;
                }
            }

            File.WriteAllText(objPath, objContent.ToString());
            if (mtlContent.Length > 0)
                File.WriteAllText(mtlPath, mtlContent.ToString());
        }

        private static void ConvertToGltf(string xPath, string gltfPath)
        {
            if (!File.Exists(xPath)) return;

            List<float> positions = new List<float>();
            List<float> uvs = new List<float>();
            List<int> indices = new List<int>();
            List<PrimitiveInfo> primitives = new List<PrimitiveInfo>();
            List<string> textures = new List<string>();

            string content = File.ReadAllText(xPath);
            int vertexOffset = 0;

            int searchPos = 0;
            while (true)
            {
                int meshIdx = content.IndexOf("Mesh ", searchPos);
                if (meshIdx == -1)
                {
                    meshIdx = content.IndexOf("Mesh\r\n", searchPos);
                    if (meshIdx == -1)
                    {
                        meshIdx = content.IndexOf("Mesh\n", searchPos);
                        if (meshIdx == -1) break;
                    }
                }

                // Check if it's "template Mesh"
                if (meshIdx >= 9 && content.Substring(meshIdx - 9, 9) == "template ")
                {
                    searchPos = meshIdx + 5;
                    continue;
                }

                int blockStart = content.IndexOf("{", meshIdx);
                if (blockStart == -1) break;
                
                int depth = 1;
                int blockEnd = -1;
                for (int i = blockStart + 1; i < content.Length; i++)
                {
                    if (content[i] == '{') depth++;
                    else if (content[i] == '}')
                    {
                        depth--;
                        if (depth == 0)
                        {
                            blockEnd = i;
                            break;
                        }
                    }
                }

                if (blockEnd == -1) break;
                searchPos = blockEnd + 1;

                string meshBlock = content.Substring(blockStart + 1, blockEnd - blockStart - 1);
                XFileTokenReader reader = new XFileTokenReader(meshBlock);

                if (reader.TryParseInt(out int vertexCount))
                {
                    for (int j = 0; j < vertexCount; j++)
                    {
                        if (reader.TryParseFloat(out float rawX) && 
                            reader.TryParseFloat(out float rawY) && 
                            reader.TryParseFloat(out float rawZ))
                        {
                            positions.Add(-rawY * DefaultScale);
                            positions.Add(rawX * DefaultScale);
                            positions.Add(rawZ * DefaultScale);
                        }
                    }

                    int uvIdx = meshBlock.IndexOf("MeshTextureCoords");
                    if (uvIdx != -1)
                    {
                        int uvBlockStart = meshBlock.IndexOf("{", uvIdx);
                        if (uvBlockStart != -1)
                        {
                            XFileTokenReader uvReader = new XFileTokenReader(meshBlock.Substring(uvBlockStart + 1));
                            if (uvReader.TryParseInt(out int nUvs))
                            {
                                for (int j = 0; j < vertexCount; j++)
                                {
                                    if (j < nUvs && uvReader.TryParseFloat(out float u) && uvReader.TryParseFloat(out float v))
                                    {
                                        uvs.Add(u);
                                        uvs.Add(1.0f - v);
                                    }
                                    else
                                    {
                                        uvs.Add(0); uvs.Add(0);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < vertexCount; j++)
                        {
                            uvs.Add(0); uvs.Add(0);
                        }
                    }

                    string meshTextureName = null;
                    int matListIdx = meshBlock.IndexOf("MeshMaterialList");
                    if (matListIdx != -1)
                    {
                        int texFileIdx = meshBlock.IndexOf("TextureFilename", matListIdx);
                        if (texFileIdx != -1)
                        {
                            int quoteStart = meshBlock.IndexOf("\"", texFileIdx);
                            int quoteEnd = meshBlock.IndexOf("\"", quoteStart + 1);
                            if (quoteStart != -1 && quoteEnd != -1)
                            {
                                meshTextureName = meshBlock.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
                                meshTextureName = Path.GetFileName(meshTextureName);
                                if (meshTextureName.ToLower().EndsWith(".tga") || meshTextureName.ToLower().EndsWith(".bmp") || meshTextureName.ToLower().EndsWith(".jpg") || meshTextureName.ToLower().EndsWith(".png"))
                                    meshTextureName = Path.ChangeExtension(meshTextureName, ".dds");
                                else if (!meshTextureName.ToLower().EndsWith(".dds"))
                                    meshTextureName += ".dds";

                                if (!textures.Contains(meshTextureName))
                                    textures.Add(meshTextureName);
                            }
                        }
                    }

                    int primitiveIndexStart = indices.Count;
                    if (reader.TryParseInt(out int faceCount))
                    {
                        for (int j = 0; j < faceCount; j++)
                        {
                            if (reader.TryParseInt(out int nIndices))
                            {
                                List<int> faceIndices = new List<int>();
                                for (int k = 0; k < nIndices; k++)
                                {
                                    if (reader.TryParseInt(out int idx))
                                        faceIndices.Add(idx + vertexOffset);
                                }

                                if (faceIndices.Count >= 3)
                                {
                                    for (int k = 1; k < faceIndices.Count - 1; k++)
                                    {
                                        // Winding flip for glTF (CCW)
                                        indices.Add(faceIndices[0]);
                                        indices.Add(faceIndices[k + 1]);
                                        indices.Add(faceIndices[k]);
                                    }
                                }
                            }
                        }
                    }

                    primitives.Add(new PrimitiveInfo { 
                        indexStart = primitiveIndexStart, 
                        indexCount = indices.Count - primitiveIndexStart,
                        textureName = meshTextureName
                    });

                    vertexOffset += vertexCount;
                }
            }

            if (positions.Count == 0)
            {
                Console.WriteLine("[DEBUG_LOG] No geometry found to export to glTF for {0}", xPath);
                return;
            }

            try
            {
                // Calculate bounding box
                float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
                float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;
                for (int i = 0; i < positions.Count; i += 3)
                {
                    float x = positions[i];
                    float y = positions[i + 1];
                    float z = positions[i + 2];
                    minX = Math.Min(minX, x); minY = Math.Min(minY, y); minZ = Math.Min(minZ, z);
                    maxX = Math.Max(maxX, x); maxY = Math.Max(maxY, y); maxZ = Math.Max(maxZ, z);
                }

                // Create Binary Buffers
                byte[] posBytes = new byte[positions.Count * 4];
                for (int i = 0; i < positions.Count; i++)
                {
                    byte[] bytes = BitConverter.GetBytes(positions[i]);
                    if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
                    Buffer.BlockCopy(bytes, 0, posBytes, i * 4, 4);
                }

                byte[] uvBytes = new byte[uvs.Count * 4];
                for (int i = 0; i < uvs.Count; i++)
                {
                    byte[] bytes = BitConverter.GetBytes(uvs[i]);
                    if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
                    Buffer.BlockCopy(bytes, 0, uvBytes, i * 4, 4);
                }

                byte[] idxBytes = new byte[indices.Count * 4];
                for (int i = 0; i < indices.Count; i++)
                {
                    byte[] bytes = BitConverter.GetBytes(indices[i]);
                    if (!BitConverter.IsLittleEndian) Array.Reverse(bytes);
                    Buffer.BlockCopy(bytes, 0, idxBytes, i * 4, 4);
                }

                string posBase64 = Convert.ToBase64String(posBytes);
                string uvBase64 = Convert.ToBase64String(uvBytes);
                string idxBase64 = Convert.ToBase64String(idxBytes);

                int posByteLength = posBytes.Length;
                int uvByteLength = uvBytes.Length;
                int idxByteLength = idxBytes.Length;

                bool hasUVs = uvs.Count > 0;
                int firstPrimAccessorIdx = hasUVs ? 3 : 2;

                // Build glTF JSON
                StringBuilder gltf = new StringBuilder();
                gltf.AppendLine("{");
                gltf.AppendLine("  \"asset\": { \"version\": \"2.0\", \"generator\": \"ChocolateBox\" },");
                gltf.AppendLine("  \"scenes\": [ { \"nodes\": [ 0 ] } ],");
                gltf.AppendLine("  \"nodes\": [ { \"mesh\": 0 } ],");
                gltf.AppendLine("  \"meshes\": [");
                gltf.AppendLine("    {");
                gltf.AppendLine("      \"primitives\": [");
                for (int i = 0; i < primitives.Count; i++)
                {
                    var prim = primitives[i];
                    gltf.AppendLine("        {");
                    gltf.Append("          \"attributes\": { \"POSITION\": 1");
                    if (hasUVs) gltf.Append(", \"TEXCOORD_0\": 2");
                    gltf.AppendLine(" },");
                    int matIdx = textures.IndexOf(prim.textureName);
                    gltf.AppendLine($"          \"indices\": {firstPrimAccessorIdx + i}" + (matIdx == -1 ? "" : $", \"material\": {matIdx}"));
                    gltf.Append("        }" + (i < primitives.Count - 1 ? "," : "") + "\n");
                }
                gltf.AppendLine("      ]");
                gltf.AppendLine("    }");
                gltf.AppendLine("  ],");

                if (textures.Count > 0)
                {
                    gltf.AppendLine("  \"materials\": [");
                    for (int i = 0; i < textures.Count; i++)
                    {
                        gltf.AppendLine("    { \"pbrMetallicRoughness\": { \"baseColorTexture\": { \"index\": " + i + " }, \"metallicFactor\": 0.0, \"roughnessFactor\": 1.0 }, \"doubleSided\": true }" + (i < textures.Count - 1 ? "," : ""));
                    }
                    gltf.AppendLine("  ],");
                    
                    gltf.AppendLine("  \"textures\": [");
                    for (int i = 0; i < textures.Count; i++)
                    {
                        gltf.AppendLine("    { \"sampler\": 0, \"source\": " + i + " }" + (i < textures.Count - 1 ? "," : ""));
                    }
                    gltf.AppendLine("  ],");
                    
                    gltf.AppendLine("  \"samplers\": [");
                    gltf.AppendLine("    { \"magFilter\": 9729, \"minFilter\": 9987, \"wrapS\": 10497, \"wrapT\": 10497 }");
                    gltf.AppendLine("  ],");
                    
                    gltf.AppendLine("  \"images\": [");
                    for (int i = 0; i < textures.Count; i++)
                    {
                        gltf.AppendLine("    { \"uri\": \"" + textures[i] + "\" }" + (i < textures.Count - 1 ? "," : ""));
                    }
                    gltf.AppendLine("  ],");
                }

                gltf.AppendLine("  \"accessors\": [");
                gltf.AppendLine("    {"); // Legacy Indices (0)
                gltf.AppendLine("      \"bufferView\": 0,");
                gltf.AppendLine("      \"componentType\": 5125,"); // UNSIGNED_INT
                gltf.AppendLine($"      \"count\": {indices.Count},");
                gltf.AppendLine("      \"type\": \"SCALAR\"");
                gltf.AppendLine("    },");
                gltf.AppendLine("    {"); // Positions (1)
                gltf.AppendLine("      \"bufferView\": 1,");
                gltf.AppendLine("      \"componentType\": 5126,"); // FLOAT
                gltf.AppendLine($"      \"count\": {positions.Count / 3},");
                gltf.AppendLine("      \"type\": \"VEC3\",");
                gltf.AppendLine($"      \"min\": [ {minX.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {minY.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {minZ.ToString(System.Globalization.CultureInfo.InvariantCulture)} ],");
                gltf.AppendLine($"      \"max\": [ {maxX.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {maxY.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {maxZ.ToString(System.Globalization.CultureInfo.InvariantCulture)} ]");
                gltf.AppendLine("    }");
                if (hasUVs)
                {
                    gltf.AppendLine("    , {"); // UVs (2)
                    gltf.AppendLine("      \"bufferView\": 2,");
                    gltf.AppendLine("      \"componentType\": 5126,"); // FLOAT
                    gltf.AppendLine($"      \"count\": {uvs.Count / 2},");
                    gltf.AppendLine("      \"type\": \"VEC2\"");
                    gltf.AppendLine("    }");
                }
                
                // Accessors for each primitive's indices
                for (int i = 0; i < primitives.Count; i++)
                {
                    var prim = primitives[i];
                    gltf.AppendLine("    , {");
                    gltf.AppendLine("      \"bufferView\": 0,");
                    gltf.AppendLine($"      \"byteOffset\": {prim.indexStart * 4},");
                    gltf.AppendLine("      \"componentType\": 5125,"); // UNSIGNED_INT
                    gltf.AppendLine($"      \"count\": {prim.indexCount},");
                    gltf.AppendLine("      \"type\": \"SCALAR\"");
                    gltf.AppendLine("    }");
                }
                
                gltf.AppendLine("  ],");
                gltf.AppendLine("  \"bufferViews\": [");
                gltf.AppendLine("    { \"buffer\": 0, \"byteLength\": " + idxByteLength + ", \"target\": 34963 },");
                gltf.AppendLine("    { \"buffer\": 1, \"byteLength\": " + posByteLength + ", \"target\": 34962 }");
                if (hasUVs) gltf.AppendLine("    , { \"buffer\": 2, \"byteLength\": " + uvByteLength + ", \"target\": 34962 }");
                gltf.AppendLine("  ],");
                gltf.AppendLine("  \"buffers\": [");
                gltf.AppendLine("    { \"byteLength\": " + idxByteLength + ", \"uri\": \"data:application/octet-stream;base64," + idxBase64 + "\" },");
                gltf.AppendLine("    { \"byteLength\": " + posByteLength + ", \"uri\": \"data:application/octet-stream;base64," + posBase64 + "\" }");
                if (hasUVs) gltf.AppendLine("    , { \"byteLength\": " + uvByteLength + ", \"uri\": \"data:application/octet-stream;base64," + uvBase64 + "\" }");
                gltf.AppendLine("  ]");
                gltf.AppendLine("}");

                File.WriteAllText(gltfPath, gltf.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DEBUG_LOG] Error writing glTF file {0}: {1}", gltfPath, ex.Message);
            }
        }
    }
}
