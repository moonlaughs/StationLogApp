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
        private string _taskName;
        private string _taskSchedule;
        private string _registration;
        private string _taskType;
        private DateTimeOffset _dueDate;
        //private DateTimeOffset? _doneDate;
        private string _comment;
        private string _doneVar;

        private TaskEquipmentStationSingleton _singleton;
        private readonly TaskVm _taskVm;
        
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
                //OnPropertyChanged(nameof(TaskTypes));
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
            _singleton = TaskEquipmentStationSingleton.GetInstance();

            TaskId = _singleton.GetTaskId();
            TaskName = _singleton.GetTaskName();
            TaskSchedule = _singleton.GetTaskSchedule();
            TaskType = _singleton.GetTaskType();
            EquipmentId = _singleton.GetEquipmentId();
            DueDate = _singleton.GetDueDate();
            //DoneDate = _singleton.GetDoneDate();
            EquipmentName = _singleton.GetEquipmentName();
            StationName = _singleton.GetStationName();
            Registration = _singleton.GetRegistration();
            Comment = _singleton.GetComment();
            DoneVar = _singleton.GetDoneVar();

            Uph = new UpdateTaskHandler(this);
            DoUpdate = new RelayCommandClass(Uph.UpdateTask);
        }

        //props without ability to change
        public int TaskId { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string StationName { get; set; }

        //props that can be changed
        public DateTimeOffset DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        //public DateTimeOffset? DoneDate
        //{
        //    get { return _doneDate; }
        //    set
        //    {
        //        _doneDate = value;
        //        OnPropertyChanged(nameof(DoneDate));
        //    }
        //}
        
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
        
        public string TaskType
        {
            get { return _taskType; }
            set
            {
                _taskType = value;
                OnPropertyChanged(nameof(TaskType));
            }
        }

        public string TaskSchedule
        {
            get { return _taskSchedule; }
            set
            {
                _taskSchedule = value;
                OnPropertyChanged(nameof(TaskSchedule));
            }
        }

        public string Registration
        {
            get { return _registration; }
            set
            {
                _registration = value;
                OnPropertyChanged(nameof(Registration));
            }
        }

        public string DoneVar
        {
            get { return _doneVar; }
            set
            {
                _doneVar = value;
                OnPropertyChanged(nameof(DoneVar));
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
    }
}
