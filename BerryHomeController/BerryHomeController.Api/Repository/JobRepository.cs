using System;
using System.Collections.Generic;
using System.Linq;
using BerryHomeController.Api.Contracts;
using BerryHomeController.Api.Extensions;
using BerryHomeController.Api.Models;

namespace BerryHomeController.Api.Repository
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return FindAll().OrderBy(j => j.Start);
        }

        public Job GetJobById(Guid jobId)
        {
            return FindByCondition(j => j.Id.Equals(jobId))
                .DefaultIfEmpty(new Job())
                .FirstOrDefault();
        }

        public IEnumerable<Job> GetJobsByDeviceId(Guid id)
        {
            return FindAll().Where(j => j.DeviceId.Equals(id));
        }

        public void CreateJob(Job job)
        {
            if(job.Id == Guid.Empty) job.Id = Guid.NewGuid();
            Create(job);
            Save();
        }

        public void UpdateJob(Job dbJob, Job job)
        {
            dbJob.Map(job);
            Update(job);
            Save();
        }

        public void DeleteJob(Job job)
        {
            Delete(job);
            Save();
        }
    }
}
