// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;
using System.ComponentModel;
using System.Text;

namespace cssync.Backend;

public class Rclone
{
    public async Task<string> RunRclone(params string[] commands)
    {
        if (commands == null || commands.Length == 0)
            return "Error: No commands provided.";

        var tasks = commands.Select(cmd => ExecCommand(cmd.Trim()));
        var results = await Task.WhenAll(tasks);
        return string.Join("\n", results);
    }

    private async Task<string> ExecCommand(string commandArgs)
    {
        return IsInteractiveCommand(commandArgs)
            ? await ExecInteractiveCommand(commandArgs)
            : await ExecNonInteractiveCommand(commandArgs);
    }

    #region INTERACTIVE
    private async Task<string> ExecInteractiveCommand(string commandArgs)
    {
        var psi = CreateProcessStartInfo(commandArgs, interactive: true);

        using var process = new Process { StartInfo = psi };

        if (!TryStartProcess(process, out string error))
            return error;

        await process.WaitForExitAsync();
        return $"Interactive command '{commandArgs}' completed with exit code {process.ExitCode}";
    }
    #endregion

    #region NON-INTERACTIVE
    private async Task<string> ExecNonInteractiveCommand(string commandArgs)
    {
        var psi = CreateProcessStartInfo(commandArgs, interactive: false);

        using var process = new Process { StartInfo = psi };
        var output = new StringBuilder();
        var errors = new StringBuilder();

        if (!TryStartProcess(process, out string error))
            return error;

        var outputTask = ReadStreamLineByLine(process.StandardOutput, output, line => Console.WriteLine(line));
        var errorTask = ReadStreamLineByLine(process.StandardError, errors, line => Console.WriteLine($"[ERROR] {line}"));

        await process.WaitForExitAsync();
        await Task.WhenAll(outputTask, errorTask);

        if (process.ExitCode != 0)
            return errors.Length > 0 ? errors.ToString() : $"Command failed with exit code {process.ExitCode}";

        return output.ToString();
    }
    #endregion

    #region UTILITIES
    private ProcessStartInfo CreateProcessStartInfo(string args, bool interactive)
    {
        return new ProcessStartInfo
        {
            FileName = GetExecutableName(),
            Arguments = args,
            UseShellExecute = interactive,
            CreateNoWindow = !interactive,
            RedirectStandardOutput = !interactive,
            RedirectStandardError = !interactive
        };
    }

    private bool TryStartProcess(Process process, out string error)
    {
        try
        {
            process.Start();
            error = "";
            return true;
        }
        catch (Win32Exception ex) when (ex.NativeErrorCode == 2)
        {
            error = "Error: rclone not found. Make sure it is installed and in your PATH.";
            return false;
        }
        catch (Exception ex)
        {
            error = $"Error starting rclone: {ex.Message}";
            return false;
        }
    }

    private async Task ReadStreamLineByLine(StreamReader reader, StringBuilder buffer, Action<string> onLine)
    {
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            buffer.AppendLine(line);
            onLine(line);
        }
    }

    private bool IsInteractiveCommand(string command)
    {
        var first = command.Split(' ')[0].ToLowerInvariant();
        return first is "config" or "authorize" or "setup";
    }

    private string GetExecutableName()
    {
        return OperatingSystem.IsWindows() ? "rclone.exe" : "rclone";
    }
    #endregion
}
