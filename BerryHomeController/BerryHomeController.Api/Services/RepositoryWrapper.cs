using BerryHomeController.Api.Contracts;
using BerryHomeController.Api.Repository;

namespace BerryHomeController.Api.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public IJobRepository Job { get; set; }
        public IDeviceRepository Devices { get; set; }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            Job = new JobRepository(repositoryContext);
            Devices = new DeviceRepository(repositoryContext);
        }
    }
}
