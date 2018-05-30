using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Converters;
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
        private string _comment;
        private string _doneVar;

        public UpdateTaskHandler Uph { get; set; }
        private DateConverter DC { get; set; }

        public RelayCommandClass DoUpdate { get; set; }

        public Collections Col { get; set; }

        public ObservableCollection<TaskEquipmentStation> EquipmentStations
        {
            get => Col.EquipmentStationsCollection();
            set => Col.EquipmentStationsCollection();
        }

        public string[] TaskTypes
        {
            get => Col.TypeArray;
            set => Col.TypeArray = value;
        }

        public string[] TaskSchedules
        {
            get => Col.ScheduleArray;
            set => Col.ScheduleArray = value;
        }

        public UpdateTaskVm()
        {
            Col = new Collections();
            TaskTypes = Col.TypeArray;
            TaskSchedules = Col.ScheduleArray;
            EquipmentStations = Col.EquipmentStationsCollection();
            var singleton = TaskEquipmentStationSingleton.GetInstance();
            DC = new DateConverter();
            TaskVm tvm = new TaskVm();

            TaskId = singleton.GetTaskId();
            //TaskId = singleton.GetTaskId();
            ////TaskName = tvm.SelectedItem.TaskName;//singleton.GetTaskName();
            //TaskSchedule = singleton.GetTaskSchedule();
            //TaskType = singleton.GetTaskType();
            //EquipmentId = singleton.GetEquipmentId();
            DueDate = DateTimeOffset.Now;
            ////DoneDate = _singleton.GetDoneDate();
            //EquipmentName = singleton.GetEquipmentName();
            //StationName = singleton.GetStationName();
            //Registration = singleton.GetRegistration();
            //Comment = singleton.GetComment();
            //DoneVar = singleton.GetDoneVar();

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
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }
        
        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
        
        public string TaskType
        {
            get => _taskType;
            set
            {
                _taskType = value;
                OnPropertyChanged(nameof(TaskType));
            }
        }

        public string TaskSchedule
        {
            get => _taskSchedule;
            set
            {
                _taskSchedule = value;
                OnPropertyChanged(nameof(TaskSchedule));
            }
        }

        public string Registration
        {
            get => _registration;
            set
            {
                _registration = value;
                OnPropertyChanged(nameof(Registration));
            }
        }

        public string DoneVar
        {
            get => _doneVar;
            set
            {
                _doneVar = value;
                OnPropertyChanged(nameof(DoneVar));
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
    }
}