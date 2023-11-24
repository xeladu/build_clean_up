using System;
using System.Collections.Generic;
using System.Linq;

using CommandLine;

namespace BuildCleanUp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(
            $"""

             ####    #    #   #   #       ###            ####   #        ####      #      #    #      #    #   ####
             #   #   #    #   #   #       #   #        #        #       #         # #     ##   #      #    #   #   #
             #   #   #    #   #   #       #    #      #         #       #        #   #    # #  #      #    #   #   #
             ####    #    #   #   #       #    #      #         #       ###      #   #    # #  #      #    #   ####
             ####    #    #   #   #       #    #      #         #       ###     #######   #  # #      #    #   #
             #   #   #    #   #   #       #    #      #         #       #       #     #   #  # #      #    #   #
             #   #   #    #   #   #       #   #        #        #       #       #     #   #   ##      #    #   #
             ####     ####    #    ####   ###           ####     ####    ####   #     #   #    #       ####    #

            """);

        Parser.Default
            .ParseArguments<Options>(args)
            .WithParsed(RunApp)
            .WithNotParsed(HandleErrors);
    }

    static void HandleErrors(IEnumerable<Error> errors)
    {
        errors.ToList().ForEach(Console.WriteLine);
    }

    static void RunApp(Options options)
    {
        try
        {
            var c = new Cleaner();
            c.ProgressUpdated += (s, e) => Console.WriteLine(e.Message);
            c.Run(options);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
        }
    }
}
