// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend;

namespace cssync.Cli;

internal class MainCli
{
    internal static async Task Main(string[] args)
    {
        await Run();
    }

    internal static async Task Run()
    {
        string input;
        string response;

        Console.WriteLine(Constants.mainOptions);

        while (true)
        {
            input = GetInput.GetString("\n~\n> ").ToLower();
            response = "";

            switch (input)
            {
                case "quit" or "q":
                    return;

                case "rclone" or "r":
                    response = await InitRclone();
                    break;

                case "cssync" or "c":
                    response = await InitCssync();
                    break;

                case "help" or "h":
                    Console.WriteLine(Constants.mainOptions);
                    break;

                default:
                    Console.WriteLine(Constants.invalidOption);
                    break;
            }
            Console.WriteLine(response);
        }
    }

    internal static async Task<string> InitRclone()
    {
        return Constants.unavailableOption;
    }

    internal static async Task<string> InitCssync()
    {
        return Constants.unavailableOption;
    }
}
