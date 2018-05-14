﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Model;

namespace StationLogApp.Interfaces
{
    public interface ILoad<T>
    {
        Task<ObservableCollection<T>> Load(string _apiID);
        ObservableCollection<T> RetriveCollection(string table);
    }
}
