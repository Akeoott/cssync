// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend.helper;

using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace cssync.Backend;

internal class MainBackend
{
    internal static void Main(string[] args)
    {
        Console.WriteLine(Process.GetCurrentProcess());
        Log.BackendInfo("Initiated Backend application.");
        Log.BackendInfo("Make sure rclone is configured. Use `rclone configure` to configure rclone.");
        InitBackend();
    }

    internal static void InitBackend()
    {
        Thread backgroundThread = new(static async () => Backend())
        {
            Name = "backgroundThread"
        };
        Log.BackendInfo("Started backgroundThread");
        backgroundThread.Start();
    }

    internal static void Backend()
    {

    }
}
