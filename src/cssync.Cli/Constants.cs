// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Cli;

internal static class Constants
{
    internal const string invalidOption =
        "Enter a valid command (enter 'help' to list all commands). ";

    internal const string unavailableOption =
        "This operation is not available.\nIt may not have be integrated yet.";

    internal const string mainOptions =
        "Enter a command\n" +
        "> help   (h)  |  See all options\n" +
        "> quit   (q)  |  Quit program\n" +
        "> rclone (r)  |  Interact directly with rclone\n" +
        "> cssync (c)  |  Interact with cssync";

}
