# Silver Chest (ChocolateBox)

A modding tool for Fable: The Lost Chapters that allows viewing, editing, and exporting game assets including textures, models, scripts, and more.

## Architecture Overview

### Project Structure

```
ChocolateBox/
├── Core/                   # Core utilities and infrastructure
│   ├── Settings.cs        # Application settings and INI file management
│   ├── Progress.cs        # Progress tracking for long operations
│   ├── ThemeManager.cs    # Dark mode UI theming
│   ├── FileInterface.cs   # File management interface
│   └── ...
├── Controllers/            # MVC Controllers for file types
│   ├── BIGFileController.cs    # Handles .BIG archive files
│   ├── BINFileController.cs    # Handles .BIN database files
│   ├── WLDFileController.cs    # Handles .WLD world/level files
│   └── ...
├── Processors/             # Background task processors
│   ├── ModelExporter.cs   # Exports 3D models to OBJ format
│   ├── FileProcessor.cs   # Generic file processing
│   └── ...
├── Forms/                  # Windows Forms UI
│   ├── FormMain.cs        # Main MDI application window
│   ├── FormBIG.cs         # BIG archive viewer
│   ├── FormEditor.cs      # Level/world editor
│   └── ...
├── Database/              # Game asset databases
│   └── FileDatabase.cs    # Central registry of game files
└── DecompiledDLLs/        # Decompiled FableMod libraries (reference only)
    ├── FableMod.BIG/      # BIG archive format support
    ├── FableMod.Gfx.Integration/  # DirectX graphics integration
    └── ...
```

### Key Concepts

#### 1. File Types

Silver Chest supports various Fable file formats:

- **BIG Files** (`.big`) - Archive files containing multiple assets (textures, models, etc.)
- **BIN Files** (`.bin`) - Database files containing game definitions and data
- **WLD Files** (`.wld`) - World/level files containing 3D scenes
- **TNG Files** (`.tng`) - Thing definition files
- **LEV Files** (`.lev`) - Level metadata
- **STB Files** (`.stb`) - String table files

#### 2. MVC Pattern

The application follows a Model-View-Controller pattern:

- **Models**: Loaded from FableMod DLL libraries (e.g., `BIGFile`, `BINFile`)
- **Views**: Windows Forms (`FormBIG`, `FormBIN`, etc.)
- **Controllers**: Link models to views (`BIGFileController`, `BINFileController`)

Example flow:
```
User opens file → FileDatabase → Controller creates Model → Controller creates View → View displays data
```

#### 3. MDI Architecture

The application uses Multiple Document Interface (MDI):
- `FormMain` is the parent window
- Each opened file creates a child window (`FormBIG`, `FormBIN`, etc.)
- Child windows can be tiled, cascaded, or maximized

#### 4. Progress Reporting

Long operations use the `Progress` class for user feedback:
```csharp
Progress progress = new Progress();
progress.Begin(totalSteps);
progress.Info = "Loading file...";
// ... do work ...
progress.Update();
progress.End();
```

#### 5. Theme System

The `ThemeManager` applies dark mode styling to all controls:
- Automatically applies on form/control creation
- Supports all standard Windows Forms controls
- Custom colors for Fable modding aesthetic

## Development Guide

### Prerequisites

- Visual Studio 2022 or Rider
- .NET Framework 4.8
- Windows 10/11 (for DirectX 9 support)

### Building

1. Open `ChocolateBox.sln` or `ChocolateBox.csproj`
2. Restore NuGet packages (if any)
3. Build solution (Ctrl+Shift+B)
4. Executable will be in `bin\Debug\` or `bin\Release\`

### Configuration

Edit `ChocolateBox.ini` to configure:

```ini
[Settings]
; Path to Fable installation
MyPath=C:\Program Files (x86)\Microsoft Games\Fable - The Lost Chapters\

; Registry key for auto-detection
FableRegistry=SOFTWARE\Microsoft\Microsoft Games\Fable

[ModelExport]
; Default scale for model exports
Scale=1.0
```

### Adding New File Type Support

1. **Create Controller**: Inherit from `FileController`
   ```csharp
   public class MyFileController : FileController
   {
       protected override bool OnForm(Progress progress)
       {
           // Create and show form
       }
   }
   ```

2. **Create Form**: Inherit from `FormFileController`
   ```csharp
   public class FormMyFile : FormFileController
   {
       public void Build(MyFile file, Progress progress)
       {
           // Populate UI with file data
       }
   }
   ```

3. **Register in FileDatabase**: Add file pattern and controller

### Code Style Guidelines

1. **Documentation**: Add XML comments to all public APIs
   ```csharp
   /// <summary>
   /// Exports a 3D model to OBJ format.
   /// </summary>
   /// <param name="model">The model to export</param>
   /// <param name="outputPath">Where to save the OBJ file</param>
   public void ExportModel(Model model, string outputPath)
   ```

2. **Naming Conventions**:
   - Classes: `PascalCase`
   - Methods: `PascalCase`
   - Private fields: `camelCase` or `_camelCase`
   - Properties: `PascalCase`

3. **Error Handling**:
   - Use try-catch for expected failures
   - Let unexpected exceptions bubble up
   - Show user-friendly messages via `MessageBox`

4. **Async Operations**:
   - Use `FormProcess` for long-running tasks
   - Report progress via `Progress` class
   - Run on background thread to keep UI responsive

## Common Tasks

### Exporting a Model

```csharp
ModelExporter exporter = new ModelExporter();
exporter.ExportModel(model, "output.obj", ExportFormat.OBJ);
```

### Loading a BIG File

```csharp
BIGFile big = new BIGFile("textures.big");
foreach (BIGBank bank in big.Banks)
{
    foreach (AssetEntry entry in bank.Entries)
    {
        // Process entry
    }
}
```

### Applying Theme to Custom Form

```csharp
public class MyForm : Form
{
    public MyForm()
    {
        InitializeComponent();
        ThemeManager.ApplyTheme(this); // Apply dark mode
    }
}
```

## Troubleshooting

### Application won't start
- Ensure Fable is installed or manually select directory on first launch
- Check `ChocolateBox.ini` exists and has valid paths

### Models export incorrectly
- Verify coordinate system in `ModelExporter.cs`
- Check scale settings in INI file
- Ensure source model format is supported

### Textures appear pink/magenta
- This indicates transparency - it's expected behavior for texture previews
- Export texture to view in external image editor

### Build errors about missing DLLs
- Ensure `lib\` folder contains all FableMod DLLs
- These are pre-compiled libraries from the original ChocolateBox

## Contributing

When contributing code:

1. Follow existing code style and patterns
2. Add XML documentation comments to public APIs
3. Test with various Fable file types
4. Update this README if adding new features

## Known Limitations

- Decompiled DLL source code in `DecompiledDLLs\` is for reference only - it cannot be recompiled
- Some advanced model features may not export correctly
- Large BIG files (>10,000 entries) may take time to load

## Credits

- Original ChocolateBox by [original authors]
- Refactored and maintained as "Silver Chest"
- Uses FableMod library for Fable file format support
- Built with .NET Framework and Windows Forms

## License

[Add license information here]
