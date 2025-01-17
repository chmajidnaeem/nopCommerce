﻿using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Misc.OmnibusDirective;

/// <summary>
/// Represents EU Omnibus Directive plugin
/// </summary>
public class OmnibusDirectivePlugin : BasePlugin, IAdminMenuPlugin, IMiscPlugin
{
    #region Fields

    private readonly IPermissionService _permissionService;
    private readonly IWebHelper _webHelper;

    #endregion

    #region Ctor

    public OmnibusDirectivePlugin(IPermissionService permissionService,
        IWebHelper webHelper)
    {
        _permissionService = permissionService;
        _webHelper = webHelper;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets a configuration page URL
    /// </summary>
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/OmnibusDirective/Configure";
    }

    public async Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_PLUGINS))
            return;

        var config = rootNode.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Configuration"));
        if (config == null)
            return;

        var plugins = config.ChildNodes.FirstOrDefault(node => node.SystemName.Equals("Local plugins"));

        if (plugins == null)
            return;

        var index = config.ChildNodes.IndexOf(plugins);

        if (index < 0)
            return;

        config.ChildNodes.Insert(index, new SiteMapNode
        {
            SystemName = "EU Omnibus Directive",
            Title = "Omnibus Directive plugin",
            ControllerName = "OmnibusDirective",
            ActionName = "Configure",
            IconClass = "far fa-dot-circle",
            Visible = true,
            RouteValues = new RouteValueDictionary { { "area", AreaNames.ADMIN } }
        });
    }

    /// <summary>
    /// Install the plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task InstallAsync()
    {
        await base.InstallAsync();
    }

    /// <summary>
    /// Uninstall the plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }

    #endregion
}
