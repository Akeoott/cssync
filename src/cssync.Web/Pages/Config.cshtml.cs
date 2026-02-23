// Copyright (c) 2025-2026 Ame (Akeoot/Akeoott) <akeoot@pm.me>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cssync.Web.Pages;

public class ConfigModel(ILogger<ConfigModel> logger) : PageModel
{
    private readonly ILogger<ConfigModel> _logger = logger;

    public void OnGet()
    {

    }
}
