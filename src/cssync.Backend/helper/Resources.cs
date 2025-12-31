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
        Configuring cssync (manual editing required until GUI is available):

        Overview:
            This application is primarily a GUI for managing rclone operations. Until the GUI is available,
            you must configure cssync manually by editing the config.json file located next to the application binary.

        Initializing Config:
            Run the application with --init to generate a default config if one does not exist.
            Default config structure:
            {
              "Run": false,
              "Variables": {},
              "Timers": {}
            }

        Config Fields:

        1. "Run":
            Controls whether cssync is active.
            - true  => cssync will execute rclone commands
            - false => cssync will not execute any commands

        2. "Variables":
            Stores named groups of rclone commands.
            - Each key represents a user-defined preset (e.g., "key1").
            - The value is an ordered list of rclone commands that run sequentially.
            Example:
            "Variables": {
              "key1": [
                "sync ~/Pictures remote:Pictures/",
                "delete remote:Pictures/"
              ],
              "key2": [
                "sync ~/Downloads remote:Backup/"
              ]
            }

            Important:
            - Each Variable requires a corresponding Timer with the exact same key name to execute.
            - Variables can contain multiple keys, each representing a distinct command group.
            - For rclone help, run the application with --help rclone or visit: https://rclone.org/docs/

        3. "Timers":
            Stores delay times (in seconds) before executing a Variable.
            Example:
            "Timers": {
              "key1": [3600], // 1 hour delay
              "key2": [1200]  // 20 minute delay
            }

            Notes:
            - Each Timer key must match a Variable key.
            - Currently, only a single integer value per Timer is supported (array reserved for future expansion).

        Full Config Example:
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
                "key1": [3600],
                "key2": [1200]
              }
            }

        Configuring rclone:
            1. Open a terminal and run `rclone config`.
            2. Follow the prompts to set up your remotes.
            3. For detailed guidance, refer to official rclone docs or tutorials:
               https://rclone.org/docs/
        """;

    internal const string HelpRclone = "This option is currently under development! Please try again later.";
    internal const string HelpCssync = "This option is currently under development! Please try again later.";
    internal const string HelpMore = "This option is currently under development! Please try again later.";
}
