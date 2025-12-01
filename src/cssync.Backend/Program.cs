// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend.helper;

using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;

namespace cssync.Backend;

internal class MainBackend
{
    internal static async Task Main(string[] args)
    {
        Console.WriteLine(Process.GetCurrentProcess());
        Globals.logger.LogInformation("Initiated Backend application.");
        Globals.logger.LogInformation("Make sure rclone is configured. Use `rclone configure` to configure rclone.");
    }
}

public class HttpServer
{
    // TODO: Add HttpServer logic
}
