// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Backend.helper;

internal class ParseInput
{
    internal static void NoArguments()
    {
        Console.WriteLine("Use --help for available options.");
    }

    internal static (string[], bool) ForceArgument(string[] args)
    {
        bool forced = false;
        if (args.Contains("--force") || args.Contains("-f"))
        {
            Array.Clear(args);
            forced = true;
        }
        return (args, forced);
    }

    internal static async Task SingleArgument(string arg)
    {
        switch (arg)
        {
            case "--help" or "-h":
                Console.WriteLine
                (
                    """
                    Usage:
                        cssync [flag] [option]  |  Only one flag and option at a time.
                    Flags taking options:
                        --help      |  Learn how to use the available options
                    Flags not taking options:
                        --enable    |  Enable cssync to start performing rclone operations
                        --disable   |  Disable cssync to stop performing rclone operations
                        --force     |  Force running this application without a terminal (Can cause issues)
                    Options:
                        config      |  Learn to configure cssync and rclone
                        rclone      |  Learn how to use rclone
                        cssync      |  Learn how to use cssync
                        learn-more  |  Learn the in depth details about cssync

                    Please read through the options before using cssync!
                    """
                );
                break;

            case "--enable" or "-e":
                // TODO: Add enabling value to config
                Console.WriteLine("This option will come soon...");
                break;

            case "--disable" or "-d":
                // TODO: Add disabling value to config
                Console.WriteLine("This option will come soon...");
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
                case "learn-more":
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
