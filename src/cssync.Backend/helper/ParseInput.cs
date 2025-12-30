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
                Console.WriteLine(Resources.Help);
                break;

            case "--status" or "-s":
                if (await ModifyConfig.GetStatus())
                    Console.WriteLine("cssync is currently enabled");
                else
                    Console.WriteLine("cssync is currently disabled");
                break;

            case "--init" or "-i":
                Console.WriteLine("Generating config");
                await Json.GenConfig();
                Console.WriteLine("Finished generating config");
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
                    Console.WriteLine(Resources.HelpConfig);
                    break;

                case "rclone":
                    Console.WriteLine(Resources.HelpRclone);
                    break;

                case "cssync":
                    Console.WriteLine(Resources.HelpCssync);
                    break;

                case "more":
                    Console.WriteLine(Resources.HelpMore);
                    break;

                default:
                    Console.WriteLine($"{option} is unknown. Use --help for available options.");
                    break;
            }
            return;
        }

        // Unknown flag with option
        Console.WriteLine($"{flag} is unknown or not compatible with {option}. Use --help for available options.");
    }
}
