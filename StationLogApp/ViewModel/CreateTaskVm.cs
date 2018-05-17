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
using StationLogApp.Singletons;

namespace StationLogApp.ViewModel
{
    public class CreateTaskVm : NotifyPropertyChangedClass
    {
        #region instancefields
        private DateTimeOffset _dueDate;
        private TaskEquipmentStation _newItem;
        #endregion

        #region properties
        private ICreateTask CreateTaskHandler { get; }

        public RelayCommandClass DoCreateTask { get; set; }

        public ObservableCollection<TaskClass> Tasks { get; set; }
        public ObservableCollection<TaskEquipmentStation> EquipmentStations { get; set; }

        public string[] TaskTypes { get; set; }
        public string[] TaskSchedules { get; set; }

        public TaskEquipmentStation NewItem
        {
            get { return _newItem; }
            set
            {
                _newItem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }

        public DateTimeOffset DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        #endregion

        #region constructor
        
        public CreateTaskVm()
        {
            CreateTaskHandler = new CreateTaskHandler(this);
            DoCreateTask = new RelayCommandClass(CreateTaskHandler.CreateTask);
            NewItem = new TaskEquipmentStation();
            var col = new Collections();
            _dueDate = DateTimeOffset.Now;
            TaskTypes = col.TypeArray;
            TaskSchedules = col.ScheduleArray;
            EquipmentStations = col.EquipmentStationsCollection();
        }

        #endregion
    }
}
