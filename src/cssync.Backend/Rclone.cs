// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the LGPL-3.0 License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;

namespace cssync.Backend;

public class Rclone
{
    public async Task<string> ExecuteCommand(string arguments)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "rclone",
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();
        process.StartInfo = processStartInfo;

        process.Start();

        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            throw new Exception($"Rclone failed: {error}");
        }

        return output;
    }
}
