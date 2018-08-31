using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BerryHomeController.Common.Models;
using Newtonsoft.Json;

namespace BerryHomeController.Common.Services
{
    public class BerryApiJobService : BerryApiService<Job>
    {
        public BerryApiJobService() : base("Job/")
        {
        }

        public async Task<ICollection<Job>> GetByDeviceIdAsync(Guid id)
        {
            HttpResponseMessage response;
            using (var client = CreateHttpClient())
            {
                response = await client.GetAsync(Endpoint + "device/" + id);
            }

            await HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<ICollection<Job>>(content));
        }
    }
}
