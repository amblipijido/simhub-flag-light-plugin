using FlagEventEmitter.client;
using FlagEventEmitter.service.data;
using GameReaderCommon;
using System;

namespace FlagEventEmitter.service
{
    public class FlagService
    {
        private DeviceClient deviceClient;
        private ClientFactory clientFactory;

        public void init(DataPluginDemoSettings settings)
        {
            clientFactory = new ClientFactory(settings);
            deviceClient = clientFactory.generateClient();
            deviceClient.init(settings);
        }

        public  void sendFlagRequestIfNeeded(StatusDataBase oldData, StatusDataBase newData)
        {
            bool blueFlagChanged = newData.Flag_Blue != oldData.Flag_Blue;
            bool yellowFlagChanged = newData.Flag_Yellow != oldData.Flag_Yellow;
            bool greenFlagChanged = newData.Flag_Green != oldData.Flag_Green;
            bool orangeFlagChanged = newData.Flag_Orange != oldData.Flag_Orange;

            if (blueFlagChanged || yellowFlagChanged || greenFlagChanged || orangeFlagChanged)
            {
                bool blueFlagState = Convert.ToBoolean(newData.Flag_Blue);
                bool yellowFlagState = Convert.ToBoolean(newData.Flag_Yellow);
                bool greenFlagState = Convert.ToBoolean(newData.Flag_Green);
                bool orangeFlagState = Convert.ToBoolean(newData.Flag_Orange);

                if(!blueFlagState && !yellowFlagState && !greenFlagState && !orangeFlagState)
                {
                    SimHub.Logging.Current.Info("Turning off Flag!!");
                    deviceClient.changeDeviceColor(new RgbValue(255, 255, 255));
                } 
                else
                {
                    SimHub.Logging.Current.Info("Flag detected!!");
                    if (blueFlagState)
                    {
                        deviceClient.changeDeviceColor(new RgbValue(0, 0, 255));
                    } 
                    else if(yellowFlagState)
                    {
                        deviceClient.changeDeviceColor(new RgbValue(255, 255, 0));
                    }
                    else if(greenFlagState)
                    {
                        deviceClient.changeDeviceColor(new RgbValue(0, 255, 0));
                    }
                    else
                    {
                        deviceClient.changeDeviceColor(new RgbValue(255, 165, 0));
                    }
                }
            }
        }

        public void finish()
        {
            deviceClient.switchOff();
        }
    }
}
