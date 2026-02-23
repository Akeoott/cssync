// Copyright (c) 2025-2026 Ame (Akeoot/Akeoott) <akeoot@pm.me>. Licensed under the GPLv3 License.
// See the LICENSE file in the repository root for full license text.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cssync.Web.Pages;

public class PrivacyModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

