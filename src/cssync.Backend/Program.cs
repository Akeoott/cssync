// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Backend.helper;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace cssync.Backend;

internal class MainBackend
{
    [DllImport("libc")]
    private static extern int isatty(int fd);
    public static async Task<bool> HasTerminal()
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return !Console.IsOutputRedirected && !Console.IsInputRedirected;
            }
            else
            {
                return isatty(0) == 1; // stdin
            }
        }
        catch
        {
            return false;
        }
    }

    internal static async Task Main(string[] args)
    {
        if (!await HasTerminal())
        {
            Log.Critical("This program must be run from a terminal.");
            Thread.Sleep(1000);
            return;
        }

        Log.Debug("Current process: {ProcessName}", Process.GetCurrentProcess());
        switch (args.Length)
        {
            case 0:
                ParseInput.NoArguments();
                break;

            case 1:
                await ParseInput.SingleArgument(args[0]);
                break;

            case 2:
                await ParseInput.TwoArguments(args[0], args[1]);
                break;

            default:
                Console.WriteLine($"Too many arguments. Use --help for usage.");
                break;
        }

        // await Cssync.RunCssync();
    }
}
