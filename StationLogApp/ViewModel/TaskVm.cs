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
        public RelayCommandClass SortCommand { get; set; }
        public RelayCommandClass DoGoUpdate { get; set; }
        public RelayCommandClass DoDelete { get; set; }
        public RelayCommandClass DoClear { get; set; }

        public TaskHandler TaskHandler { get; set; }
        public IDeleteTask DeleteTaskHandler { get; set; }

        public string[] ScheduleArray { get; set; }
        public ObservableCollection<Station> StationCollection { get; set; }

        public ObservableCollection<TaskEquipmentStation> TaskCatalog
        {
            get { return TaskHandler.LoadedCollection; }
            set
            {
                TaskHandler.LoadedCollection = value;
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
        #endregion

        #region constructor
        public TaskVm()
        {
            _singleton = TaskEquipmentStationSingleton.GetInstance();

            TaskHandler = new TaskHandler(this);
            DeleteTaskHandler = new DeleteTaskHandler(this);
            var infoHandler = new InfoHandler(this);

            TaskHandler.LoadCollection();
            _col = new Collections();
            StationCollection = _col.LoadStation();
            ScheduleArray = _col.ScheduleArray;

            SaveTaskClass = new RelayCommandClass(TaskHandler.OperateTask);
            SortCommand = new RelayCommandClass(TaskHandler.SortCollection);
            DoGoUpdate = new RelayCommandClass(GoUpdate);
            DoClear = new RelayCommandClass(Clear);
            DoDelete = new RelayCommandClass(Delete);
            DoInfo = new RelayCommandClass(infoHandler.Info);

            _selectedItem = new TaskEquipmentStation();
        }
        #endregion

        public async void GoUpdate()
        {
            if (SelectedItem.TaskId != 0)
            {
                _singleton.SetTaskEquipmentStation(SelectedItem);
                FrameNavigateClass frame = new FrameNavigateClass();
                frame.ActivateFrameNavigation(typeof(UpdatePage), SelectedItem);
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the task.");
                await msg.ShowAsync();
            }
        }

        public void Clear()
        {
            TaskCatalog = TaskHandler.LoadCollection();
        }

        public async void Delete()
        {
            if (SelectedItem.TaskId != 0)
            {
                _singleton.SetTaskEquipmentStation(SelectedItem);
                DeleteTaskHandler.DeleteTask(SelectedItem.TaskId);
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the task.");
                await msg.ShowAsync();
            }
        }
    }
}
