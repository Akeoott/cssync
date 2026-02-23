// Copyright (c) 2025-2026 Ame (Akeoot/Akeoott) <akeoot@pm.me>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using cssync.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        // Dependency Injection
        // builder.Services.AddSingleton<INumberCounter, NumberCounter>();

        // Singleton = one instance for the lifetime of the application
        // builder.Services.AddSingleton<>();

        // Scoped = new instance each time
        // builder.Services.AddScoped<>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
