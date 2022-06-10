using System.Threading.Tasks;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Themes.BootstrapBlazorTheme;

public class AbpLayout: BootstrapBlazor.Components.Layout
{
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        IsAuthenticated = true;
    }
}