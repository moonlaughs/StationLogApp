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
    public class CrudVM : NotifyPropertyChangedClass
    {
        #region Instance fields
        private int _taskID;
        private string _taskName;
        private string _taskSchedule;
        private string _registration;
        private string _taskType;
        private DateTimeOffset _dueDate;
        private DateTimeOffset _doneDate;
        private string _comment;
        private string _doneVar;
        private int _equipmentID;
        private int _stationID;
        private string _stationName;
        private string _equipmentName;
        private TaskCatalogSingleton _userTaskCatalogSingleton;
        private TaskEquipmentStation _selectedItem;
        private ObservableCollection<TaskEquipmentStation> _taskCollection;
        #endregion

        #region Properties
        public TaskEquipmentStation SelectItem { get; set; }
        public RelayCommandClass CreateTask { get; set; }
        public RelayCommandClass DeleteTask { get; set; }
        public RelayCommandClass NextPage { get; set; }

        public TaskEquipmentStation SelectTask
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectTask));
            }
        }

        public TaskCatalogSingleton TaskSingleton
        {
            get { return _userTaskCatalogSingleton; }
            set
            {
                _userTaskCatalogSingleton = value;
                OnPropertyChanged(nameof(TaskSingleton));
            }
        }

        public ObservableCollection<TaskEquipmentStation> TasksCollection
        {
            get { return _taskCollection; }
            set
            {
                _taskCollection = value;
            }
        }

        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
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

        public string TaskType
        {
            get { return _taskType; }
            set
            {
                _taskType = value;
                OnPropertyChanged(nameof(TaskType));
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

        public int EquipmentID
        {
            get { return _equipmentID; }
            set
            {
                _equipmentID = value;
                OnPropertyChanged(nameof(EquipmentID));
            }
        }

        public int StationID
        {
            get { return _stationID; }
            set
            {
                _stationID = value;
                OnPropertyChanged(nameof(StationID));
            }
        }

        public string StationName
        {
            get { return _stationName; }
            set
            {
                _stationName = value;
                OnPropertyChanged(nameof(StationName));
            }
        }

        public string EquipmentName
        {
            get { return _equipmentName; }
            set
            {
                _equipmentName = value;
                OnPropertyChanged(nameof(EquipmentName));
            }
        }
#endregion

        public CrudVM()
        {
           
        }

        public void ToDelete()
        {
            
        }
    }
}
