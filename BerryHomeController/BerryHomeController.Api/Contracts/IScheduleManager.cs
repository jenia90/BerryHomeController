using System;
using BerryHomeController.Api.Models;

namespace BerryHomeController.Api.Contracts
{
    public interface IScheduleManager
    {
        Guid AddJob(Device device, DateTime start, DateTime end, DayOfWeek[] days);
        void RemoveJob(Guid jobId);
    }
}
