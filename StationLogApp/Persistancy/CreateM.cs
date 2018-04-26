using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture.Frames;
using Windows.UI.Popups;
using Newtonsoft.Json;
using StationLogApp.Factories;
using StationLogApp.Interfaces;

namespace StationLogApp.Persistancy
{
    class CreateM<T> : ICreate<T> where T : class
    {
        #region
        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task Create(T obj)
        {
            _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (_httpClient = new HttpClient(_httpClientHandler))
            {
                _httpClient.BaseAddress = new Uri(ServerUrl);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    string postitem = JsonConvert.SerializeObject(obj);
                    Task<HttpResponseMessage> task3 = _httpClient.PostAsync($"{ServerUrl}/{_apiPrefix}/{_apiID}",
                        new StringContent(postitem, Encoding.UTF8, "application/json"));
                    if (task3 != null)
                    {
                        if (task3.Result.IsSuccessStatusCode)
                        {
                            task3.Result.Content.ReadAsAsync<T>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
