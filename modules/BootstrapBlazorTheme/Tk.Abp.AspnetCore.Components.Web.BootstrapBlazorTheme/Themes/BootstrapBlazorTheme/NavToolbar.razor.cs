using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Toolbars;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Themes.BootstrapBlazorTheme;

public partial class NavToolbar: IDisposable
{
    [Inject]
    private IToolbarManager ToolbarManager { get; set; }

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private List<RenderFragment> ToolbarItemRenders { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await GetToolbarItemRendersAsync();
        AuthenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProviderOnAuthenticationStateChanged;
    }

    private async Task GetToolbarItemRendersAsync()
    {
        var toolbar = await ToolbarManager.GetAsync(StandardToolbars.Main);

        ToolbarItemRenders.Clear();

        var sequence = 0;
        foreach (var item in toolbar.Items)
        {
            ToolbarItemRenders.Add(builder =>
            {
                builder.OpenComponent(sequence++, item.ComponentType);
                builder.CloseComponent();
            });
        }
    }

    private async void AuthenticationStateProviderOnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        await GetToolbarItemRendersAsync();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        AuthenticationStateProvider.AuthenticationStateChanged -= AuthenticationStateProviderOnAuthenticationStateChanged;
    }
}
