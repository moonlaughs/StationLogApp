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
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class TaskHandler
    {
        #region instance fields
        private ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();

        private TaskVm _taskVm;
        #endregion

        #region properties
        public TaskClass SelectedTask
        {
            get { return _taskVm.SelectedTaskClass; }
        }

        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
        }
        #endregion

        // Methods 

        #region Loading methods
        public static ObservableCollection<TaskEquipmentStation> LoadToDo()
        {
            ObservableCollection<TaskEquipmentStation> ltes = new ObservableCollection<TaskEquipmentStation>();

            LoadM<TaskClass> retrivedTask = new LoadM<TaskClass>();
            Task<ObservableCollection<TaskClass>> api = retrivedTask.Load("Tasks");
            ObservableCollection<TaskClass> taskCollection = api.Result;

            LoadM<Station> retrivedStation = new LoadM<Station>();
            Task<ObservableCollection<Station>> api2 = retrivedStation.Load("Stations");
            ObservableCollection<Station> stationCollection = api2.Result;

            LoadM<Equipment> retrivedEquipment = new LoadM<Equipment>();
            Task<ObservableCollection<Equipment>> api3 = retrivedEquipment.Load("Equipments");
            ObservableCollection<Equipment> equipmentCollection = api3.Result;
            
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
            //Task<ObservableCollection<TaskClass>> api = retrivedTask.Load("Tasks");
            ObservableCollection<TaskClass> taskCollection = retrivedTask.RetriveCollection("Tasks");

            LoadM<Station> retrivedStation = new LoadM<Station>();
            Task<ObservableCollection<Station>> api2 = retrivedStation.Load("Stations");
            ObservableCollection<Station> stationCollection = api2.Result;

            LoadM<Equipment> retrivedEquipment = new LoadM<Equipment>();
            Task<ObservableCollection<Equipment>> api3 = retrivedEquipment.Load("Equipments");
            ObservableCollection<Equipment> equipmentCollection = api3.Result;

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
        #endregion

        // This method is activated by the button of the relayCommand  
        // and save the logged task and add a task to the next date that it has to be made
        public void OperateTask()
        {
            SaveTaskClass();
            //ReScheduleTask();
        }


        public async void SaveTaskClass()
        {
            DateTime loggedDate = DateTime.Now;
            TaskClass loggedTask = PostDoneTask(loggedDate, "Y");
        }


        public void ReScheduleTask()
        {
            if (_taskVm.SelectedTaskClass.TaskSchedule != null)
            {
                if (_taskVm.SelectedTaskClass.TaskSchedule == "Every week")
                {
                    DoRescheduleTask(7);
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two weeks")
                {
                    DoRescheduleTask(14);
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every three weeks")
                {
                    DoRescheduleTask(21);
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every month")
                {
                    DoRescheduleTask(28);
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two months")
                {
                    DoRescheduleTask(56);
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every Three months")
                {
                    DoRescheduleTask(84);
                }
            }
        }


        private TaskClass PostDoneTask(DateTime newDate, string doneVariable)
        {
            if (SelectedTask.TaskId != 0)
            {
                TaskClass newReSchedule = new TaskClass(
                    _taskVm.SelectedTaskClass.TaskId,
                    _taskVm.SelectedTaskClass.TaskName,
                    _taskVm.SelectedTaskClass.TaskSchedule,
                    _taskVm.SelectedTaskClass.Registration,
                    _taskVm.SelectedTaskClass.TaskType,
                    _taskVm.SelectedTaskClass.DueDate,
                    newDate,
                    _taskVm.SelectedTaskClass.Comment,
                    doneVariable,
                    _taskVm.SelectedTaskClass.EquipmentID
                );

                _savedTaskClass.Save(newReSchedule, "Tasks");
                MessageDialog msg = new MessageDialog("Task saved");
                msg.ShowAsync();

                ReScheduleTask();

                return newReSchedule;
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the task!");
                msg.ShowAsync();
            }
            return null;
        }

        private TaskClass CreateReScheduledTask(DateTime newDate, string doneVariable)
        {
            if (SelectedTask.TaskId != 0)
            {
                TaskClass newReSchedule = new TaskClass(
                    _taskVm.SelectedTaskClass.TaskId,
                    _taskVm.SelectedTaskClass.TaskName,
                    _taskVm.SelectedTaskClass.TaskSchedule,
                    _taskVm.SelectedTaskClass.Registration = null,
                    _taskVm.SelectedTaskClass.TaskType,
                    newDate,
                    null,
                    _taskVm.SelectedTaskClass.Comment = null,
                    doneVariable,
                    _taskVm.SelectedTaskClass.EquipmentID
                );
                return newReSchedule;
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the task!");
                msg.ShowAsync();
            }
            return null;
        }

        private DateTime GetNexTuesdayDate (int periodicity)
        {
            DateTime today = DateTime.Today;
            //int daysUntilNextTwoMonthlyTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + periodicity) % periodicity;
            DateTime nextTuesdayDate = today.AddDays(periodicity);
            return nextTuesdayDate;
        }


        private void DoRescheduleTask(int periodicity)
        {
            DateTime nextTuesdayDate = GetNexTuesdayDate(periodicity);
            TaskClass newReSchedule = CreateReScheduledTask(nextTuesdayDate, "N");
            _savedTaskClass.Save(newReSchedule, "Tasks");
        }
    }
}
