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

namespace StationLogApp.Persistancy
{
    public class UpdateM<T> : IUpdate<T> where T : class
    {
        #region

        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;

        #endregion

        public async Task Update(int key, T obj)
        {
            _httpClientHandler = new HttpClientHandler() {UseDefaultCredentials = true};
            using (_httpClient = new HttpClient(_httpClientHandler))
            {
                _httpClient.BaseAddress = new Uri(ServerUrl);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    string putitem = JsonConvert.SerializeObject(obj);
                    Task<HttpResponseMessage> task2 = _httpClient.PutAsync($"{ServerUrl}/{_apiPrefix}/{_apiID}/{key}",
                        new StringContent(putitem, Encoding.UTF8, "application/json"));
                    if (task2 != null)
                    {
                        if (task2.Result.IsSuccessStatusCode)
                        {
                            await task2.Result.Content.ReadAsStringAsync();
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
