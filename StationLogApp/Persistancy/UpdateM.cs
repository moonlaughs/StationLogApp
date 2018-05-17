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
        private const string ServerUrl = "http://stationlogdbwebservice20180514015122.azurewebsites.net/";

        private string ApiPrefix = "api/";
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task Update(T obj, string apiId, int key)
        {
            string url = String.Concat(ServerUrl, ApiPrefix, $"{apiId}/", key);
            {
                _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
                using (_httpClient = new HttpClient(_httpClientHandler))
                {
                    _httpClient.BaseAddress = new Uri(ServerUrl);
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    try
                    {
                        string putitem = JsonConvert.SerializeObject(obj);
                        Task<HttpResponseMessage> task2 = _httpClient.PutAsync(url, new StringContent(putitem, Encoding.UTF8, "application/json"));
                    }
                    catch (Exception ex)
                    {
                        await new MessageDialog(ex.Message).ShowAsync();
                    }
                }
            }
        }
    }
}
