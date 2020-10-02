using DeviceApp.Models;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApp
{
    class Program
    {
        private static DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=ec-win20-ass3-iothub.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=dDkn3FG1NHiScF1rw5E5KWaxcv4zoxLtVgL89nA/u+A=", TransportType.Mqtt);

        static void Main(string[] args)
        {
            deviceClient.SetMethodHandlerAsync("SetTelemetryInterval", DeviceAppModel.SetTelemetryInterval, null).Wait();
            DeviceAppModel.SendMessageAsync(deviceClient).GetAwaiter();

            Console.ReadKey();
        }
    }
}
