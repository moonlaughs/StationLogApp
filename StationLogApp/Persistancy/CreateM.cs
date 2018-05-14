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
using StationLogApp.Interfaces;

namespace StationLogApp.Persistancy
{
    class CreateM<T> : ICreate<T> where T : class
    {
        #region instancefields
        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix = "api/";
        private string _apiID = "Notes";
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        private string _url;
        #endregion

        public CreateM()
        {
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.UseDefaultCredentials = true;
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.BaseAddress = new Uri(ServerUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = String.Format("{0}{1}", _apiPrefix, _apiID);
            _url = url;
        }

        public async Task Create(T obj)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url, obj);
            response.EnsureSuccessStatusCode();
        }
    }
}
