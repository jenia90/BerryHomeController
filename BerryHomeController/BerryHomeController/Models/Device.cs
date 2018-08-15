using System;
using System.Collections.Generic;

namespace BerryHomeController.Common.Models
{
    public enum DeviceType
    {
        Input = 0,
        Output = 1,
        PWM = 2
    }
    public class Device
    {
        public Guid Id { get; set; }
        public DeviceType Type { get; set; }
        public string DeviceName { get; set; }
        public int DevicePin { get; set; }
        public bool State { get; set; }
        public IEnumerable<Job> Jobs { get; set; }


        public static void Map(this Device dbDevice, Device device)
        {
            dbDevice.DeviceName = device.DeviceName;
            dbDevice.DevicePin = device.DevicePin;
            dbDevice.Jobs = device.Jobs;
            dbDevice.State = device.State;
            dbDevice.Type = device.Type;
        }
    }
}
