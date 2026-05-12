# Task Manager CLI

A cross-platform terminal-based task manager application built with .NET 10 and C#. This application provides a text-based user interface for monitoring and managing system processes directly from the terminal.

## Prerequisites

- .NET 10 SDK (or higher)
- Unix-like operating system (Linux, macOS, BSD)
- Terminal with ANSI escape sequence support

### Windows Users

This application is developed for Unix-like systems and uses Unix-specific system APIs. Windows users should use **Windows Subsystem for Linux (WSL)** to run this application. Install WSL via PowerShell or the Microsoft Store, then proceed with the Linux build instructions below.

## Building the Project

### Using Nix (Recommended)

This project includes a Nix flake for reproducible builds. If you have Nix installed:

```bash
# Enter the development shell with .NET SDK
nix develop

# Build the project
nix build
```

The compiled binary will be available in the `result` directory.

### Using .NET CLI Directly

If you have .NET 10 SDK installed system-wide:

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Create a release build
dotnet publish -c Release -o ./publish
```

### Cross-Compilation to x86_64

To compile a 64-bit x86 binary:

```bash
# Restore and publish as 64-bit Linux binary
dotnet publish -c Release -r linux-x64 --self-contained -o ./publish-x86_64
```

The `./publish-x86_64/TaskManagerCli` binary will be a 64-bit executable.

## Running the Application

### From the project directory (development mode):

```bash
dotnet run
```

### From a published release:

```bash
./publish/TaskManagerCli
```

## Project Structure

```
.
├── Program.cs                    # Application entry point
├── TaskManagerCli.csproj         # Project configuration
├── Models/                       # Data models
│   ├── CpuStat.cs
│   ├── ProcessStat.cs
│   └── TerminalCell.cs
├── Services/                     # Business logic
│   ├── ProcessManager.cs         # Process management
│   ├── StatParser.cs             # System statistics parsing
│   └── Tui/                      # Terminal UI components
│       ├── Components/
│       ├── Core/
│       └── Interfaces/
└── Services/Tui/                 # TUI framework
    ├── TerminalScreen.cs
    └── ...
```

## Features

- Real-time process monitoring
- Terminal-based user interface
- CPU and memory statistics display
- Process management capabilities
- Fully custom rendering engine with double buffering to eliminate screen flicker

## Controls

- `j` - Move selection down
- `k` - Move selection up
- `x` - Kill the selected process
- `q` - Quit the application

## Dependencies

The project uses only the .NET standard library with no external NuGet dependencies.

## License

See project repository for license information.