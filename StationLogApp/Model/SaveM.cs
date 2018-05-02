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
        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task Save(T obj)
        {
            {
                _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
                using (_httpClient = new HttpClient(_httpClientHandler))
                {
                    _httpClient.BaseAddress = new Uri(ServerUrl);
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    try
                    {
                        string postBody = JsonConvert.SerializeObject(obj);
                        var response = _httpClient.PostAsync("api/Tasks", 
                        new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
                    }
                    catch (Exception ex)
                    {
                        new MessageDialog(ex.Message).ShowAsync();
                    }
                }

            }
        }
    }
}
