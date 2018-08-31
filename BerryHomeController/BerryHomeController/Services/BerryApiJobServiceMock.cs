using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BerryHomeController.Common.Extensions;
using BerryHomeController.Common.Models;

namespace BerryHomeController.Common.Services
{
    internal class BerryApiJobServiceMock : IBerryApiService<Job>
    {
        private readonly ICollection<Job> _jobs;
        public BerryApiJobServiceMock()
        {
            _jobs = new List<Job>
            {
                new Job()
                {
                    DeviceName = "TestDevice1",
                    DeviceId = Guid.Parse("d36d3795-dd59-4bf9-a576-ffa5156764ab"),
                    DaysList = new []{DayOfWeek.Tuesday, DayOfWeek.Friday},
                    Start = DateTime.Now,
                    End = DateTime.Now + new TimeSpan(0, 1, 0, 0)
                },
                new Job()
                {
                    DeviceName = "TestDevice2",
                    DeviceId = Guid.Parse("fa5ba3a7-aa5c-4824-afa3-466bd3e33360"),
                    DaysList = new []{DayOfWeek.Thursday, DayOfWeek.Saturday},
                    Start = DateTime.Now + new TimeSpan(0, 5, 30, 0),
                    End = DateTime.Now + new TimeSpan(0, 1, 0, 0)
                },
                new Job()
                {
                    DeviceName = "TestDevice3",
                    DeviceId = Guid.Parse("ad85d76e-17dd-4993-b81e-8468e57d5722"),
                    DaysList = new List<DayOfWeek>(),
                    Start = DateTime.Now,
                    End = DateTime.Now + new TimeSpan(0, 1, 0, 0)
                },
                new Job()
                {
                    DeviceName = "TestDevice1",
                    DeviceId = Guid.Parse("d36d3795-dd59-4bf9-a576-ffa5156764ab"),
                    DaysList = new []{DayOfWeek.Sunday},
                    Start = DateTime.Now + new TimeSpan(0, 2, 30, 0),
                    End = DateTime.Now + new TimeSpan(0, 2, 0, 0)
                }
            };
        }

        public async Task<ICollection<Job>> GetAsync()
        {
            return _jobs.OrderBy(j => j.Start).ToList();
        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            return _jobs.FirstOrDefault(j => j.Id == id);
        }

        public async Task<ICollection<Job>> GetByDeviceIdAsync(Guid id)
        {
            return _jobs.Where(j => j.DeviceId == id).ToList();
        }

        public async Task<Job> PostAsync(Job data)
        {
            data.Id = Guid.NewGuid();
            _jobs.Add(data);
            return data;
        }

        public async Task PutAsync(Guid id, Job data)
        {

            var job = _jobs.FirstOrDefault(j => j.Id == id);
            if (job.DeviceId != data.DeviceId) return;

            job.Map(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            var job = _jobs.First(j => j.Id == id);
            _jobs.Remove(job);
        }
    }
}
