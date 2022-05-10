using FlagEventEmitter;
using GameReaderCommon;
using Newtonsoft.Json;
using System;

namespace FlagEventEmitter
{
    public class FlagService
    {
        private GoveeClient goveeClient;
        private Device device;
        private string goveeApiKey;

        public void init(string deviceName, string goveeApiKey)
        {
            this.goveeApiKey = goveeApiKey; 
            goveeClient = new GoveeClient(this.goveeApiKey);
            device = goveeClient.findDeviceByName(deviceName);
            if(device != null)
            {
                goveeClient.changeLedStripColor(device, new RgbValue(255, 255, 255));
            } else
            {
                SimHub.Logging.Current.Warn("Device not found!!");
            }
        }

        public  void sendFlagRequestIfNeeded(StatusDataBase oldData, StatusDataBase newData)
        {
            bool blueFlagChanged = newData.Flag_Blue != oldData.Flag_Blue;
            bool yellowFlagChanged = newData.Flag_Yellow != oldData.Flag_Yellow;
            bool greenFlagChanged = newData.Flag_Green != oldData.Flag_Green;
            bool orangeFlagChanged = newData.Flag_Orange != oldData.Flag_Orange;

            if ((blueFlagChanged || yellowFlagChanged || greenFlagChanged || orangeFlagChanged) && device != null)
            {
                bool blueFlagState = Convert.ToBoolean(newData.Flag_Blue);
                bool yellowFlagState = Convert.ToBoolean(newData.Flag_Yellow);
                bool greenFlagState = Convert.ToBoolean(newData.Flag_Green);
                bool orangeFlagState = Convert.ToBoolean(newData.Flag_Orange);

                if(!blueFlagState && !yellowFlagState && !greenFlagState && !orangeFlagState)
                {
                    SimHub.Logging.Current.Info("Turning off Flag!!");
                    goveeClient.changeLedStripColor(device, new RgbValue(255, 255, 255));
                } 
                else
                {
                    SimHub.Logging.Current.Info("Flag detected!!");
                    if (blueFlagState)
                    {
                        goveeClient.changeLedStripColor(device, new RgbValue(0, 0, 255));
                    } 
                    else if(yellowFlagState)
                    {
                        goveeClient.changeLedStripColor(device, new RgbValue(255, 255, 0));
                    }
                    else if(greenFlagState)
                    {
                        goveeClient.changeLedStripColor(device, new RgbValue(0, 255, 0));
                    }
                    else
                    {
                        goveeClient.changeLedStripColor(device, new RgbValue(255, 165, 0));
                    }
                }
            }
        }

        public void finish()
        {
            if(device != null)
            {
                SimHub.Logging.Current.Info("Turning of device: " + device.deviceName);
                goveeClient.switchOff(device);
            }
        }
    }
}
