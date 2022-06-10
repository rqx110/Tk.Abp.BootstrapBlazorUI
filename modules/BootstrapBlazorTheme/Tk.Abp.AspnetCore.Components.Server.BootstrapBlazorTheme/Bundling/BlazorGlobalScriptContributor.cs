using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme.Bundling;

public class BlazorGlobalScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_framework/blazor.server.js");
        context.Files.AddIfNotContains("/_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
        context.Files.AddIfNotContains("/_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");
    }
}
