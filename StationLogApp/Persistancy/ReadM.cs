using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Interfaces;

namespace StationLogApp.Persistancy
{
    public class ReadM<T> : IRead<T> where T : class 
    {
        #region instancefields
        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task<T> Read(int key)
        {
            _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (_httpClient = new HttpClient(_httpClientHandler))
            {
                _httpClient.BaseAddress = new Uri(ServerUrl);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    Task<HttpResponseMessage> task1 = _httpClient.GetAsync($"{ServerUrl}/{_apiPrefix}/{_apiID}/{key}");
                    if (task1 != null)
                    {
                        if (task1.Result.IsSuccessStatusCode)
                        {
                            return task1.Result.Content.ReadAsAsync<T>().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
                return null;
            }
        }
    }
}
