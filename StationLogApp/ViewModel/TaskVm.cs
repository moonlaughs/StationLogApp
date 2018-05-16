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

        // private TaskCatalogSingleton _catalogSingleton;

        private TaskClass _selectedTaskClass;
        private TaskHandler _taskHandler;
        private ObservableCollection<TaskEquipmentStation> _taskCollection;
        private ObservableCollection<Station> _stationCatalog;
        private Station _selectedItemStation;
        

        #endregion

        #region properties

        public ObservableCollection<TaskEquipmentStation> TaskCatalog
        {
            get { return _taskHandler.LoadedCollection; }
            set
            {
                _taskHandler.LoadedCollection = value;
                OnPropertyChanged(nameof(TaskCatalog));
            }
        }

        //public ObservableCollection<TaskEquipmentStation> TaskCatalog
        //{
        //    get
        //    {
        //        return _catalogSingleton.TaskCatalog;
        //    }
        //    set
        //    {
        //        _catalogSingleton.TaskCatalog = value;
        //        OnPropertyChanged(nameof(TaskCatalog));
        //    } 
        //}

        
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
        public RelayCommandClass SortStationCommand { get; set; }
        

        public TaskHandler TaskHandler
        {
            get { return _taskHandler; }
            set { _taskHandler = value; }
        }

        public Station SelectedItemStation
        {
            get
            {
                return _selectedItemStation;
            }
            set
            {
                _selectedItemStation = value;
                OnPropertyChanged(nameof(SelectedItemStation));
            }
        }

        #endregion

        #region constructor
        public TaskVm()
        {
            //_catalogSingleton = TaskCatalogSingleton.Instance;
           
            _selectedTaskClass = new TaskClass();
            _selectedItemStation = new Station();
            _taskHandler = new TaskHandler(this);
            _taskCollection = _taskHandler.LoadCollection();
            _stationCatalog = _taskHandler.LoadStation();
            SaveTaskClass = new RelayCommandClass(_taskHandler.OperateTask);
            SortStationCommand = new RelayCommandClass(_taskHandler.SortCollection);
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
