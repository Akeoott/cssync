// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Backend.helper;

internal static class Resources
{
    internal const string Help = """
        cssync is work in progress. If you find any bugs or have suggestions,
        please open an issue on github Akeoott/cssync, any help is appreciated!

        Usage:
            cssync [flag] [help-scope]
        Flags:
            --help    |  Learn how to use the available scope
            --status  |  Display if cssync is enabled or disabled
            --init    |  Generate the config
        Help-Scopes (only usable with the --help flag):
            config    |  Learn to configure cssync and rclone
            rclone    |  Learn how to use rclone
            cssync    |  Learn how to use cssync
            more      |  Learn the in-depth details about cssync

        Please read through the options before using cssync!
        """;

    internal const string HelpConfig = """
        Note (ignore till the GUI is available):
            When using this application without the GUI,
            configuration must be done manually.

        How to configure cssync:
            cssync currently stores config.json next to the application binary in all cases.
            If the config does not exist, run this application with --init to generate one.
            Once you found the config, open it and manually edit its values.
            The default config is going to look like this:
                {
                  "Run": false,
                  "Variables": {},
                  "Timers": {}
                }

            "Run":
                It's basically an on and off switch:
                If "Run" = true, it will run cssync.
                If "Run" = false, it will not run cssync.

            "Variables":
                Stores all your rclone presets.
                Each key in "Variables" is an ordered list of rclone commands that run sequentially.
                Here is an example on how "Variables" can look like:
                    "Variables": {
                      "key1": [
                        "sync ~/Pictures remote:Pictures/",
                        "delete remote:Pictures/"
                      ],
                      "key2": [
                        "sync ~/Downloads remote:Backup/",
                        "delete remote:Backup/"
                      ]
                    },

                IMPORTANT:
                    A timer is required to execute a Variable.
                    For a timer to work, it must have the exact same name as the Variable it affects.

                "Variables" may contain multiple keys.
                Each key is a user-chosen name that represents a group of rclone commands.

                For more information about rclone, run the application with --help rclone

            "Timers":
                Stores a delay time in seconds before executing the matching Variable.
                1200 in "Timers" equals a 20 minute delay.
                3600 in "Timers" equals a 1 hour delay.
                Here is an example on how "Timers" can look like:
                    "Timers": {
                      "key1": [
                        1200
                      ],
                      "key2": [
                        3600
                      ]
                    },

                For a timer to work, it must have the exact same name as the Variable it affects.
                Otherwise, it will not work. A timer is required to execute a variable.

                Note:
                    Although timers are arrays internally (for future expansion),
                    each timer must contain exactly ONE integer value today.

            Full config example:
                {
                  "Run": true,
                  "Variables": {
                    "key1": [
                      "sync ~/Pictures remote:Pictures/",
                      "sync ~/Videos remote:Videos/"
                    ],
                    "key2": [
                      "sync ~/Documents remote:Backup/"
                    ]
                  },
                  "Timers": {
                    "key1": [
                      3600
                    ],
                    "key2": [
                      1200
                    ]
                  },
                }

        How to configure rclone:
            Open your terminal and enter rclone config.
            Follow the instructions provided by rclone.
            If you're unsure how to correctly configure rclone,
            please read the official rclone documentation for your remote,
            aka cloud storage provider or watch a YouTube video explaining it.

            For more information about rclone, run the application with --help rclone

            https://rclone.org/docs/
        """;

    internal const string HelpRclone = "This option is currently under development! Please try again later.";
    internal const string HelpCssync = "This option is currently under development! Please try again later.";
    internal const string HelpMore = "This option is currently under development! Please try again later.";
}
