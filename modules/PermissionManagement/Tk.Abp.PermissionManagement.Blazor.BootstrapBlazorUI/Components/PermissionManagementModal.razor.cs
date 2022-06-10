using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Configuration;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Localization;

namespace Tk.Abp.PermissionManagement.Blazor.BootstrapBlazorUI.Components;

public partial class PermissionManagementModal
{
    [Inject]
    protected IPermissionAppService PermissionAppService { get; set; }
    
    [Inject]
    protected ICurrentApplicationConfigurationCacheResetService CurrentApplicationConfigurationCacheResetService { get; set; }

    private Modal Modal { get; set; }
    
    private string _providerName;
    private string _providerKey;

    private string _entityDisplayName;
    private List<PermissionGroupDto> _groups;
    private List<PermissionGrantInfoDto> _disabledPermissions = new();

    protected int _grantedPermissionCount = 0;
    protected int _notGrantedPermissionCount = 0;

    protected bool GrantAll {
        get {
            if (_notGrantedPermissionCount == 0)
            {
                return true;
            }

            return false;
        }
        set {
            if (_groups == null)
            {
                return;
            }

            _grantedPermissionCount = 0;
            _notGrantedPermissionCount = 0;

            foreach (var permission in _groups.SelectMany(x => x.Permissions))
            {
                if (!IsDisabledPermission(permission))
                {
                    permission.IsGranted = value;

                    if (value)
                    {
                        _grantedPermissionCount++;
                    }
                    else
                    {
                        _notGrantedPermissionCount++;
                    }
                }
            }
        }
    }
    
    public PermissionManagementModal()
    {
        LocalizationResource = typeof(AbpPermissionManagementResource);
    }
    
    private async Task<bool> SaveAsync()
    {
        try
        {
            var updateDto = new UpdatePermissionsDto
            {
                Permissions = _groups
                    .SelectMany(g => g.Permissions)
                    .Select(p => new UpdatePermissionDto { IsGranted = p.IsGranted, Name = p.Name })
                    .ToArray()
            };
            
            if (!updateDto.Permissions.Any(x => x.IsGranted))
            {
                if (!await Message.Confirm(L["RemoveAllPermissionsWarningMessage"].Value))
                {
                    return false;
                }
            }

            await PermissionAppService.UpdateAsync(_providerName, _providerKey, updateDto);

            await CurrentApplicationConfigurationCacheResetService.ResetAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return true;
    }

    private async Task ClosingModal()
    {
        await Modal.Close();
    } 
    
    public virtual async Task OpenAsync(string providerName, string providerKey, string entityDisplayName = null)
    {
        try
        {
            _providerName = providerName;
            _providerKey = providerKey;

            var result = await PermissionAppService.GetAsync(_providerName, _providerKey);

            _entityDisplayName = entityDisplayName ?? result.EntityDisplayName;
            _groups = result.Groups;

            _grantedPermissionCount = 0;
            _notGrantedPermissionCount = 0;
            foreach (var permission in _groups.SelectMany(x => x.Permissions))
            {
                if (permission.IsGranted && permission.GrantedProviders.All(x => x.ProviderName != _providerName))
                {
                    _disabledPermissions.Add(permission);
                    continue;
                }

                if (permission.IsGranted)
                {
                    _grantedPermissionCount++;
                }
                else
                {
                    _notGrantedPermissionCount++;
                }
            }

            await InvokeAsync(async () =>
            {
                StateHasChanged();
                await Modal.Show();
            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
    
    private string GetNormalizedGroupName(string name)
    {
        return "PermissionGroup_" + name.Replace(".", "_");
    }
    
    private Task GrantAllChanged(CheckboxState state, bool value)
    {
        foreach (var groupDto in _groups)
        {
            foreach (var permission in groupDto.Permissions)
            {
                if (!IsDisabledPermission(permission))
                {
                    SetPermissionGrant(permission, value);
                }
            }
        }
        return Task.CompletedTask;
    }
    
    private Task GroupGrantAllChanged(bool value, PermissionGroupDto permissionGroup)
    {
        foreach (var permission in permissionGroup.Permissions)
        {
            if (!IsDisabledPermission(permission))
            {
                SetPermissionGrant(permission, value);
            }
        }
        return Task.CompletedTask;
    }
    
    private bool IsDisabledPermission(PermissionGrantInfoDto permissionGrantInfo)
    {
        return _disabledPermissions.Any(x => x == permissionGrantInfo);
    }
    
    private Task PermissionChanged(bool value, PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        SetPermissionGrant(permission, value);

         if (value && permission.ParentName != null)
         {
             var parentPermission = GetParentPermission(permissionGroup, permission);
        
             SetPermissionGrant(parentPermission, true);
         }
         else if (value == false)
         {
             var childPermissions = GetChildPermissions(permissionGroup, permission);
        
             foreach (var childPermission in childPermissions)
             {
                 SetPermissionGrant(childPermission, false);
             }
         }
         return Task.CompletedTask;
    }

    private void SetPermissionGrant(PermissionGrantInfoDto permission, bool value)
    {
        if (permission.IsGranted == value)
        {
            return;
        }
        
        if (value)
        {
            _grantedPermissionCount++;
            _notGrantedPermissionCount--;
        }
        else
        {
            _grantedPermissionCount--;
            _notGrantedPermissionCount++;
        }

        permission.IsGranted = value;
    }
    
    private PermissionGrantInfoDto GetParentPermission(PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        return permissionGroup.Permissions.First(x => x.Name == permission.ParentName);
    }

    private List<PermissionGrantInfoDto> GetChildPermissions(PermissionGroupDto permissionGroup, PermissionGrantInfoDto permission)
    {
        return permissionGroup.Permissions.Where(x => x.Name.StartsWith(permission.Name)).ToList();
    }
    
    private string GetShownName(PermissionGrantInfoDto permissionGrantInfo)
    {
        if (!IsDisabledPermission(permissionGrantInfo))
        {
            return permissionGrantInfo.DisplayName;
        }

        return string.Format(
            "{0} ({1})",
            permissionGrantInfo.DisplayName,
            permissionGrantInfo.GrantedProviders
                .Where(p => p.ProviderName != _providerName)
                .Select(p => p.ProviderName)
                .JoinAsString(", ")
        );
    }
}
