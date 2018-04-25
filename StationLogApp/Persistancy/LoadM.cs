﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StationLogApp.Interfaces;

namespace StationLogApp.Persistancy
{
    public class LoadM<T> : ILoad<T> where T : class 
    {
        #region
        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task<List<T>> Load()
        {
            _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (_httpClient = new HttpClient(_httpClientHandler))
            {
                _httpClient.BaseAddress = new Uri(ServerUrl);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    Task<HttpResponseMessage> task5 = _httpClient.GetAsync($"{ServerUrl}/{_apiPrefix}/{_apiID}");
                    if (task5 != null)
                    {
                        if (task5.Result.IsSuccessStatusCode)
                        {
                            var task51 = await task5.Result.Content.ReadAsStringAsync();
                            List<T> listt = JsonConvert.DeserializeObject<List<T>>(task51);
                            return listt;
                        }
                    }

                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }
    }
}
