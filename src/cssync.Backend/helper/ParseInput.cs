// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Backend.helper;

internal class ParseInput
{
    internal static void NoArguments()
    {
        Console.WriteLine("Use --help for available options.");
    }

    internal static async Task SingleArgument(string arg)
    {
        switch (arg)
        {
            case "--help" or "-h":
                Console.WriteLine(
                    """
                    Usage:
                        cssync [flag] [help-scope]
                    Flags:
                        --help     |  Learn how to use the available scope
                        --status   |  Display if cssync is enabled or disabled
                        --enable   |  Enable cssync to start performing rclone operations
                        --disable  |  Disable cssync to stop performing rclone operations
                    Help-Scopes (only usable with the --help flag):
                        config     |  Learn to configure cssync and rclone
                        rclone     |  Learn how to use rclone
                        cssync     |  Learn how to use cssync
                        more       |  Learn the in depth details about cssync

                    Please read through the options before using cssync!
                    """);
                break;

            case "--status" or "-s":
                if (await ModifyConfig.GetCssyncStatus())
                    Console.WriteLine("cssync is currently enabled");
                else
                    Console.WriteLine("cssync is currently disabled");
                break;

            case "--enable" or "-e":
                await ModifyConfig.EnableDisableCssync(true);
                Console.WriteLine("Successfully enabled cssync to perform rclone operations");
                break;

            case "--disable" or "-d":
                await ModifyConfig.EnableDisableCssync(false);
                Console.WriteLine("Successfully disabled cssync from performing rclone operations");
                break;

            default:
                Console.WriteLine($"{arg} is unknown. Use --help for available options.");
                break;
        }
    }

    internal static async Task TwoArguments(string flag, string option)
    {
        // These flags don't accept options
        if (flag is "--enable" or "-e" or "--disable" or "-d")
        {
            Console.WriteLine($"{flag} does not accept options.");
            return;
        }

        // Only help flag accepts options
        if (flag is "--help" or "-h")
        {
            switch (option)
            {
                case "config":
                case "rclone":
                case "cssync":
                case "more":
                    Console.WriteLine(
                        $"""
                        This option is not available at the moment.
                        Check back later for {option} help.
                        """);
                    break;

                default:
                    Console.WriteLine($"{option} is unknown. Use --help for available options.");
                    break;
            }
            return;
        }

        // Unknown flag with option
        Console.WriteLine($"{flag} is unknown. Use --help for available options.");
    }
}
