// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Cli.helper;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace cssync.Cli.helper;

/// <summary>
/// Check if application is running in the terminal. If not, opens one.
/// </summary>
internal static class Terminal
{
    /// <summary>
    /// Check if application is running inside a terminal.
    /// </summary>
    /// <returns>True if application is running in a terminal</returns>
    internal static bool IsRunningInTerminal()
    {
        if (!Environment.UserInteractive)
            return false;

        try
        {
            return !Console.IsInputRedirected &&
                   !Console.IsOutputRedirected &&
                   Console.WindowWidth > 0 &&
                   Console.WindowHeight > 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Attempts to run application in a terminal.
    /// </summary>
    internal static void RelaunchInTerminal()
    {
        string exePath = Process.GetCurrentProcess().MainModule?.FileName ?? AppContext.BaseDirectory.TrimEnd('/', '\\');

        if (!File.Exists(exePath))
            exePath = Environment.ProcessPath ?? exePath;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Use cmd as the most reliable default
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k \"\"{exePath}\"\"",
                UseShellExecute = true
            });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // xdg-terminal is standard (if available)
            if (CommandExists("xdg-terminal"))
            {
                Process.Start("xdg-terminal", $"\"{exePath}\"");
            }
            else
            {
                // Fallback: Try common terminals
                string[] t =
                [
                    "x-terminal-emulator", "gnome-terminal", "konsole",
                    "xfce4-terminal", "xterm"
                ];

                foreach (var term in t)
                {
                    if (CommandExists(term))
                    {
                        Process.Start(term, $" -e \"{exePath}\"");
                        Finish();
                        return;
                    }
                }

                // Last fallback
                Process.Start("bash", $"-c \"'{exePath}'; exec bash\"");
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // macOS: open Terminal.app
            Process.Start("open", $"-a Terminal \"{exePath}\"");
        }
        else
        {
            // Unknown OS – fallback
            Process.Start(exePath);
        }

        Finish();
    }

    private static bool CommandExists(string cmd)
    {
        try
        {
            using var prog = Process.Start(new ProcessStartInfo
            {
                FileName = "which",
                Arguments = cmd,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            prog?.WaitForExit(150);
            return prog?.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }

    private static void Finish()
    {
        Environment.Exit(0);
    }
}

