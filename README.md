# build_clean_up
Console application to clean up local build files for various project types. This is useful
to free up space on the hard disk.

## How it works

The program searches recursively from the given base directory for folders that indicate 
being a build directory of a programming framework.

## Supported Frameworks

- Flutter (looks for the "build" folder)
- .NET (looks for "bin" and "obj" folders)

## Arguments

- `-p`, `--path`: The root directory to start the search from (required)
- `--dry-run`: Prints the found directories only, no deleting (default: false)
- `max-depth`: The maximum amount of subdirectories to traverse (default: 3)
- `disable-flutter`: Disables the search for Flutter build directories
- `disable-dotnet`: Disables the search for .NET build directories

## How to run

- Clone the repository
- Build the solution with Visual Studio or msbuild
- Execute the `BuildCleanUp.exe` file in `bin\xxx\net8.0`

## Stack

- .NET8
- Cross platform (Windows, Linux, Mac)
