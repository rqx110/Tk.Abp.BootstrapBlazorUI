using System.Collections.Generic;
using System.Linq;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace Tk.Abp.BootstrapBlazorUI.Components.ObjectExtending;

public abstract class ExtensionPropertyComponentBase<TEntity, TResourceType> : OwningComponentBase
    where TEntity : IHasExtraProperties
{
    [Inject] public IStringLocalizerFactory StringLocalizerFactory { get; set; }

    [Parameter] public TEntity Entity { get; set; }

    [Parameter] public ObjectExtensionPropertyInfo PropertyInfo { get; set; }

    [Parameter] public AbpBlazorMessageLocalizerHelper<TResourceType> LH { get; set; }

    protected virtual List<IValidator> CustomerRules =>
        PropertyInfo.GetValidationAttributes()
            .Select(attribute => new FormItemValidator(attribute)).Cast<IValidator>().ToList();
}