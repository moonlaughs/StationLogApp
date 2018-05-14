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

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion


        public async Task Delete(int key)
        {
            _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (_httpClient = new HttpClient(_httpClientHandler))
            {
                _httpClient.BaseAddress = new Uri(ServerUrl);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    Task<HttpResponseMessage> task4 =
                        _httpClient.DeleteAsync($"{ServerUrl}/{_apiPrefix}/{_apiID}/{key}");
                    //if (task4 != null)
                    //{
                    //    if (task4.Result.IsSuccessStatusCode)
                    //    {
                    //        await task4.Result.Content.ReadAsAsync<T>();
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
