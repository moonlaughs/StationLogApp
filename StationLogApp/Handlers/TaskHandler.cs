﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
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

        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
        }

        // Methods 

        public static ObservableCollection<TaskEquipmentStation> LoadTaskEquipmentStations()
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
                         select new TaskEquipmentStation(){TaskName = t.TaskName, TaskType = t.TaskType, EquipmentName = e.EquipmentName, StationName = s.StationName }).ToList();

            foreach (var item in query)
            {
                ltes.Add(item);
            }

            return ltes;
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
            TaskClass loggedTask = CreateScheduledTask(loggedDate);
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


        private TaskClass CreateScheduledTask( DateTime newDate)
        {
            TaskClass newReSchedule = new TaskClass(
                _taskVm.SelectedTaskClass.TaskId,
                _taskVm.SelectedTaskClass.TaskName,
                _taskVm.SelectedTaskClass.TaskSchedule,
                _taskVm.SelectedTaskClass.Registration,
                _taskVm.SelectedTaskClass.TaskType,
                newDate,
                _taskVm.SelectedTaskClass.Comment,
                _taskVm.SelectedTaskClass.DoneVar,
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
            TaskClass newReSchedule = CreateScheduledTask(nextTuesdayDate);
            _savedTaskClass.Save(newReSchedule, "Tasks");
        }
    }
}
