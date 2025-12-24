// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.Extensions.Logging;

namespace cssync.Backend.helper;

public static class Log
{
    private static readonly Lazy<ILoggerFactory> _loggerFactory = new(() =>
        LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(options =>
            {
                options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                options.IncludeScopes = false;
                options.SingleLine = true;
            }); builder.SetMinimumLevel(LogLevel.Information);
        })
    );

    public static void Debug(string message)
        => Write(LogLevel.Debug, null, message, []);

    public static void Debug(string message, params object[] args)
        => Write(LogLevel.Debug, null, message, args);

    public static void Info(string message)
        => Write(LogLevel.Information, null, message, []);

    public static void Info(string message, params object[] args)
        => Write(LogLevel.Information, null, message, args);

    public static void Warn(string message)
        => Write(LogLevel.Warning, null, message, []);

    public static void Warn(string message, params object[] args)
        => Write(LogLevel.Warning, null, message, args);

    public static void Error(string message)
        => Write(LogLevel.Error, null, message, []);

    public static void Error(string message, params object[] args)
        => Write(LogLevel.Error, null, message, args);

    public static void Error(Exception ex, string message)
        => Write(LogLevel.Error, ex, message, []);

    public static void Error(Exception ex, string message, params object[] args)
        => Write(LogLevel.Error, ex, message, args);

    public static void Critical(string message)
        => Write(LogLevel.Critical, null, message, []);

    public static void Critical(string message, params object[] args)
        => Write(LogLevel.Critical, null, message, args);

    public static void Critical(Exception ex, string message)
        => Write(LogLevel.Critical, ex, message, []);

    public static void Critical(Exception ex, string message, params object[] args)
        => Write(LogLevel.Critical, ex, message, args);

    private static void Write(
        LogLevel level,
        Exception? exception,
        string message,
        object[] args)
    {
        var logger = _loggerFactory.Value.CreateLogger(typeof(Log).FullName ?? "Log");

        logger.Log(
            level,
            exception,
            message,
            args
        );
    }
}
