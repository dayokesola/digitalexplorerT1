using BizNest.Service.Interfaces;
using BizNest.Service.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BizNest.Service.Services
{
    public class HttpService : IHttpService
    {
        private static HttpClient _client;

        public HttpService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "domainstatus.p.rapidapi.com");
                _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "5acbd0464bmsh17680cb27715eabp13091bjsnf01f91412d73");
            }

          
        }
        public async Task<TOut> GetAsync<TOut>(string url) where TOut : class
        {
           
            TOut result = null;
            try
            {
                var response = await _client.GetAsync(url);
                result = await HandleResponse<TOut>(response);
            }
            catch (Exception e)
            {

                Debug.WriteLine(e);
            }
            return result;
        }

        public async Task<TOut> PostAsync<TOut, TIn>(TIn requestObject, string url) where TOut : class
        {
            TOut result = null;

            try
            {
                var response = await _client.PostAsync(url, new JsonContent(requestObject));
                result = await HandleResponse<TOut>(response);
            }
            catch (Exception e)
            {
                //Log Exception
                Debug.WriteLine(e);
            }

            return result;
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response) where T : class
        {
            T res = null;
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    res = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException();
            }

            return res;

        }
        public (bool success, string message) SetClientHeader(string key, string value)
        {
            //check if value exists on client then replace with new
            try
            {
                if (_client.DefaultRequestHeaders.Contains(key))
                {
                    _client.DefaultRequestHeaders.Remove(key);
                }
                _client.DefaultRequestHeaders.TryAddWithoutValidation(key, value);

                return (true, string.Empty);
            }
            catch (Exception e)
            {
                //Log Error
                return (false, $"An Error Ocurred! Exception Message: {e.Message}! Inner Exception: {e?.InnerException}");
            }
        }
    }
}
