using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme.Bundling;

public class BlazorGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/BootstrapBlazor.FontAwesome/css/font-awesome.min.css");
        context.Files.AddIfNotContains("/_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
    }
}
