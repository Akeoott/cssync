// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using System.Text.Json;

namespace cssync.Backend.helper;

public class Config
{
    public required Dictionary<string, List<string>> Variables { get; set; }
    public required Dictionary<string, List<int>> Timer { get; set; }
}

public class Json
{
    internal static readonly string configPath = AppDomain.CurrentDomain.BaseDirectory + "config.json";
    internal static readonly JsonSerializerOptions options = new() { WriteIndented = true };

    /// <summary>
    /// Serializes input from the Config class
    /// </summary>
    /// <param name="input">json config</param>
    /// <returns>string containing serialized config</returns>
    internal static string Serialize(Config input)
    {
        return JsonSerializer.Serialize(input, options);
    }

    /// <summary>
    /// Deserializes config located next to application.
    /// Generates config if not found.
    /// </summary>
    /// <returns>Config of application</returns>
    internal static async Task<Config> Deserialize()
    {
        int attempts = 0;

        while (attempts < 3)
        {
            if (!File.Exists(configPath))
            {
                await GenConfig();
            }
            try
            {
                Log.BackendDebug("Reading config.");

                string json = await File.ReadAllTextAsync(configPath);
                return JsonSerializer.Deserialize<Config>(json)
                    ?? throw new InvalidDataException("Output is null or empty.");
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is JsonException || ex is InvalidDataException)
            {
                attempts++;
                Log.BackendCritical("Loading config failed (attempt {attempts}/3). {ex}: {ex.Message}.", attempts, ex, ex.Message);
                await Task.Delay(100);
            }
        }
        throw new InvalidOperationException($"Something went wrong. Failed to get config after {attempts} attempts.");
    }

    /// <summary>
    /// Generates a json file in application directory if no file was found.
    /// </summary>
    internal static async Task GenConfig()
    {
        Log.BackendInfo("Generating config.");
        if (File.Exists(configPath))
        {
            Log.BackendInfo("Config exists.");
            return;
        }
        else
        {
            Log.BackendWarn("Config doesn't exist.\nGenerating config.");
            Config config = new()
            {
                Variables = [],
                Timer = [],
            };
            await WriteConfig(config);
            Log.BackendInfo("Successfully wrote config.");
        }
    }

    /// <summary>
    /// Writes config to file asynchronously
    /// </summary>
    /// <param name="config">Config to write</param>
    internal static async Task WriteConfig(Config config)
    {
        try
        {
            string jsonString = Serialize(config);
            await File.WriteAllTextAsync(configPath, jsonString);
            Log.BackendInfo("Successfully wrote config.");
        }
        catch (Exception ex)
        {
            Log.BackendCritical("Failed to write config. {ex}: {ex.Message}", ex, ex.Message);
        }
    }

    /// <summary>
    /// Get current configuration of cssync.
    /// </summary>
    /// <returns>Current config</returns>
    public static async Task<Config> GetConfig()
    {
        return await Deserialize();
    }
}
