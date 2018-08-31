using BerryHomeController.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerryHomeController.Common.Extensions;

namespace BerryHomeController.Common.Services
{
    public class BerryApiDeviceServiceMock : IBerryApiService<Device>
    {
        private List<Device> _devices;

        public BerryApiDeviceServiceMock()
        {
            _devices = InitializeDevices();
        }

        public async Task<ICollection<Device>> GetAsync()
        {
            return _devices.OrderBy(d => d.DeviceName).ToList();
        }

        public async Task<Device> GetByIdAsync(Guid id)
        {
            return _devices.FirstOrDefault(d => d.Id == id);
        }

        public async Task<Device> PostAsync(Device data)
        {
            data.Id = Guid.NewGuid();
            _devices.Add(data);
            return data;
        }

        public async Task PutAsync(Guid id, Device data)
        {
            _devices.FirstOrDefault(d => d.Id == id)?.Map(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            var device = _devices.First(d => d.Id == id);
            _devices.Remove(device);
        }

        private List<Device> InitializeDevices()
        {
            return new List<Device>(5)
            {
                new Device()
                {
                    DeviceName = "TestDevice1",
                    DevicePin = 0,
                    Id = Guid.Parse("d36d3795-dd59-4bf9-a576-ffa5156764ab"),
                    Type = DeviceType.Input,
                    State = false,
                    Jobs = null
                },
                new Device()
                {
                    DeviceName = "TestDevice2",
                    DevicePin = 1,
                    Id = Guid.Parse("fa5ba3a7-aa5c-4824-afa3-466bd3e33360"),
                    Type = DeviceType.Output,
                    State = false,
                    Jobs = null
                },
                new Device()
                {
                    DeviceName = "TestDevice3",
                    DevicePin = 2,
                    Id = Guid.Parse("ad85d76e-17dd-4993-b81e-8468e57d5722"),
                    Type = DeviceType.Output,
                    State = true,
                    Jobs = null
                },
                new Device()
                {
                    DeviceName = "TestDevice4",
                    DevicePin = 3,
                    Id = Guid.Parse("aff7f710-a308-43f9-bd4a-7e1e6d973460"),
                    Type = DeviceType.Input,
                    State = true,
                    Jobs = null
                },
                new Device()
                {
                    DeviceName = "TestDevice5",
                    DevicePin = 4,
                    Id = Guid.Parse("3a91316c-9828-45ec-837a-87b5cab3ec44"),
                    Type = DeviceType.Input,
                    State = false,
                    Jobs = null
                }
            };
        }
    }
}
