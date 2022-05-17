using FlagEventEmitter.client;
using FlagEventEmitter.client.govee.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FlagEventEmitter
{
    public class GoveeClient : DeviceClient
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BASE_PATH = "https://developer-api.govee.com/v1";
        private const string DEVICES_PATH = BASE_PATH + "/devices";
        private const string CONTROL_PATH = DEVICES_PATH + "/control";
        private string goveeApiKey;
        private Device device;

        public void init(DataPluginDemoSettings settings)
        {
            client.DefaultRequestHeaders.Add("Govee-API-Key", this.goveeApiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.device = findDeviceByName(settings.deviceName);
            this.goveeApiKey = settings.goveeApiKey; 
            if(device != null)
            {
                changeDeviceColor(new service.data.RgbValue(255, 255, 255));
            } else
            {
                SimHub.Logging.Current.Warn("Device not found!!");
            }
        }

        public void changeDeviceColor(service.data.RgbValue value)
        {
            RgbValue goveeRgbValue = new RgbValue(value.r, value.g, value.b);
            GoveeComand comand = new GoveeRgbComand("color", goveeRgbValue);
            string jsonRequest = createCommandAndParseToJson(device.device, device.model, comand);
            asynchronousPut(CONTROL_PATH, jsonRequest);
        }

        public void switchOff()
        {
            GoveeComand comand = new GoveeSwithcComand("turn", "off");
            string jsonRequest = createCommandAndParseToJson(device.device, device.model, comand);
            asynchronousPut(CONTROL_PATH, jsonRequest);
        }

        private Device findDeviceByName(string name)
        {
            Device deviceToBeManaged = null;
            HttpResponseMessage httpResponse = synchronousGet(DEVICES_PATH);
            string result = httpResponse.Content.ReadAsStringAsync().Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                
                GoveeApiDevicesResponse goveeResponse = JsonConvert.DeserializeObject<GoveeApiDevicesResponse>(result);
                if (goveeResponse.code == 200)
                {
                    List<Device> devices = goveeResponse.data.devices;
                    devices.Where(device => device.deviceName.Equals(name)).ToList();
                    if (devices.Count > 0)
                    {
                        deviceToBeManaged =  devices[0];
                    }
                }
            } 
            return deviceToBeManaged;
        }

        private string createCommandAndParseToJson(string device, string model, GoveeComand comand)
        {
            GoveeRgbLedStripColorRequest request = new GoveeRgbLedStripColorRequest(device, model, comand);

            return JsonConvert.SerializeObject(request);
        }

        private HttpResponseMessage synchronousGet(string path)
        {
            return client.GetAsync(path).Result;
        }

        private async void asynchronousPut(string path, string body)
        {
            StringContent content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync(path, content);
        }

    }
}
