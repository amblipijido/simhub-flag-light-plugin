using FlagEventEmitter.service;
using GameReaderCommon;
using SimHub.Plugins;
using System;
using System.Windows.Media;

namespace FlagEventEmitter
{
    [PluginDescription("This plugin send a HTTP request when a flag is activated to managage a govee device")]
    [PluginAuthor("Amblipijido")]
    [PluginName("Govee Flag emulation")]
    public class DataPluginDemo : IPlugin, IDataPlugin, IWPFSettingsV2
    {

        public DataPluginDemoSettings settings;
        private FlagService flagService = new FlagService();


        /// <summary>
        /// Instance of the current plugin manager
        /// </summary>
        public PluginManager PluginManager { get; set; }

        public ImageSource PictureIcon => throw new NotImplementedException();

        public string LeftMenuTitle => throw new NotImplementedException();

        /// <summary>
        /// Called one time per game data update, contains all normalized game data, 
        /// raw data are intentionnally "hidden" under a generic object type (A plugin SHOULD NOT USE IT)
        /// 
        /// This method is on the critical path, it must execute as fast as possible and avoid throwing any error
        /// 
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <param name="data"></param>
        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            if (data.GameRunning)
            {
                StatusDataBase oldData = data.OldData;
                StatusDataBase newData = data.NewData;
                if (oldData != null && newData != null)
                {
                    flagService.sendFlagRequestIfNeeded(oldData, newData);
                }
            }
        }

        /// <summary>
        /// Called at plugin manager stop, close/dispose anything needed here ! 
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void End(PluginManager pluginManager)
        {
            flagService.finish();
        }

        public void SaveGeneralSettings(DataPluginDemoSettings settings)
        {
            SimHub.Logging.Current.Info("Saving settings: " + settings.deviceName);
            this.SaveCommonSettings("GeneralSettings", settings);
        }

        /// <summary>
        /// Returns the settings control, return null if no settings control is required
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <returns></returns>
        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return new SettingsControlDemo(this, settings);
        }

        /// <summary>
        /// Called once after plugins startup
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting plugin FlagEventEmitter");
            // Load settings
            settings = this.ReadCommonSettings("GeneralSettings", () => new DataPluginDemoSettings("DEVICE_NAME", "GOVEE_API_KEY", client.DeviceType.GOVEE));
            SimHub.Logging.Current.Info("Device to be managed: " + settings.deviceName);
            flagService.init(settings);
        }
    }
}