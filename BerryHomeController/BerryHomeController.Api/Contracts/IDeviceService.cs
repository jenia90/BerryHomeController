using BerryHomeController.Api.Models;
using System;

namespace BerryHomeController.Api.Contracts
{
    public interface IDeviceService
    {
        void SetState(Device device, bool state);
        void SetStateById(Guid id, bool state);
        bool GetState(Device device);
        bool GetStateById(Guid id);
        Guid RegisterDevice(Device device);
        void UpdateDevice(Guid id, Device dbDevice, Device device);
        void DeregisterDevice(Device device);
    }
}
