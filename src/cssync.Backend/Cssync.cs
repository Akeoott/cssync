// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using Newtonsoft.Json.Linq;

using cssync.Backend.helper;

namespace cssync.Backend;

/// <summary>
/// Reads config and acts on the data it contains
/// </summary>
public static class Cssync
{
    public static async Task RunCssync()
    {
        if (!await Json.GetConfigStatus())
        {
            Log.Info("cssync is currently disabled by config");
        }

        var cts = new CancellationTokenSource();
        var token = cts.Token;

        Task task = Task.Run(() => CssyncLoop(token), token);

        while (true)
        {
            if (!await Json.GetConfigStatus())
            {
                cts.Cancel();
                await Task.WhenAll(task);
                Log.Info("cssync was stopped by config");
                return;
            }
            await Task.Delay(5000);
        }
    }

    private static async Task CssyncLoop(CancellationToken token)
    {
        // TODO
    }
}
