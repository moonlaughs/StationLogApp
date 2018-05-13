using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class TaskHandler 
    {
        private ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private TaskVm _taskVm;
        

        public TaskClass SelectedTask
        {
            get { return _taskVm.SelectedTaskClass; }
        }


        public ObservableCollection<Station> StationCatalog
        {
            get { return LoadStation(); } 
        }


        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
        }
      
       
        public static ObservableCollection<TaskEquipmentStation> LoadTaskEquipmentStations()
        {
            ObservableCollection<TaskEquipmentStation> ltes = new ObservableCollection<TaskEquipmentStation>();

            ILoad<TaskClass> loadedTaskClass = new LoadM<TaskClass>();
            ObservableCollection<TaskClass> taskCollection = loadedTaskClass.RetrieveCollection("Tasks");

            ILoad<Station> loadedStationClass = new LoadM<Station>();
            ObservableCollection<Station> stationCollection = loadedStationClass.RetrieveCollection("Stations");

            ILoad<Equipment> loadedEquipmentClass = new LoadM<Equipment>();
            ObservableCollection<Equipment> equipmentCollection = loadedEquipmentClass.RetrieveCollection("Equipments");
            

            var query = (from t in taskCollection
                         join e in equipmentCollection on t.EquipmentID equals e.EquipmentID
                         join s in stationCollection on e.StationID equals s.StationID
                         select new TaskEquipmentStation() { TaskName = t.TaskName, TaskType = t.TaskType, EquipmentName = e.EquipmentName, StationName = s.StationName, TaskSchedule =  t.TaskSchedule, EquipmentID = e.EquipmentID }).ToList();

            foreach (var item in query)
            {
                ltes.Add(item);
            }
            return ltes;
        }


        public ObservableCollection<Station> LoadStation()
        {
           ILoad<Station> loadedStationClass = new LoadM<Station>();
           ObservableCollection<Station> stationCollection = loadedStationClass.RetrieveCollection("Stations");

            var query = (from s in stationCollection
                select new Station() { StationName = s.StationName, StationID = s.StationID}).ToList();

            foreach (var item in query)
            {
                stationCollection.Add(item);
            }
            return stationCollection;
        }


        // This method is activated by the button of the relayCommand  
        // and save the logged task and add a task to the next date that it has to be made
        public void OperateTask()
        {
            SaveTaskClass();
            ReScheduleTask();
        }


        public void SaveTaskClass()
        {
            DateTime loggedDate = DateTime.Now;
            TaskClass loggedTask = CreateScheduledTask(loggedDate, "Y");
            _savedTaskClass.Save(loggedTask, "Tasks");
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
                    DoRescheduleTask(56);
                }

                else if (_taskVm.SelectedTaskClass.TaskSchedule == "Every two month")
                {
                    DoRescheduleTask(84);
                }
            }
        }


        private TaskClass CreateScheduledTask(DateTime newDate, string doneVariable)
        {

            TaskClass newReSchedule = new TaskClass(
                _taskVm.SelectedTaskClass.TaskId,
                _taskVm.SelectedTaskClass.TaskName,
                _taskVm.SelectedTaskClass.TaskSchedule,
                _taskVm.SelectedTaskClass.Registration,
                _taskVm.SelectedTaskClass.TaskType,
                newDate,
                _taskVm.SelectedTaskClass.Comment,
                doneVariable,
                _taskVm.SelectedTaskClass.EquipmentID
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
            TaskClass newReSchedule = CreateScheduledTask(nextTuesdayDate, "N");
            _savedTaskClass.Save(newReSchedule, "Tasks");
        }
    }
}
