// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using System.Text.Json;

namespace cssync.Backend.helper;

/// <summary>
/// Manage `config.json` for cssync
/// </summary>
public class Config
{
    public required Dictionary<string, List<string>> Variables { get; set; }
    public required Dictionary<string, List<int>> Timer { get; set; }
}

public class Json
{
    public static readonly string configPath = AppDomain.CurrentDomain.BaseDirectory + "config.json";
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };

    internal static string Serialize(Config input)
    {
        Log.BackendInfo("Serializing json data.");
        return JsonSerializer.Serialize(input, options);
    }

    /// <summary>
    /// Deserializes config located next to application
    /// </summary>
    /// <returns>Config of application</returns>
    internal static Config Deserialize()
    {
        int attempts = 0;

        while (attempts < 3)
        {
            if (!File.Exists(configPath))
            {
                GenConfig();
            }
            try
            {
                Log.BackendInfo("Reading config.");

                string json = File.ReadAllText(configPath);
                return JsonSerializer.Deserialize<Config>(json)
                    ?? throw new InvalidDataException("Output is null or empty.");
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is JsonException || ex is InvalidDataException)
            {
                attempts++;
                Log.BackendCritical("Loading config failed (attempt {attempts}/3). {ex}: {ex.Message}.", attempts, ex, ex.Message);
                Thread.Sleep(100);
            }
        }
        throw new InvalidOperationException($"Something went wrong. Failed to get config after {attempts} attempts.");
    }

    /// <summary>
    /// Generates a json file in application directory if no file was found.
    /// </summary>
    private static void GenConfig()
    {
        if (File.Exists(configPath))
        {
            Log.BackendInfo("Config exists.");
            return;
        }
        else
        {
            string jsonString;

            Config config = new()
            {
                Variables = [],
                Timer = [],
            };

            Log.BackendWarn("Config doesn't exist.\nGenerating config.");

            jsonString = Serialize(config);
            File.WriteAllText(configPath, jsonString);

            Log.BackendInfo("Successfully wrote config.");
        }
    }

    /// <summary>
    /// Edit value in config
    /// </summary>
    public static void EditConfig()
    {

    }

    /// <summary>
    /// Read config values
    /// </summary>
    public static void ReadConfig()
    {

    }
}
