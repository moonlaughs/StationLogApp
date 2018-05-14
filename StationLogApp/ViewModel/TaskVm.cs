using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
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
        private TaskHandler _taskHandler;
        private ObservableCollection<Station> _stationCatalog;

        #endregion


        #region properties

        public ObservableCollection<TaskEquipmentStation> TaskCatalog
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

        public ObservableCollection<Station> StationCatalog
        {
            get { return _stationCatalog; }
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


        public TaskHandler TaskHandler
        {
            get { return _taskHandler; }
            set { _taskHandler = value; }
        }

        #endregion

        #region constructor
        public TaskVm()
        {
            _catalogSingleton = TaskCatalogSingleton.Instance;
            _selectedTaskClass = new TaskClass();
            _taskHandler = new TaskHandler(this);
            _stationCatalog = _taskHandler.LoadStation();
            SaveTaskClass = new RelayCommandClass(TaskHandler.OperateTask);
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
