using System;

namespace BerryHomeController.Api.Models
{
    public class State
    {
        public Guid DeviceId { get; set; }
        public bool DeviceState { get; set; }
    }
}
