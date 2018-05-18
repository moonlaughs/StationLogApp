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
    public class DeleteM<T> : IDelete<T> where T : class
    {
        #region
        private const string _serverUrl = "http://stationlogdbwebservice20180514015122.azurewebsites.net/";
        private string _apiPrefix = "api/";
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task Delete(string _apiID, int key)
        {
            _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (_httpClient = new HttpClient(_httpClientHandler))
            {
                _httpClient.BaseAddress = new Uri(_serverUrl);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage task4 =
                        _httpClient.DeleteAsync($"{_serverUrl}{_apiPrefix}{_apiID}/{key}").Result;
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
