using Volo.Abp.Bundling;

namespace Tk.Abp.AspnetCore.Components.WebAssembly.BootstrapBlazorTheme;

public class ComponentsComponentsBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
        context.Add("_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js");
        context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/abp.js");
        context.Add("_content/Volo.Abp.AspNetCore.Components.Web/libs/abp/js/lang-utils.js");
        context.Add("_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js");
    }

    public void AddStyles(BundleContext context)
    {
        context.Add("_content/BootstrapBlazor.FontAwesome/css/font-awesome.min.css");
        context.Add("_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css");
        context.Add("_content/Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme/libs/abp/css/theme.css");
    }
}
