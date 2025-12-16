// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using System.Runtime.CompilerServices;
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
            });
        })
    );

    public static void Debug(string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Debug, null, message, [], member, file, line);

    public static void Debug(string message, params object[] args)
        => Write(LogLevel.Debug, null, message, args);

    public static void Info(string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Information, null, message, [], member, file, line);

    public static void Info(string message, params object[] args)
        => Write(LogLevel.Information, null, message, args);

    public static void Warn(string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Warning, null, message, [], member, file, line);

    public static void Warn(string message, params object[] args)
        => Write(LogLevel.Warning, null, message, args);

    public static void Error(string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Error, null, message, [], member, file, line);

    public static void Error(string message, params object[] args)
        => Write(LogLevel.Error, null, message, args);

    public static void Error(Exception ex, string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Error, ex, message, [], member, file, line);

    public static void Error(Exception ex, string message, params object[] args)
        => Write(LogLevel.Error, ex, message, args);

    public static void Critical(string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Critical, null, message, [], member, file, line);

    public static void Critical(string message, params object[] args)
        => Write(LogLevel.Critical, null, message, args);

    public static void Critical(Exception ex, string message,
        [CallerMemberName] string member = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
        => Write(LogLevel.Critical, ex, message, [], member, file, line);

    public static void Critical(Exception ex, string message, params object[] args)
        => Write(LogLevel.Critical, ex, message, args);

    private static void Write(
        LogLevel level,
        Exception? exception,
        string message,
        object[] args,
        string member = "",
        string file = "",
        int line = 0)
    {
        var fileName = Path.GetFileName(file);
        var className = Path.GetFileNameWithoutExtension(file);
        var logger = _loggerFactory.Value.CreateLogger(className);
        var prefix = $"[{className}.{member} ({fileName}:{line})] ";

        logger.Log(
            level,
            exception,
            prefix + message,
            args
        );
    }
}
