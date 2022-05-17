using System;
using FlagEventEmitter.service.data;

namespace FlagEventEmitter.client
{
    internal interface DeviceClient
    {
        /// <summary>
        /// Used to initialize the client
        /// </summary>
        /// <param name="settings">Complete settings object</param>
        void init(DataPluginDemoSettings settings);
        /// <summary>
        /// USed to change the device color
        /// </summary>
        /// <param name="value">RGB value to set the device color</param>
        void changeDeviceColor(RgbValue value);
        /// <summary>
        /// Turn off the device
        /// </summary>
        void switchOff();
    }
}
