using System;

namespace az220_connect
{
    /// <summary>
    /// This class represents a sensor 
    /// real-world sensors would contain code to initialize
    /// the device or devices and maintain internal state
    /// a real-world example can be found here: https://bit.ly/IoT-BME280
    /// </summary>
    internal class EnvironmentSensor
    {
        // Initial telemetry values
        double minTemperature = 20;
        double minHumidity = 60;
        string geoloc = "somewhere";
        Random rand = new Random();

        internal EnvironmentSensor()
        {
            // device initialization could occur here
        }

        internal double ReadTemperature()
        {
            return minTemperature + rand.NextDouble() * 15;
        }

        internal double ReadHumidity()
        {
            return minHumidity + rand.NextDouble() * 20;
        }  

        internal string ReadLocation()
        {
            return geoloc + " - " + rand.NextDouble();
        }  
    }

    internal class location
    {
        string loc = "";
        internal string readLocation()
        {
            return loc;
       }
    }
}
