﻿namespace BerryHomeController.Api.Contracts
{
    public interface IRepositoryWrapper
    {
        IJobRepository Job { get; set; }
        IDeviceRepository Devices { get; set; }
    }
}
