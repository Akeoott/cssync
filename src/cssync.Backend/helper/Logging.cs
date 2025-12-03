// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.Extensions.Logging;

namespace cssync.Backend.helper;

public static class Log
{
    // Lazy shared logger factory
    private static readonly Lazy<ILoggerFactory> _loggerFactory = new(() =>
    {
        return LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                options.IncludeScopes = true;
            });
        });
    });

    // Logger instances
    public static readonly ILogger Backend = InitLogger("cssync.Backend");
    public static readonly ILogger Cli = InitLogger("cssync.Cli");
    public static readonly ILogger Ui = InitLogger("cssync.Ui");

    // Backend
    public static void BackendDebug(string message, params object[] args)
        => Backend.LogDebug(message, args);
    public static void BackendInfo(string message, params object[] args)
        => Backend.LogInformation(message, args);
    public static void BackendWarn(string message, params object[] args)
        => Backend.LogWarning(message, args);
    public static void BackendError(string message, params object[] args)
        => Backend.LogError(message, args);
    public static void BackendError(Exception ex, string message, params object[] args)
        => Backend.LogError(ex, message, args);
    public static void BackendCritical(string message, params object[] args)
        => Backend.LogCritical(message, args);
    public static void BackendCritical(Exception ex, string message, params object[] args)
        => Backend.LogCritical(ex, message, args);

    // CLI
    public static void CliDebug(string message, params object[] args)
        => Cli.LogDebug(message, args);
    public static void CliInfo(string message, params object[] args)
        => Cli.LogInformation(message, args);
    public static void CliWarn(string message, params object[] args)
        => Cli.LogWarning(message, args);
    public static void CliError(string message, params object[] args)
        => Cli.LogError(message, args);
    public static void CliError(Exception ex, string message, params object[] args)
        => Cli.LogError(ex, message, args);
    public static void CliCritical(string message, params object[] args)
        => Cli.LogCritical(message, args);
    public static void CliCritical(Exception ex, string message, params object[] args)
        => Cli.LogCritical(ex, message, args);

    // UI
    public static void UiDebug(string message, params object[] args)
        => Ui.LogDebug(message, args);
    public static void UiInfo(string message, params object[] args)
        => Ui.LogInformation(message, args);
    public static void UiWarn(string message, params object[] args)
        => Ui.LogWarning(message, args);
    public static void UiError(string message, params object[] args)
        => Ui.LogError(message, args);
    public static void UiError(Exception ex, string message, params object[] args)
        => Ui.LogError(ex, message, args);
    public static void UiCritical(string message, params object[] args)
        => Ui.LogCritical(message, args);
    public static void UiCritical(Exception ex, string message, params object[] args)
        => Ui.LogCritical(ex, message, args);

    private static ILogger InitLogger(string name)
        => _loggerFactory.Value.CreateLogger(name);
}
