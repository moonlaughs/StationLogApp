using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Model;
using StationLogApp.Singletons;

namespace StationLogApp.ViewModel
{
    public class UpdateTaskVm : NotifyPropertyChangedClass
    {
        private DateTimeOffset _dueDate;
        private TaskEquipmentStationSingleton _singleton;
        private TaskVm _taskVm;

        public UpdateTaskHandler Uph { get; set; }

        public RelayCommandClass DoUpdate { get; set; }

        public Collections col { get; set; }

        public ObservableCollection<TaskEquipmentStation> EquipmentStations
        {
            get { return col.EquipmentStationsCollection(); }
            set
            {
                var equipmentStationsCollection = col.EquipmentStationsCollection();
                equipmentStationsCollection = value;
                OnPropertyChanged(nameof(EquipmentStations));
            }
        }

        public string[] TaskTypes
        {
            get { return col.TypeArray; }
            set
            {
                col.TypeArray = value;
                OnPropertyChanged(nameof(TaskTypes));
            }
        }

        public string[] TaskSchedules
        {
            get { return col.ScheduleArray; }
            set
            {
                col.ScheduleArray = value;
                OnPropertyChanged(nameof(TaskSchedules));
            }
        }

        public UpdateTaskVm()
        {
            _taskVm = new TaskVm();
            col = new Collections();
            TaskTypes = col.TypeArray;
            TaskSchedules = col.ScheduleArray;
            EquipmentStations = col.EquipmentStationsCollection();
            SelectedItem = new TaskEquipmentStation();
            _singleton = TaskEquipmentStationSingleton.GetInstance();

            TaskId = _singleton.GetTaskId();
            TaskName = _singleton.GetTaskName();
            TaskSchedule = _singleton.GetTaskSchedule();
            TaskType = _singleton.GetTaskType();
            EquipmentID = _singleton.GetEquipmentId();
            EquipmentName = _singleton.GetEquipmentName();
            DueDate = _singleton.GetDueDate();

            Uph = new UpdateTaskHandler(this);
            DoUpdate = new RelayCommandClass(Uph.UpdateTask);
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

        public TaskEquipmentStation SelectedItem
        {
            get { return _taskVm.SelectedItem; }
            set
            {
                _taskVm.SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public int TaskId { get; set; }

        public string TaskName
        {
            get { return _singleton.GetTaskName(); }
            set
            {
                var taskName = _singleton.GetTaskName();
                taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        public string EquipmentName
        {
            get { return _taskVm.SelectedItem.EquipmentName; }
            set
            {
                _taskVm.SelectedItem.EquipmentName = value;
                OnPropertyChanged(nameof(EquipmentName));
            }
        }

        public int EquipmentID
        {
            get { return _taskVm.SelectedItem.EquipmentID; }
            set
            {
                _taskVm.SelectedItem.EquipmentID = value; 
                OnPropertyChanged(nameof(EquipmentID));
            }
        }

        public string TaskType
        {
            get { return _taskVm.SelectedItem.TaskType; }
            set
            {
                _taskVm.SelectedItem.TaskType = value;
                OnPropertyChanged(nameof(TaskType));
            }
        }

        public string TaskSchedule
        {
            get { return _taskVm.SelectedItem.TaskSchedule; }
            set
            {
                _taskVm.SelectedItem.TaskSchedule = value;
                OnPropertyChanged(nameof(TaskSchedule));
            }
        }
    }
}
