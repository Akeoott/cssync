// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend.helper;

using System;
using System.Threading.Tasks;

namespace cssync.Backend;

public class Cssync
{
    internal const string cssyncOptions = """
        Usage:
            > cssync [options][scope]
        Options:
            > cssync help    |  See all options
            > cssync edit    |  Edit a value in config
            > cssync append  |  Append a value to config
            > cssync remove  |  Remove a value from config
            > cssync list    |  Display entire json config
            > cssync run     |  Run a variable
        Scopes (case sensitive):
            help             |  See usage and expanded info for each option
            Varaibles        |  Configure your Variables (rclone command presets)
            Timer            |  Configure the Timer (when commands periodically execute)
            Manual           |  Enter your own args (not recommended)
        """;

    internal static readonly string[] commandList = ["edit", "append", "remove", "list", "run"];

    public static async Task<string> RunCssync(params string[] commands)
    {
        Log.Warn("Cssync is still under development! Check out GitHub (Akeoott/cssync) for the latest commits");
        return "Cssync is still under development! Check out GitHub (Akeoott/cssync) for the latest commits";

        Log.Info("Running rclone command");
        if (commands == null || commands.Length == 0)
            return "No commands provided";
        foreach (var cmd in commands)
            HandleInput(cmd.Trim());
        return "";
    }

    private static void HandleInput(params string[] commands)
    {

    }
}
