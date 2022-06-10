using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme.Bundling;

public class BlazorBootstrapBlazorThemeStyleContributor: BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme/libs/abp/css/theme.css");
    }
}
