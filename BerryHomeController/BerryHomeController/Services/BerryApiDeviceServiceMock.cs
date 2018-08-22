using BerryHomeController.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerryHomeController.Common.Extensions;

namespace BerryHomeController.Common.Services
{
    public class BerryApiDeviceServiceMock : IBerryApiService<Device>
    {
        private readonly List<Device> _devices;

        public BerryApiDeviceServiceMock()
        {
            _devices = new List<Device>
            {
                new Device() { DeviceName = "TestDevice1", DevicePin = 0, Id = new Guid(), Type = DeviceType.Input, State = false},
                new Device() { DeviceName = "TestDevice2", DevicePin = 1, Id = new Guid(), Type = DeviceType.Output, State = false},
                new Device() { DeviceName = "TestDevice3", DevicePin = 2, Id = new Guid(), Type = DeviceType.Output, State = true},
                new Device() { DeviceName = "TestDevice4", DevicePin = 3, Id = new Guid(), Type = DeviceType.Input, State = true},
                new Device() { DeviceName = "TestDevice5", DevicePin = 4, Id = new Guid(), Type = DeviceType.Input, State = false}
            };
        }

        public async Task<List<Device>> GetAsync()
        {
            return _devices;
        }

        public async Task<Device> GetAsyncById(Guid id)
        {
            return _devices.FirstOrDefault(d => d.Id == id);
        }

        public async Task<Device> PostAsync(Device data)
        {
            data.Id = new Guid();
            _devices.Add(data);
            return data;
        }

        public async Task PutAsync(Guid id, Device data)
        {
            var dbDevice = _devices.Find(d => d.Id == id);
            _devices.Remove(dbDevice);
            dbDevice.Map(data);
            _devices.Add(dbDevice);
        }

        public async Task DeleteAsync(Guid id)
        {
            var dev = _devices.Find(d => d.Id == id);
            _devices.Remove(dev);
        }

        
    }
}
