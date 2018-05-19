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
using static System.String;

namespace StationLogApp.Model
{
    public class SaveM<T> : ISave<T> where T : class
    {
        #region
        private const string _serverUrl = "http://stationlogdbwebservice20180514015122.azurewebsites.net/";
        public readonly string ApiPrefix = "api/";
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        #endregion

        public async Task<T> Save(T obj, string apiId)
        {
            var url = Concat(_serverUrl, ApiPrefix, apiId);
            {
                _httpClientHandler = new HttpClientHandler() { UseDefaultCredentials = true };
                using (_httpClient = new HttpClient(_httpClientHandler))
                {
                    _httpClient.BaseAddress = new Uri(_serverUrl);
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    try
                    {
                        var postBody = JsonConvert.SerializeObject(obj);
                        var response = _httpClient.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
                    }
                    catch (Exception ex)
                    {
                        await new MessageDialog(ex.Message).ShowAsync();
                    }
                }
            }
            return null;
        }
    }
}