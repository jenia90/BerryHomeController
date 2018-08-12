using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BerryHomeController.Common.Services
{
    public class BerryApiService<T> : IBerryApiService<T>
    {
        //TODO: Move this into settings.
        private static readonly string API_URL = "http://192.168.1.220:8000/";
        private readonly string _endpoint;

        public BerryApiService(string endpoint)
        {
            _endpoint = endpoint;
        }

        public Task<List<T>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsyncById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> PostAsync(T data)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(Guid id, T data)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(API_URL);
            

            return client;
        }
    }
}