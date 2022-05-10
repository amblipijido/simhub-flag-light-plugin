using System.Windows.Controls;

namespace FlagEventEmitter
{
    /// <summary>
    /// Logique d'interaction pour SettingsControlDemo.xaml
    /// </summary>
    public partial class SettingsControlDemo : UserControl
    {
        public DataPluginDemo Plugin { get; }
        public DataPluginDemoSettings Settings { get; }

        public SettingsControlDemo()
        {
            InitializeComponent();
        }

        public SettingsControlDemo(DataPluginDemo plugin, DataPluginDemoSettings dataPluginDemoSettings) : this()
        {
            this.Plugin = plugin;
            this.Settings = dataPluginDemoSettings;
            this.DeviceName.Text = dataPluginDemoSettings.deviceName;
            this.GoveeApiKey.Password = dataPluginDemoSettings.goveeApiKey;
        }

        private void SHButtonPrimary_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Settings.deviceName =  this.DeviceName.Text;
            this.Settings.goveeApiKey = this.GoveeApiKey.Password;
            this.Plugin.SaveGeneralSettings(this.Settings);
        }

    }
}
