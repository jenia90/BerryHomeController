using System;
using System.Collections.Generic;
using System.Text;
using BerryHomeController.Common.Models;

namespace BerryHomeController.Common.Extensions
{
    internal static class JobExtensions
    {
        public static void Map(this Job dbJob, Job job)
        {
            dbJob.Id = job.Id;
            dbJob.Start = job.Start;
            dbJob.End = job.End;
            dbJob.DaysList = job.DaysList;
        }
    }
}
