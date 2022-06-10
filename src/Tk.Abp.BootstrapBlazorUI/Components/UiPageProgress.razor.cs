using System;
using System.Collections.Generic;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Progression;

namespace Tk.Abp.BootstrapBlazorUI.Components;

public partial class UiPageProgress : ComponentBase
{
    private int _percent;

    private Color _progressStatus;

    private string _css = "uiPageProgress";

    private Dictionary<string, string> _gradients = new()
    {
        { "0%", "#108ee9" },
        { "100%", "#87d068" }
    };

    [Inject] public IUiPageProgressService UiPageProgressService { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        UiPageProgressService.ProgressChanged += OnProgressChanged;
    }

    protected virtual async void OnProgressChanged(object sender, UiPageProgressEventArgs e)
    {
        _percent = e.Percentage ?? new Random().Next(0, 100);
        
        SetProgressStatus(e.Options.Type);
        if (_percent is >= 0 and <= 100)
        {
            ShowProgress();
        }
        else
        {
            HideProgress();
        }

        await InvokeAsync(StateHasChanged);
    }

    protected virtual void ShowProgress()
    {
        _css = "uiPageProgress uiPageProgress-show";
    }

    protected virtual void HideProgress()
    {
        _css = "uiPageProgress";
    }

    protected virtual void SetProgressStatus(UiPageProgressType pageProgressType)
    {
        _progressStatus = pageProgressType switch
        {
            UiPageProgressType.Info => Color.Active,
            UiPageProgressType.Default => Color.Active,
            UiPageProgressType.Success => Color.Success,
            UiPageProgressType.Warning => Color.Warning,
            UiPageProgressType.Error => Color.Danger,
            _ => _progressStatus
        };
    }
}
