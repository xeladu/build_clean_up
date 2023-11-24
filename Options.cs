using CommandLine;

namespace BuildCleanUp;
internal class Options
{
    [Option('p', "path", Required = true, HelpText = "The root directory to start the search from")]
    public string BasePath { get; set; } = "";

    [Option("dry-run", Required = false, HelpText = "Prints the found directories only, no deleting (default: false)")]
    public bool DryRun { get; set; } = false;

    [Option("max-depth", Required = false, HelpText = "The maximum amount of subdirectories to traverse (default: 3)")]
    public int MaxSearchDepth { get; set; } = 3;

    [Option("disable-flutter", Required = false, HelpText = "Disables the search for Flutter build directories")]
    public bool DisableFlutter { get; set; } = false;

    [Option("disable-dotnet", Required = false, HelpText = "Disables the search for .NET build directories")]
    public bool DisableDotnet { get; set; } = false;
}
