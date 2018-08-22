using System;
using System.Collections.Generic;
using System.Text;
using BerryHomeController.Common.Models;

namespace BerryHomeController.Common.Extensions
{
    public static class DeviceExtensions
    {
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
