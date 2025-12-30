// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend.helper;

namespace cssync.Backend;

/// <summary>
/// Reads config and acts on the data it contains
/// </summary>
public static class Cssync
{
    public static async Task RunCssync()
    {
        if (!await ModifyConfig.GetStatus())
        {
            Log.Info("cssync is currently disabled by config");
        }
        // TODO: Add Cssync logic
    }
}
