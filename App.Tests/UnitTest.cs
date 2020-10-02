using MAD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DeviceApp.Models;
using ServiceApp.Models;

namespace DeviceApp.Tests
{
    public class UnitTest
    {
        private static DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=ec-win20-ass3-iothub.azure-devices.net;DeviceId=DeviceApp;SharedAccessKey=dDkn3FG1NHiScF1rw5E5KWaxcv4zoxLtVgL89nA/u+A=", TransportType.Mqtt);
        private static MAD.ServiceClient serviceClient = MAD.ServiceClient.CreateFromConnectionString("HostName=ec-win20-ass3-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0EksTxvmcuxcsZ+6gC4GEFRl1/rEWr03SjGEtfShqSk=");

        // ----------------- DeviceApp tests -----------------

        [Theory]
        [InlineData("10", 200)]
        [InlineData("ten", 501)]
        public void SetTelemetryInterval_ShouldReturnCorrectStatus(string payload, int expectedStatusCode)
        {
            var response = DeviceAppModel.SetTelemetryInterval(new MethodRequest(payload, Encoding.UTF8.GetBytes(payload)), null).GetAwaiter().GetResult();
            Assert.Equal(expectedStatusCode, response.Status);
        }

        [Theory]
        [InlineData("10", 10)]
        [InlineData("ten", 5)]
        public void SetTelemetryInterval_ShouldChangeTelemetryInterval(string payload, int expectedTelemetryInterval)
        {
            DeviceAppModel.telemetryInterval = 5;
            DeviceAppModel.SetTelemetryInterval(new MethodRequest(payload, Encoding.UTF8.GetBytes(payload)), null).GetAwaiter();
            Assert.Equal(expectedTelemetryInterval, DeviceAppModel.telemetryInterval);
        }

        // ----------------- ServiceApp tests -----------------

        [Theory]
        [InlineData("SetTelemetryInterval", "10", 200)]
        [InlineData("SetInterval", "10", 501)]
        public void InvokeMethod_ShouldReturnCorrectStatus(string methodName, string payload, int expectedStatusCode)
        {
            deviceClient.SetMethodHandlerAsync("SetTelemetryInterval", DeviceAppModel.SetTelemetryInterval, null).Wait();
            var response = ServiceAppModel.InvokeMethod(serviceClient, "DeviceApp", methodName, payload).GetAwaiter();
            Assert.Equal(expectedStatusCode, response.GetResult().Status);
        }
    }
}
