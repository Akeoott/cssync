// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend.helper;

namespace cssync.Backend;

public static class Cssync
{
    internal const string cssyncOptions = """
        Usage:
            > cssync [option] [scope] [args]
        Options:
            > cssync help    |  See all options
            > cssync list    |  Display entire json config
            > cssync run     |  Run a variable
            > cssync edit    |  Edit a value in config
            > cssync append  |  Append a value to config
            > cssync remove  |  Remove a value from config
        Scopes:
            help             |  See usage and expanded info for each option
            variables        |  Configure your Variables (rclone command presets)
            timer            |  Configure the Timer (when commands periodically execute)
        """;

    public static async Task<string> RunCssync(string option = "help", string? scope = null, params string[] args)
    {
        string warningMsg = "Cssync has not been implemented yet! Please check the main branch out for the latest updates.";
        Log.Warn(warningMsg);
        return warningMsg;
    }
}
