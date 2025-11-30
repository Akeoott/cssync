// Copyright (c) Ame (Akeoott) <ame@akeoot.org>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.


using Microsoft.Extensions.Logging;

namespace cssync.Backend;

internal static class Globals
{
    public static readonly ILogger logger = InitLogger();

    private static ILogger InitLogger()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                options.IncludeScopes = true;
            });
        });

        ILogger logger = factory.CreateLogger("cssync.Backend");
        return logger;
    }
}
