using System;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.DependencyInjection;

namespace Tk.Abp.BootstrapBlazorUI;

[Dependency(ReplaceServices = true)]
public class BootstrapBlazorUiMessageService : IUiMessageService , IScopedDependency
{
    [Inject]
    public SwalService ModalService { get; set; }
    
    private readonly IStringLocalizer<AbpUiResource> _localizer;

    public BootstrapBlazorUiMessageService(IStringLocalizer<AbpUiResource> localizer)
    {
        _localizer = localizer;
    }

    public async Task Info(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        await ModalService.Show(CreateConfirmOptions(message, title ?? _localizer["Info"], SwalCategory.Information));
    }

    public async Task Success(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        await ModalService.Show(CreateConfirmOptions(message, title ?? _localizer["Success"], SwalCategory.Success));
    }

    public async Task Warn(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        await ModalService.Show(CreateConfirmOptions(message, title ?? _localizer["Warn"], SwalCategory.Warning));
    }

    public async Task Error(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        await ModalService.Show(CreateConfirmOptions(message, title ?? _localizer["Error"], SwalCategory.Error));
    }

    public async Task<bool> Confirm(string message, string title = null, Action<UiMessageOptions> options = null)
    {
        return await ModalService.ShowModal(CreateConfirmOptions(message, title ?? _localizer["Confirm"], SwalCategory.Question));
    }

    protected virtual SwalOption CreateConfirmOptions(string message, string title, SwalCategory swalCategory)
    {
        var options = new SwalOption()
        {
            Category = swalCategory,
            Title = title,
            Content = message,
            ShowClose = true
        };
        return options;
    }
}
