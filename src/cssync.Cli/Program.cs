// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend;
using cssync.Backend.helper;
using cssync.Cli.helper;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace cssync.Cli;

internal class MainCli
{
    internal static async Task Main()
    {
        if (!HasTerminal())
        {
            Console.WriteLine("This program must be run from a terminal.");
            return;
        }
        Console.WriteLine(Process.GetCurrentProcess());
        Log.Info("Initiated CLI application. Make sure rclone is configured. Use `rclone config` to configure rclone.");

        await RunCLI();
    }

    [DllImport("libc")]
    private static extern int isatty(int fd);
    public static bool HasTerminal()
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return !Console.IsOutputRedirected && !Console.IsInputRedirected;
            }
            else
            {
                return isatty(0) == 1; // stdin
            }
        }
        catch
        {
            return false;
        }
    }

    internal const string mainOptions = """
        Usage:
            > [options]
        Options:
            > help    |  See all options
            > exit    |  Exit program
            > rclone  |  Interact directly with rclone
            > cssync  |  Interact with cssync
        """;

    internal static async Task RunCLI()
    {
        string input;

        Console.WriteLine(mainOptions);
        Console.WriteLine("\nWARNING: Its highly recommended to use the UI version of cssync (If available)");

        while (true)
        {
            input = GetInput.GetString("\n~\n> ");

            switch (input)
            {
                case "exit":
                    return;

                case "rclone":
                    await InitRclone();
                    break;

                case "cssync":
                    await InitCssync();
                    break;

                default:
                    Console.WriteLine(mainOptions);
                    break;
            }
        }
    }

    internal static async Task InitRclone()
    {
        string input;
        string response;

        while (true)
        {
            Console.WriteLine("Enter 'return' to go back");
            input = GetInput.GetString("\n~\n> rclone ");

            if (input == "return")
            {
                return;
            }
            else
            {
                response = await Rclone.RunRclone(input);
                Console.WriteLine(response);
            }
        }
    }

    internal static async Task InitCssync()
    {
        string input;
        string response;

        while (true)
        {
            Console.WriteLine("Enter 'return' to go back");
            input = GetInput.GetString("\n~\n> cssync ");

            if (input == "return")
            {
                return;
            }
            else
            {
                response = await Cssync.RunCssync(input);
                Console.WriteLine(response);
            }
        }
    }
}
