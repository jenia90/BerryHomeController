using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BerryHomeController.Common.Services
{
    public abstract class BerryApiService<T> : IBerryApiService<T>
    {
        //TODO: Move this into settings.
        private static readonly string API_URL = "http://192.168.1.220:8000/";
        protected readonly string Endpoint;

        public BerryApiService(string endpoint)
        {
            Endpoint = endpoint;
        }

        public async Task<ICollection<T>> GetAsync()
        {
            HttpResponseMessage response;
            using (var client = CreateHttpClient())
            {
                response = await client.GetAsync(Endpoint);
            }

            await HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(content));
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            HttpResponseMessage response;
            using (var client = CreateHttpClient())
            {
                response = await client.GetAsync(Endpoint + id);
            }

            await HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
        }

        public async Task<T> PostAsync(T data)
        {
            HttpResponseMessage response;
            var sData = await Task.Run(() => JsonConvert.SerializeObject(data));

            using (var client = CreateHttpClient())
            {
                response = await client.PostAsync(Endpoint,
                    new StringContent(sData, Encoding.UTF8, "application/json"));
            }

            await HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
        }

        public async Task PutAsync(Guid id, T data)
        {
            HttpResponseMessage response;
            var sData = await Task.Run(() => JsonConvert.SerializeObject(data));

            using (var client = CreateHttpClient())
            {
                response = await client.PutAsync(Endpoint + id,
                    new StringContent(sData, Encoding.UTF8, "application/json"));
            }

            await HandleResponse(response);
        }

        public async Task DeleteAsync(Guid id)
        {
            HttpResponseMessage response;
            using (var client = CreateHttpClient())
            {
                response = await client.DeleteAsync(Endpoint + id);
            }

            await HandleResponse(response);
        }

        protected HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        protected async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}