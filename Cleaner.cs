using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BuildCleanUp;
internal class Cleaner
{
    const string FLUTTER_BUILD_DIRECTORY = "build";
    const string DOTNET_BIN_DIRECTORY = "bin";
    const string DOTNET_OBJ_DIRECTORY = "obj";

    public event EventHandler<ProgressUpdatedEventArgs>? ProgressUpdated;

    /// <summary>
    /// Looks for project build directories and deletes them
    /// </summary>
    /// <param name="options">The program options to configure the run</param>
    public void Run(Options options)
    {
        var searchPatterns = CollectActiveSearchPatternsFromOptions(options);
        if (!searchPatterns.Any())
        {
            OnProgressUpdated("All search patterns disabled!");
            return;
        }

        var directories = FindDirectories(options.BasePath, options.MaxSearchDepth, searchPatterns);
        var directoriesCount = directories.Count();

        OnProgressUpdated($"Found {directories.Count()} directories");

        if (directoriesCount == 0)
        {
            OnProgressUpdated($"Nothing to do here =)");
            return;
        }

        for (var dirIndex = 0; dirIndex < directoriesCount; dirIndex++)
        {
            var dir = directories.ElementAt(dirIndex);
            try
            {
                var dirInfo = new DirectoryInfo(dir);
                if (options.DryRun)
                {
                    OnProgressUpdated($"'{dir}' found");
                }
                else
                {
                    if (dirInfo.Exists)
                    {
                        dirInfo.Delete(true);
                        OnProgressUpdated($"'{dir}' deleted");
                    }
                }
            }
            catch
            {
                OnProgressUpdated($"Could not delete '{dir}'");
            }
        }

        OnProgressUpdated($"Clean up finished ");
    }

    private List<string> CollectActiveSearchPatternsFromOptions(Options options)
    {
        List<string> searchPatterns = [];

        if (!options.DisableFlutter)
            searchPatterns.Add(FLUTTER_BUILD_DIRECTORY);

        if (!options.DisableDotnet)
        {
            searchPatterns.Add(DOTNET_BIN_DIRECTORY);
            searchPatterns.Add(DOTNET_OBJ_DIRECTORY);
        }

        return searchPatterns;
    }

    private IEnumerable<string> FindDirectories(string basePath, int maxDepth, IEnumerable<string> searchPatterns)
    {
        OnProgressUpdated($"Root directory is {basePath}");
        OnProgressUpdated($"Looking for the following folder patterns: {searchPatterns.Aggregate((x, y) => x + "," + y)} ...");

        var enumOptions = new EnumerationOptions
        {
            MaxRecursionDepth = maxDepth,
            RecurseSubdirectories = true,
            MatchCasing = MatchCasing.CaseSensitive
        };

        return searchPatterns.SelectMany(pattern => Directory.EnumerateDirectories(basePath, pattern, enumOptions));
    }

    private void OnProgressUpdated(string message)
    {
        ProgressUpdated?.Invoke(this, new ProgressUpdatedEventArgs(message));
    }
}
