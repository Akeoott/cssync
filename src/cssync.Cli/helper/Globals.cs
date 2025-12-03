// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Cli.helper;

internal class StringConst
{
    internal const string unavailableOption =
        "This operation is not available.\nIt may not have been integrated yet.";

    internal const string mainOptions =
        "Available commands:\n" +
        "\t> help    |  See all options\n" +
        "\t> exit    |  Exit program\n" +
        "\t> rclone  |  Interact directly with rclone\n" +
        "\t> cssync  |  Interact with cssync";
}
