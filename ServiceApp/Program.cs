using Microsoft.Azure.Devices;
using ServiceApp.Models;
using System;
using System.Threading.Tasks;

namespace ServiceApp
{
    class Program
    {
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=ec-win20-ass3-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0EksTxvmcuxcsZ+6gC4GEFRl1/rEWr03SjGEtfShqSk=");

        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();
            ServiceAppModel.InvokeMethod(serviceClient, "DeviceApp", "SetTelemetryInterval", "10").GetAwaiter();

            Console.ReadKey();
        }
    }
}
