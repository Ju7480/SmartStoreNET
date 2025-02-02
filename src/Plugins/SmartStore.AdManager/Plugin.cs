﻿using SmartStore.AdManager;
using SmartStore.AdManager.Settings;
using SmartStore.Core.Plugins;
using SmartStore.Services;
using SmartStore.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Web.Routing;




namespace SmartStore.AdManager
{
    public class Plugin : BasePlugin, IConfigurable
    {
        private readonly ISettingService _settingService;
        private readonly ICommonServices _services;


        public Plugin(ISettingService settingService,
            ICommonServices services)
        {
            _settingService = settingService;
            _services = services;

        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "AdManager";
            routeValues = new RouteValueDictionary() { { "area", "SmartStore.AdManager" } };
        }





        public override void Install()
        {
            // Save settings with default values.
            _services.Settings.SaveSetting(new AdManagerSettings());

            // Import localized plugin resources (you can edit or add these in /Localization/resources.[Culture].xml).
            _services.Localization.ImportPluginResourcesFromXml(this.PluginDescriptor);


            base.Install();
        }

        public override void Uninstall()
        {
            _services.Settings.DeleteSetting<AdManagerSettings>();
            _services.Localization.DeleteLocaleStringResources(this.PluginDescriptor.ResourceRootKey);


            base.Uninstall();
        }
    }
}
