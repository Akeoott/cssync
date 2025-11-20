// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;
using System.ComponentModel;

namespace cssync.Backend;

/// <summary>
/// Allows indirect interactions with rclone by passing in variables.
/// </summary>
/// <returns>
/// A string containing the output of rclone.
/// </returns>
public class Rclone
{
    public async Task<string> RunRclone(params string[] commands)
    {
        if (commands == null || commands.Length == 0)
        {
            return "Error: No commands provided.";
        }

        var results = new List<string>();

        foreach (var command in commands)
        {
            var result = await ExecCommand(command.Trim());
            results.Add(result);
        }

        return string.Join("\n", results);
    }

    private async Task<string> ExecCommand(string commandArgs)
    {
        var psi = new ProcessStartInfo
        {
            FileName = GetExecutableName(),
            Arguments = commandArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = psi };

        try
        {
            process.Start();
        }
        catch (Win32Exception ex) when (ex.NativeErrorCode == 2)
        {
            return "Error: rclone not found. Make sure it is installed and in your PATH.";
        }
        catch (Exception ex)
        {
            return $"Error starting rclone: {ex.Message}";
        }

        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            return !string.IsNullOrWhiteSpace(error)
                ? error
                : $"Command failed with exit code {process.ExitCode}";
        }

        return output;
    }

    private string GetExecutableName()
    {
        return OperatingSystem.IsWindows() ? "rclone.exe" : "rclone";
    }
}
