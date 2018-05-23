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
        private const string ServerUrl = "http://stationlogdbwebservice20180514015122.azurewebsites.net/";
        public readonly string ApiPrefix = "api/";
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task Delete(string apiId, int key)
        {
            _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (_httpClient = new HttpClient(_httpClientHandler))
            {
                _httpClient.BaseAddress = new Uri(ServerUrl);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage task4 =
                        _httpClient.DeleteAsync($"{ServerUrl}{ApiPrefix}{apiId}/{key}").Result;
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}