using System;
using System.Collections.Generic;

namespace BerryHomeController.Common.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; }
        public IEnumerable<DayOfWeek> DaysList { get; set; }

    }
}
