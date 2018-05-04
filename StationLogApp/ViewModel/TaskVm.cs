﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Factories;
using StationLogApp.Handlers;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.Singletons;
using StationLogApp.View;

namespace StationLogApp.ViewModel
{
    public class TaskVm : NotifyPropertyChangedClass
    {
        #region instancefields

        private TaskCatalogSingleton _catalogSingleton;
        private FrameNavigateClass _frameNavigation;
        private TaskClass _selectedTaskClass;
       
        


        #endregion

        #region properties

        public ObservableCollection<TaskClass> TaskCatalog
        {
            get
            {
                return _catalogSingleton.TaskCatalog;
            }
            set
            {
                _catalogSingleton.TaskCatalog = value;
            } 
        }

        public TaskClass SelectedEvent
        {
            get
            {
                return _selectedTaskClass;
            }
            set
            {
                _selectedTaskClass = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        public RelayCommandClass SaveTaskClass { get; set; }

        public TaskHandler TaskHandler { get; set; }

        #endregion

        #region constructor
        public TaskVm()
        {
            _catalogSingleton = TaskCatalogSingleton.Instance;
            TaskHandler = new TaskHandler(this);
            SaveTaskClass = new RelayCommandClass(TaskHandler.SaveTaskClass);
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
