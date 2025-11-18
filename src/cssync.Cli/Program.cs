// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend;

namespace cssync.Cli;

internal class MainCli
{
    internal static async Task Main(string[] args)
    {
        var rc = new Rclone();

        string response;
        string input;

        while (true)
        {
            input = GetInput.GetString("Enter a command: ");
            response = await rc.ExecuteCommand("--help");
            Console.WriteLine(response);
        }

    }
}

