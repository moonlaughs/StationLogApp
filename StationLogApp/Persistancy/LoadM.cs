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
using StationLogApp.Interfaces;

namespace StationLogApp.Persistancy
{
    public class LoadM<T> : ILoad<T> where T : class
    {
        #region

        private const string ServerUrl = "http://http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        // private HttpClient _httpClient;

        #endregion

        //public async Task<ObservableCollection<T>> Load()
        //{
        //    _httpClientHandler = new HttpClientHandler() {UseDefaultCredentials = true};
        //    using (_httpClient = new HttpClient(_httpClientHandler))
        //    {
        //        _httpClient.BaseAddress = new Uri(ServerUrl);
        //        _httpClient.DefaultRequestHeaders.Clear();
        //        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        try
        //        {
        //            Task<HttpResponseMessage> task5 = _httpClient.GetAsync($"{ServerUrl}/{_apiPrefix}/{_apiID}");
        //            if (task5 != null)
        //            {
        //                if (task5.Result.IsSuccessStatusCode)
        //                {
        //                    var task51 = await task5.Result.Content.ReadAsStringAsync();
        //                    ObservableCollection<T> listt = JsonConvert.DeserializeObject<ObservableCollection<T>>(task51);
        //                    return listt;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await new MessageDialog(ex.Message).ShowAsync();
        //        }
        //        return null;
        //    }
        //}

        public ObservableCollection<ITaskFactory> Load()
        {
            _httpClientHandler = new HttpClientHandler() {UseDefaultCredentials = true};

            using (var client = new HttpClient(_httpClientHandler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/TaskTables").Result;

                if (response.IsSuccessStatusCode)
                {
                    var taskCatalog = response.Content.ReadAsAsync<ObservableCollection<ITaskFactory>>().Result;
                    return taskCatalog;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
