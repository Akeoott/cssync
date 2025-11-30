// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend;

using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace cssync.Cli;

internal class MainCli
{
    internal static async Task Main(string[] args)
    {
        Console.WriteLine(Process.GetCurrentProcess());
        Globals.logger.LogInformation("Initiated logging for CLI application.");

        Globals.logger.LogInformation("Make sure rclone is configured. If not, do so.");
        Globals.logger.LogInformation("Use `rclone configure` to configure rclone.");

        await InitBackend();
        await RunCLI();
    }

    internal static async Task InitBackend()
    {
        var backendProcesses = Process.GetProcessesByName("cssync.Backend");
        bool currentBackendProcess = backendProcesses.Length > 0;

        if (currentBackendProcess)
        {
            Globals.logger.LogInformation("Detected `cssync.Backend` successfully");
        }
        else
        {
            Globals.logger.LogWarning("Could not detect `cssync.Backend`");
            Globals.logger.LogInformation("Attempting start backend.");
        }

        Globals.logger.LogWarning("Could not detect backend as this feature was not implemented yet.");
    }

    internal static async Task RunCLI()
    {
        string input;

        Console.WriteLine(StringConst.mainOptions);

        while (true)
        {
            input = GetInput.GetString("\n~\n> ").ToLower();

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
                    Console.WriteLine(StringConst.mainOptions);
                    break;
            }
        }
    }

    internal static async Task InitRclone()
    {
        var rc = new Rclone();
        string input;
        string response;

        while (true)
        {
            Console.WriteLine("Enter 'return' to go back");
            input = GetInput.GetString("\n~\n> rclone ").ToLower();

            if (input == "return")
            {
                return;
            }
            else
            {
                response = await rc.RunRclone(input);
                Console.WriteLine(response);
            }
        }
    }

    internal static async Task InitCssync()
    {
        Globals.logger.LogWarning(StringConst.unavailableOption);
    }
}
