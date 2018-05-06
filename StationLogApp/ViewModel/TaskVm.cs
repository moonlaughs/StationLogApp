using System;
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
                OnPropertyChanged(nameof(TaskCatalog));
            } 
        }

        public TaskClass SelectedTaskClass
        {
            get
            {
                return _selectedTaskClass;
            }
            set
            {
                _selectedTaskClass = value;
                OnPropertyChanged(nameof(SelectedTaskClass));
            }
        }

        public RelayCommandClass SaveTaskClass { get; set; }
        

        public TaskHandler TaskHandler { get; set; }

        #endregion

        #region constructor
        public TaskVm()
        {
            _catalogSingleton = TaskCatalogSingleton.Instance;
            _selectedTaskClass = new TaskClass();
            TaskHandler = new TaskHandler(this);
            SaveTaskClass = new RelayCommandClass(TaskHandler.OperateTask);
        }
        #endregion

        
    }
}
