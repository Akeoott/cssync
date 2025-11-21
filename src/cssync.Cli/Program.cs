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

        Console.WriteLine(Constants.mainOptions);

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
                    Console.WriteLine(Constants.mainOptions);
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
        Console.WriteLine(Constants.unavailableOption);
    }
}
