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
using StationLogApp.Model;

namespace StationLogApp.Persistancy
{
    public class LoadTask<TaskClass>: ILoad<TaskClass>
    {
        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix = "api";
        private string _apiID = "Tasks";

        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;


        public async Task<ObservableCollection<TaskClass>> Load()
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
                            var listt = JsonConvert.DeserializeObject<ObservableCollection<TaskClass>>(task51);
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

        public async void LoadCatalog()
        {
            ILoad<TaskClass> retrievedCatalog = new LoadTask<TaskClass>();
            Task<ObservableCollection<TaskClass>> sth = retrievedCatalog.Load();
            await sth;
            ObservableCollection<TaskClass> col = sth.Result;
            
        }

    }
}
