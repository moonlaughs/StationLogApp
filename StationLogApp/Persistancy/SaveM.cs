using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft.Json;
using StationLogApp.Interfaces;

namespace StationLogApp.Model
{
    class SaveM<T> : ISave<T> where T : class 
    {
        #region
        private const string _serverUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        
        private string _apiPrefix = "api/";
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task<T> Save (T obj, string apiId)
        {
            string url = String.Concat(_serverUrl,_apiPrefix,apiId);
            {
                _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
                using (_httpClient = new HttpClient(_httpClientHandler))
                {
                    _httpClient.BaseAddress = new Uri(_serverUrl);
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    try
                    {
                        //string postBody = JsonConvert.SerializeObject(obj);
                        //var response = _httpClient.PostAsync(url,
                        //new StringContent(postBody, Encoding.UTF8, "application/json")).Result;

                        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, obj);
                        response.EnsureSuccessStatusCode();
                    }
                    catch (Exception ex)
                    {
                        new MessageDialog(ex.Message).ShowAsync();
                    }
                }
            }

            return null;
        }
    }
}
