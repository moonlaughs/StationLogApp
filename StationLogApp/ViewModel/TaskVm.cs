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
        #endregion 

        #region properties
        public RelayCommandClass SaveTaskClass { get; set; }
        public RelayCommandClass DoInfo { get; set; }

        public TaskHandler TaskHandler { get; set; }

        public string[] ScheduleArray { get; set; }
        public ObservableCollection<TaskEquipmentStation> EquipmentStations { get; set; }
        
        public ObservableCollection<TaskEquipmentStation> TaskCatalog
        {
            get
            {
                return _col.LoadToDo();
            }
            set
            {
                var loadToDo = _col.LoadToDo();
                loadToDo = value;
                OnPropertyChanged(nameof(TaskCatalog));
            } 
        }
        
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
        #endregion
        
        #region constructor
        public TaskVm()
        {
            _singleton = TaskEquipmentStationSingleton.GetInstance();
            TaskHandler = new TaskHandler(this);
            SaveTaskClass = new RelayCommandClass(TaskHandler.OperateTask);
            
            _selectedItem = new TaskEquipmentStation();

            _col = new Collections();
            EquipmentStations = _col.EquipmentStationsCollection();
            ScheduleArray = _col.ScheduleArray;

            var infoHandler = new InfoHandler(this);
            DoInfo = new RelayCommandClass(infoHandler.Info);
        }
        #endregion
    }
}
