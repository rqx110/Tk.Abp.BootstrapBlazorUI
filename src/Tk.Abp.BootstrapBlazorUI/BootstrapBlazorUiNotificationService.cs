using System;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;

namespace Tk.Abp.BootstrapBlazorUI;

[Dependency(ReplaceServices = true)]
public class BootstrapBlazorUiNotificationService: IUiNotificationService, IScopedDependency
{
    [Inject]
    public ToastService NoticeService { get; set; }

    private readonly IStringLocalizer<AbpUiResource> _localizer;

    public BootstrapBlazorUiNotificationService(IStringLocalizer<AbpUiResource> localizer)
    {
        _localizer = localizer;
    }

    public async Task Info(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Info"], message, ToastCategory.Information);
    }

    public async Task Success(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Success"], message, ToastCategory.Success);
    }

    public async Task Warn(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Warn"], message, ToastCategory.Warning);
    }

    public async Task Error(string message, string title = null, Action<UiNotificationOptions> options = null)
    {
        var uiNotificationOptions = CreateDefaultOptions();
        options?.Invoke(uiNotificationOptions);

        await Notify(title ?? _localizer["Error"], message, ToastCategory.Error);
    }

    protected virtual async Task Notify(string title, string message, ToastCategory notificationType)
    {
        await NoticeService.Show(new ToastOption
        {
            Title = title,
            Content = message,
            Category = notificationType
        });
    }

    protected virtual UiNotificationOptions CreateDefaultOptions()
    {
        return new UiNotificationOptions();
    }
}
