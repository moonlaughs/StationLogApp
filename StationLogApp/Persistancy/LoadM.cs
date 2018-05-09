using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft.Json;
using StationLogApp.Common;
using StationLogApp.Interfaces;
using StationLogApp.Model;

namespace StationLogApp.Persistancy
{
    public class LoadM<T> : ILoad<T> where T : class
    {
        
        #region instance fields
        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";
        private string _serverURL;
        private string _apiPrefix = "api";
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion


        public async Task<ObservableCollection<T>> Load(string _apiID)
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
                            var listt = JsonConvert.DeserializeObject<ObservableCollection<T>>(task51);
                            return listt;
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
