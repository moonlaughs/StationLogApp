using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Factories;
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

        #endregion

        #region properties

        public ObservableCollection<TaskClass> TaskCatalog {
            get
            {
                return _catalogSingleton.TaskCatalog;
            }
            set
            {
                _catalogSingleton.TaskCatalog = value;
            } 
        }
        
        #endregion

        #region constructor
        public TaskVm()
        {
            _catalogSingleton = TaskCatalogSingleton.Instance;
        }
        #endregion

        #region Methods
       
        #endregion
    }
}
