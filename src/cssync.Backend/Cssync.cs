// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend.helper;

namespace cssync.Backend;

public class Cssync
{
    public static async Task<string> RunCssync(params string[] commands)
    {
        Log.Warn("This operation is not available.\nIt may not have been integrated yet.");
        return "";
    }

    // TODO: Rewrite pending...
}
