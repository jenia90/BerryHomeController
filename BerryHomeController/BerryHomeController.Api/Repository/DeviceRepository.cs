using System;
using System.Collections.Generic;
using System.Linq;
using BerryHomeController.Api.Contracts;
using BerryHomeController.Api.Extensions;
using BerryHomeController.Api.Models;

namespace BerryHomeController.Api.Repository
{
    public class DeviceRepository : RepositoryBase<Device>, IDeviceRepository
    {
        public DeviceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Device> GetAllDevices()
        {
            return FindAll().OrderBy(d => d.DeviceName);
        }

        public Device GetDeviceById(Guid deviceId)
        {
            return FindByCondition(d => d.Id.Equals(deviceId))
                .DefaultIfEmpty(null)
                .FirstOrDefault();
        }

        public void CreateDevice(Device device)
        {
            Create(device);
            Save();
        }

        public void UpdateDevice(Device dbDevice, Device device)
        {
            dbDevice.Map(device);
            Update(dbDevice);
            Save();
        }

        public void DeleteDevice(Device device)
        {
            Delete(device);
            Save();
        }
    }
}
