using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Settings;
using Volo.Abp.UI.Navigation;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Themes.BootstrapBlazorTheme;

public partial class DefaultLayout
{
    [Inject] protected IBootstrapBlazorSettingsProvider BootstrapBlazorSettingsProvider { get; set; }

    [Inject] protected IMenuManager MenuManager { get; set; }

    private string LayoutClassString => CssBuilder.Default("h-auto")
        .AddClass("has-top-menu", MenuPlacement == MenuPlacement.Top)
        .Build();

    private IEnumerable<MenuItem> MenuItems { get; set; }

    private bool Collapsed { get; set; }

    private BootstrapBlazorThemes Theme { get; set; }
    private MenuPlacement MenuPlacement { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetMenuAsync();
        await SetLayout();
        BootstrapBlazorSettingsProvider.SettingChanged += OnSettingChanged;
    }

    protected virtual async Task OnSettingChanged()
    {
        await GetMenuAsync();
        await SetLayout();
        await InvokeAsync(StateHasChanged);
    }

    private async Task GetMenuAsync()
    {
        MenuItem InFunc(ApplicationMenuItem menuItem)
        {
            var menu = new MenuItem
            {
                Text = menuItem.DisplayName,
                Icon = menuItem.Icon,
                Url = menuItem.Url == null ? "#" : menuItem.Url.TrimStart('~'),
                Target = menuItem.Target,
            };
            menu.Items = menuItem.Items.Select(InFunc).ToList();

            return menu;
        }

        var mainMenu = await MenuManager.GetMainMenuAsync();
        MenuItems = mainMenu.Items.Select(InFunc).ToList();
    }

    private async Task SetLayout()
    {
        Theme = await BootstrapBlazorSettingsProvider.GetThemeAsync();
        MenuPlacement = await BootstrapBlazorSettingsProvider.GetMenuPlacementAsync();
    }

    private Task OnCollapse(bool collapsed)
    {
        Collapsed = collapsed;
        return Task.CompletedTask;
    }
}