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
        //private ICreate<TaskClass> _savedTaskClass = new CreateM<TaskClass>();

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
                         select new TaskEquipmentStation(){TaskId = t.TaskId, TaskName = t.TaskName, TaskSchedule = t.TaskSchedule, Registration = t.Registration, DoneDate = t.DoneDate, DoneVar = t.DoneVar, TaskType = t.TaskType, Comment = t.Comment, EquipmentID = t.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

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
                select new TaskEquipmentStation() { TaskId = t.TaskId, TaskName = t.TaskName, TaskSchedule = t.TaskSchedule, Registration = t.Registration, DoneDate = t.DoneDate, DoneVar = t.DoneVar, TaskType = t.TaskType, Comment = t.Comment, EquipmentID = t.EquipmentID, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            foreach (var item in query)
            {
                if (item.DoneVar == "Y")
                {
                    done.Add(item);
                }
            }

            return done;
        }

        // This method is activated by the button of the relayCommand  
        // and save the logged task and add a task to the next date that it has to be made
        public void OperateTask()
        {
            SaveTaskClass();
            ReScheduleTask();
        }


        public async void SaveTaskClass()
        {
            DateTime loggedDate = DateTime.Now;
            TaskClass loggedTask = CreateScheduledTask(loggedDate);
            await _savedTaskClass.Save(loggedTask, "Tasks");
            //await _savedTaskClass.Create(loggedTask);
            MessageDialog msg = new MessageDialog("Task saved");
            await msg.ShowAsync();
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

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two month")
                {
                    DateTime nextTuesdayDate = GetNexTuesdayDate(56);
                    TaskClass newReSchedule = CreateScheduledTask(nextTuesdayDate);
                    _savedTaskClass.Save(newReSchedule, "Tasks");
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two month")
                {
                    DoRescheduleTask(84);
                }
            }
        }


        private TaskClass CreateScheduledTask(DateTime newDate)
        {
            TaskClass newReSchedule = new TaskClass(
                taskId: _taskVm.SelectedTaskClass.TaskId,                  //??????
                taskName: _taskVm.SelectedTaskClass.TaskName,
                taskSchedule: _taskVm.SelectedTaskClass.TaskSchedule,
                registration: _taskVm.SelectedTaskClass.Registration,
                taskType: _taskVm.SelectedTaskClass.TaskType,
                doneDate: newDate,
                comment: _taskVm.SelectedTaskClass.Comment,
                doneVar: "Y",
                equipmentID: _taskVm.SelectedTaskClass.EquipmentID
            );

            return newReSchedule;
        }

        private DateTime GetNexTuesdayDate (int periodicity)
        {
            DateTime today = DateTime.Today;
            int daysUntilNextTwoMonthlyTuesday = ((int)DayOfWeek.Tuesday - (int)today.DayOfWeek + periodicity) % periodicity;
            DateTime nextTuesdayDate = today.AddDays(daysUntilNextTwoMonthlyTuesday);
            return nextTuesdayDate;
        }


        private void DoRescheduleTask(int periodicity)
        {
            DateTime nextTuesdayDate = GetNexTuesdayDate(periodicity);
            TaskClass newReSchedule = CreateScheduledTask(nextTuesdayDate);
            _savedTaskClass.Save(newReSchedule, "Tasks");
        }
    }
}
