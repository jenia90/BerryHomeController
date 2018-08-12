using BerryHomeController.Common.Models;

namespace BerryHomeController.Common.Services
{
    public class BerryApiDeviceService : BerryApiService<Device>
    {
        public BerryApiDeviceService() : base("Device/")
        {
        }
    }
}