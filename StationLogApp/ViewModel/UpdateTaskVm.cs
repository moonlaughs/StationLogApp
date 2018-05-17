using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Model;
using StationLogApp.Singletons;

namespace StationLogApp.ViewModel
{
    public class UpdateTaskVm : NotifyPropertyChangedClass
    {
        private DateTimeOffset _dueDate;
        private TaskEquipmentStationSingleton _singleton;
        private TaskVm _taskVm;

        public ObservableCollection<TaskEquipmentStation> EquipmentStations { get; set; }

        public string[] TaskTypes { get; set; }
        public string[] TaskSchedules { get; set; }

        public UpdateTaskVm()
        {
            _taskVm = new TaskVm();
            var col = new Collections();
            TaskTypes = col.TypeArray;
            TaskSchedules = col.ScheduleArray;
            EquipmentStations = col.EquipmentStationsCollection();
            SelectedItem = new TaskEquipmentStation();
            _singleton = TaskEquipmentStationSingleton.GetInstance();

            TaskName = _singleton.GetTaskName();
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

        public string TaskName
        {
            get { return _taskVm.SelectedItem.TaskName; }
            set
            {
                _taskVm.SelectedItem.TaskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
    }
}
