using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;


namespace az220_connect
{
    class Program
    {
        private static DeviceClient deviceClient;

        // The device connection string to authenticate the device with your IoT hub.
        // Note: in real-world applications you would not "hard-code" the connection string
        // It could be stored within an environment variable, passed in via the command-line or
        // stored securely within a TPM module.
        private readonly static string connectionString = "HostName=alwayson.azure-devices.net;DeviceId=az220-connect;SharedAccessKey=H42kn43Bpi/LVnTxSEpqW3SS7T3a5Yd65dliir+oVnQ=";
        private static void Main(string[] args)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }
        private static async void SendDeviceToCloudMessagesAsync()
        {
            // Create an instance of our sensor
            var sensor = new EnvironmentSensor();
            while (true)
            {
                // read data from the sensor
                var currentTemperature = sensor.ReadTemperature();
                var currentHumidity = sensor.ReadHumidity();
                var currentloc = sensor.ReadLocation();

                var telemetryDataPoint = new
                {
                    Temperature = currentTemperature,
                    Humidity = currentHumidity,
                    geoloc = currentloc
                };

                // Create a JSON string from the anonymous object

                // create a byte array from the message string using ASCII encoding
                var message = new Message(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(telemetryDataPoint)));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                // Send the telemetry message
                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, JsonConvert.SerializeObject(telemetryDataPoint));

                await Task.Delay(1000);
            }
        }
    }
}
