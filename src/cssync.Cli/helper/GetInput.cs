// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

namespace cssync.Cli;

internal static class GetInput
{
    internal static string GetString(string prompt)
    {
        string? value;

        do
        {
            Console.Write(prompt);
            value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Please enter a non-empty value.");
            }
        } while (string.IsNullOrWhiteSpace(value));
        return value.Trim();
    }
}
