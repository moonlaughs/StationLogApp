using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Annotations;
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
        private readonly TaskEquipmentStationSingleton _singleton;
        private readonly Collections _col;
        
        private TaskEquipmentStation _selectedItem;
        //private ObservableCollection<Station> _stationCatalog { get; }              //M
        #endregion 

        #region properties
        public RelayCommandClass SaveTaskClass { get; set; }
        public RelayCommandClass DoInfo { get; set; }
        public RelayCommandClass SortCommand { get; set; }

        public TaskHandler TaskHandler { get; set; }

        public string[] ScheduleArray { get; set; }
        //public string[] PeriodicityItems { get; set; }
        //public ObservableCollection<TaskEquipmentStation> EquipmentStations { get; set; }
        public ObservableCollection<Station> StationCollection { get; set; }
//=======

//        private TaskHandler _taskHandler;
//        private TaskCatalogSingleton _catalogSingleton;
//        private TaskClass _selectedTaskClass;
//        private ObservableCollection<Station> _stationCatalog;
//        private Station _selectedItemsStation;
//        #endregion

//        #region properties

//        public ObservableCollection<Station> StationCatalog
//        {
//            get
//            {
//                return _stationCatalog;
//            }
//        }

//        public Station SelectedItemsStation
//        {
//            get
//            {
//                return _selectedItemsStation;
//            }
//            set
//            {
//                _selectedItemsStation = value;
//                OnPropertyChanged(nameof(SelectedItemsStation));
//            }
//        }
//>>>>>>> f117f382991babb3d267a9384210680e1940944b

        private ObservableCollection<TaskEquipmentStation> _catalog;

        public ObservableCollection<TaskEquipmentStation> TaskCatalog
        {
            get { return _catalog; }
            set
            {
                _catalog = value;
                OnPropertyChanged(nameof(TaskCatalog));
            }
        }
        //{
        //    get
        //    {
        //        return _col.LoadToDo();
        //    }
        //    set
        //    {
        //        var loadToDo = _col.LoadToDo();
        //        loadToDo = value;
        //        OnPropertyChanged(nameof(TaskCatalog));
        //    } 
        //}
        
        public ObservableCollection<TaskEquipmentStation> DoneCatalog
        {
            get
            {
                return _col.LoadDone();
            }
            set
            {
                var loadDone = _col.LoadDone();
                loadDone = value;
                OnPropertyChanged(nameof(DoneCatalog));
            }
        }

        public TaskEquipmentStation SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public Station SelectedItemStation
        {
            get { return TaskHandler.SelectedStation; }
            set
            {
                TaskHandler.SelectedStation = value;
                OnPropertyChanged(nameof(SelectedItemStation));
            }
        }
        
        public string SelectedItemPeriodicity
        {
            get => TaskHandler.SelectedPeriodicityItem;
            set
            {
                TaskHandler.SelectedPeriodicityItem = value;
                OnPropertyChanged(nameof(SelectedItemPeriodicity));

            }
        }
//=======
//        public TaskHandler TaskHandler
//        {
//            get { return _taskHandler;}
//            set { _taskHandler = value; } }
//>>>>>>> f117f382991babb3d267a9384210680e1940944b
        #endregion

        #region constructor
        public TaskVm()
        {
            _singleton = TaskEquipmentStationSingleton.GetInstance();
            TaskHandler = new TaskHandler(this);
//=======
//            _catalogSingleton = TaskCatalogSingleton.Instance;
//            _selectedTaskClass = new TaskClass();
//            _taskHandler = new TaskHandler(this);
//            _stationCatalog = _taskHandler.LoadStation();
//>>>>>>> f117f382991babb3d267a9384210680e1940944b
            SaveTaskClass = new RelayCommandClass(TaskHandler.OperateTask);
            
            //_selectedItem = new TaskEquipmentStation();

            //_taskHandler = new TaskHandler(this);                                   //think
            //_taskHandler.LoadCollection();
            
            _selectedItem = new TaskEquipmentStation();

            var infoHandler = new InfoHandler(this);
            DoInfo = new RelayCommandClass(infoHandler.Info);

            _col = new Collections();
            StationCollection = _col.LoadStation();
            //EquipmentStations = _col.EquipmentStationsCollection();
            ScheduleArray = _col.ScheduleArray;


            _singleton.SetTaskEquipmentStation(SelectedItem);
            //SortCommand = new RelayCommandClass(TaskHandler.SortCollection());

            SortCommand = new RelayCommandClass(TaskHandler.SortCollection);

            TaskCatalog = _col.LoadToDo();
            //PeriodicityItems = _col.ScheduleArray;
        }
        #endregion

       
    }
}
