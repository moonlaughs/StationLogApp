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
    public class CreateTaskVM : NotifyPropertyChangedClass
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
        private TaskCatalogSingleton _userTaskCatalogSingleton;
        private TaskHandler _taskHandler;
        private Task _selectedItem;
        private ObservableCollection<TaskClass> _taskCollection;
        #endregion 

        //  public Task NewItem { get; set; }
        //public RelayCommand CreateTask { get; set; }
        //public RelayCommand DeleteTask { get; set; }
        //public RelayCommand NextPage { get; set; }
        #region Properties
        public ObservableCollection<TaskClass> TaskCollection
        {
            get { return _taskCollection; }
            set { _taskCollection = value; }
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

        public Task SelectedTask
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
#endregion
    }
}
