using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class TaskHandler:NotifyPropertyChangedClass
    {
        #region instance fields
        private ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private IUpdate<TaskClass> _updateTaskClass = new UpdateM<TaskClass>();

        private TaskVm _taskVm;
        private readonly FrameNavigateClass _frameNavigation;


        // Collections for Sorting Purposes

        private ObservableCollection<TaskEquipmentStation> _newLoadedCollection;

        private ObservableCollection<TaskEquipmentStation> _loadedCollection;
        #endregion

        #region properties
        public TaskClass SelectedTask
        {
            get { return _taskVm.SelectedItem; }
        }


        public ObservableCollection<TaskEquipmentStation> LoadedCollection
        {
            get { return _loadedCollection; }
            set
            {
                _loadedCollection = value;
                OnPropertyChanged(nameof(LoadedCollection));
            }
        }

        #region Constructor

        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
            _frameNavigation = new FrameNavigateClass();
        }

        #endregion

        #endregion

        #region Loading methods
        public static ObservableCollection<TaskEquipmentStation> LoadToDo()
        {
            ObservableCollection<TaskEquipmentStation> ltes = new ObservableCollection<TaskEquipmentStation>();

            ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
            ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

            var query = (from t in taskCollection
                         join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                         join s in stationCollection on e.StationID equals s.StationID
                         select new TaskEquipmentStation(){TaskId = t.TaskId, TaskName = t.TaskName, TaskSchedule = t.TaskSchedule, Registration = t.Registration, DueDate = t.DueDate, DoneDate = t.DoneDate, DoneVar = t.DoneVar, TaskType = t.TaskType, Comment = t.Comment, EquipmentID = t.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            foreach (var item in query)
            {
                if (item.DoneVar == "N")
                {
                    ltes.Add(item);
                }
            }
            return ltes;
        }


        public static ObservableCollection<TaskEquipmentStation> LoadDone()
        {
            ObservableCollection<TaskEquipmentStation> done = new ObservableCollection<TaskEquipmentStation>();

            ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
            ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

            ILoad<User> retrivedUser = new LoadM<User>();
            ObservableCollection<User> userCollection = retrivedUser.RetriveCollection("Users");

            var query = (from t in taskCollection
                join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                join s in stationCollection on e.StationID equals s.StationID
                select new TaskEquipmentStation() { TaskId = t.TaskId, TaskName = t.TaskName, TaskSchedule = t.TaskSchedule, Registration = t.Registration, DueDate = t.DueDate, DoneDate = t.DoneDate, DoneVar = t.DoneVar, TaskType = t.TaskType, Comment = t.Comment, EquipmentID = t.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            foreach (var item in query)
            {
                if (item.DoneVar == "Y")
                {
                    done.Add(item);
                }
            }
            return done;
        }


        public static ObservableCollection<TaskEquipmentStation> SortedCollection()
        {
            ObservableCollection<TaskEquipmentStation> sortList = new ObservableCollection<TaskEquipmentStation>();

            ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
            ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

            var query = (from t in taskCollection
                join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                join s in stationCollection on e.StationID equals s.StationID
                select new TaskEquipmentStation() { TaskId = t.TaskId, TaskName = t.TaskName, TaskSchedule = t.TaskSchedule, Registration = t.Registration, DueDate = t.DueDate, DoneDate = t.DoneDate, DoneVar = t.DoneVar, TaskType = t.TaskType, Comment = t.Comment, EquipmentID = t.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            //foreach (var item in query)
            //{
            //    if (LoadToDo().Contains(item.StationName))
            //    {
            //        sortList.Add(item);
            //    }
            //}

            return sortList;
        }

        public ObservableCollection<TaskEquipmentStation> LoadCollection()
        {
            _newLoadedCollection = new ObservableCollection<TaskEquipmentStation>();

            _newLoadedCollection.Clear();


            ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
            ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

            var query = (from t in taskCollection
                join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                join s in stationCollection on e.StationID equals s.StationID
                select new TaskEquipmentStation() { TaskName = t.TaskName, TaskType = t.TaskType, EquipmentName = e.EquipmentName, StationName = s.StationName, TaskSchedule = t.TaskSchedule, EquipmentID = e.EquipmentID }).ToList();

            foreach (var item in query)
            {
                _newLoadedCollection.Add(item);
            }

            _loadedCollection = _newLoadedCollection;
            return _loadedCollection;
        }

        public ObservableCollection<Station> LoadStation()
        {
            ILoad<Station> retrivedStation = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

            var query = (from s in stationCollection
                select new Station() { StationName = s.StationName, StationID = s.StationID }).ToList();

            foreach (var item in query)
            {
                stationCollection.Add(item);
            }
            return stationCollection;
        }
        #endregion

        #region SaveAsDoneTask
        // This method is activated by the button of the relayCommand  
        // and save the logged task and add a task to the next date that it has to be made
        public void OperateTask()
        {
            SaveTaskClass();
            //_frameNavigation.ActivateFrameNavigation(typeof(TaskPage));
        }
        
        public void SaveTaskClass()
        {
            DateTime loggedDate = DateTime.Now;
            TaskClass loggedTask = PostDoneTask(loggedDate, "Y");
        }
        
        private TaskClass PostDoneTask(DateTime newDate, string doneVariable)
        {
            if (SelectedTask.TaskId != 0)
            {
                TaskClass newReSchedule = new TaskClass(
                    _taskVm.SelectedItem.TaskId,
                    _taskVm.SelectedItem.TaskName,
                    _taskVm.SelectedItem.TaskSchedule,
                    _taskVm.SelectedItem.Registration,
                    _taskVm.SelectedItem.TaskType,
                    _taskVm.SelectedItem.DueDate,
                    newDate,
                    _taskVm.SelectedItem.Comment,
                    doneVariable,
                    _taskVm.SelectedItem.EquipmentID
                );

                _savedTaskClass.Save(newReSchedule, "Tasks");
                

                ReScheduleTask();

                MessageDialog msg = new MessageDialog("Task saved");
                msg.ShowAsync();

                return newReSchedule;
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the task!");
                msg.ShowAsync();
            }
            return null;
        }
        #endregion

        #region RescheduleTask
        public void ReScheduleTask()
        {
            if (_taskVm.SelectedItem.TaskSchedule != null)
            {
                if (_taskVm.SelectedItem.TaskSchedule == "Every week")
                {
                    DoRescheduleTask(7);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every two weeks")
                {
                    DoRescheduleTask(14);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every three weeks")
                {
                    DoRescheduleTask(21);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every month")
                {
                    DoRescheduleTask(28);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every two months")
                {
                    DoRescheduleTask(56);
                }

                else if (_taskVm.SelectedItem.TaskSchedule == "Every three months")
                {
                    DoRescheduleTask(84);
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Something went wrong, please try again");
                    msg.ShowAsync();
                }
            }
        }

        private void DoRescheduleTask(int periodicity)
        {
            DateTime nextTuesdayDate = GetNextDate(periodicity);
            TaskClass newReSchedule = CreateReScheduledTask(nextTuesdayDate, "N");
            _updateTaskClass.Update(newReSchedule, "Tasks", _taskVm.SelectedItem.TaskId);
        }
        
        private DateTime GetNextDate(int periodicity)
        {
            DateTime today = DateTime.Today;
            //int daysUntilNextTwoMonthlyTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + periodicity) % periodicity;
            DateTime nextTuesdayDate = today.AddDays(periodicity + 1);
            return nextTuesdayDate;
        }

        private TaskClass CreateReScheduledTask(DateTime newDate, string doneVariable)
        {
            TaskClass newReSchedule = new TaskClass(
                _taskVm.SelectedItem.TaskId,
                _taskVm.SelectedItem.TaskName,
                _taskVm.SelectedItem.TaskSchedule,
                _taskVm.SelectedItem.Registration = null,
                _taskVm.SelectedItem.TaskType,
                newDate,
                null,
                _taskVm.SelectedItem.Comment = null,
                doneVariable,
                _taskVm.SelectedItem.EquipmentID
            );
            return newReSchedule;
        }
        #endregion

        #region SortingMethods

        public void SortCollection()
        {

            _newLoadedCollection = new ObservableCollection<TaskEquipmentStation>();
            _newLoadedCollection.Clear();

            if (_taskVm.SelectedItemStation != null)
            {
                ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
                ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

                ILoad<Station> retrivedStation = new LoadM<Station>();
                ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

                ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
                ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

                var query = (from t in taskCollection
                    join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                    join s in stationCollection on e.StationID equals s.StationID
                    select new TaskEquipmentStation() { TaskName = t.TaskName, TaskType = t.TaskType, EquipmentName = e.EquipmentName, StationName = s.StationName, TaskSchedule = t.TaskSchedule, EquipmentID = e.EquipmentID }).ToList();

                foreach (var item in query)
                {
                    if (item.StationName == _taskVm.SelectedItemStation.StationName)
                    {
                        _newLoadedCollection.Add(item);
                    }
                }

                _loadedCollection = _newLoadedCollection;
                _taskVm.TaskCatalog = _loadedCollection;
            }

            else if (_taskVm.SelectedItemPeriodicity != null)
            {
                ILoad<TaskClass> retrivedTask = new LoadM<TaskClass>();
                ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

                ILoad<Station> retrivedStation = new LoadM<Station>();
                ObservableCollection<Station> stationCollection = retrivedStation.RetriveCollection("Stations");

                ILoad<Equipment> retrivedEquipmnet = new LoadM<Equipment>();
                ObservableCollection<Equipment> equipmentCollection = retrivedEquipmnet.RetriveCollection("Equipments");

                var query = (from t in taskCollection
                    join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                    join s in stationCollection on e.StationID equals s.StationID
                    select new TaskEquipmentStation() { TaskName = t.TaskName, TaskType = t.TaskType, EquipmentName = e.EquipmentName, StationName = s.StationName, TaskSchedule = t.TaskSchedule, EquipmentID = e.EquipmentID }).ToList();

                foreach (var item in query)
                {
                    if (item.TaskSchedule == _taskVm.SelectedItemPeriodicity)
                    {
                        _newLoadedCollection.Add(item);
                    }
                }
                _loadedCollection = _newLoadedCollection;
                _taskVm.TaskCatalog = _loadedCollection;
            }

        }

        #endregion
    }
}
