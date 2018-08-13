using BerryHomeController.Common.Models;

namespace BerryHomeController.Common.Services
{
    public class BerryApiJobService : BerryApiService<Job>
    {
        public BerryApiJobService() : base("Job/")
        {
        }
    }
}
