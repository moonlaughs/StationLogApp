﻿using System;
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
    //public class LoadM<T> : ILoad<T> where T : class
    //{
    //    #region instancefields

    //    private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net";

    //    private string _serverURL;
    //    private string _apiPrefix = "api";
    //    private string _apiID;
    //    private HttpClientHandler _httpClientHandler;
    //    private HttpClient _httpClient;

    //    #endregion

    //    public async Task<ObservableCollection<User>> Load(string _apiID)
    //    {
    //        _httpClientHandler = new HttpClientHandler() {UseDefaultCredentials = true};
    //        using (_httpClient = new HttpClient(_httpClientHandler))
    //        {
    //            _httpClient.BaseAddress = new Uri(ServerUrl);
    //            _httpClient.DefaultRequestHeaders.Clear();
    //            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //            try
    //            {
    //                Task<HttpResponseMessage> task5 = _httpClient.GetAsync($"{ServerUrl}/{_apiPrefix}/{_apiID}");
    //                if (task5 != null)
    //                {
    //                    if (task5.Result.IsSuccessStatusCode)
    //                    {
    //                        var task51 = await task5.Result.Content.ReadAsStringAsync();
    //                       // ObservableCollection<T> listt = JsonConvert.DeserializeObject<ObservableCollection<T>>(task51);
    //                        ObservableCollection<User> listt = DeserializeUser(task51);
    //                        return listt;
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                await new MessageDialog(ex.Message).ShowAsync();
    //            }
    //            return null;
    //        }
    //    }

    //    private static ObservableCollection<User> DeserializeUser(string json)
    //    {
    //        //GenericJsonConverter<T> userConverter = new GenericJsonConverter<T>();
    //        //var settings = new JsonSerializerSettings();
    //        //settings.Converters.Add(new GenericJsonConverter<T>());
    //        //ObservableCollection<T> userConverted = JsonConvert.DeserializeObject<ObservableCollection<T>>(json, settings);
    //        //return userConverted;
    //        UserConverter userConverter = new UserConverter();
    //        var settings = new JsonSerializerSettings();
    //        settings.Converters.Add(new UserConverter());
    //        ObservableCollection<User> userConverted = JsonConvert.DeserializeObject<ObservableCollection<User>>(json, settings);
    //        return userConverted;

    //    }
    //}

    public class LoadM<T> : ILoad<T> where T : class
    {
        #region instancefields

        private const string ServerUrl = "http://stationlogwebservice20180424112310.azurewebsites.net/";

        private string _serverURL;
        private string _apiPrefix = "api";
        private string _apiID = "UserTables";
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;


        #endregion

        public async Task<ObservableCollection<User>> Load(string _apiID)
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
                            // ObservableCollection<T> listt = JsonConvert.DeserializeObject<ObservableCollection<T>>(task51);
                            ObservableCollection<User> listt = DeserialiseUser(task51);
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

        private static ObservableCollection<User> DeserialiseUser(string json)
        {
            UserConverter userConverter = new UserConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new UserConverter());
            ObservableCollection<User> userConverted = JsonConvert.DeserializeObject<ObservableCollection<User>>(json, settings);
            return userConverted;
        }

    }

}
