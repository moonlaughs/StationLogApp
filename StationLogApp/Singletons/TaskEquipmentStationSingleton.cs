using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;

namespace StationLogApp.Singletons
{
    public class TaskEquipmentStationSingleton
    {
        private static TaskEquipmentStation _taskEquipmentStation;

        private static TaskEquipmentStationSingleton Instance { get; set; }

        public TaskEquipmentStationSingleton()
        {
            _taskEquipmentStation = new TaskEquipmentStation();
        }

        public static TaskEquipmentStationSingleton GetInstance()
        {
            return Instance ?? (Instance = new TaskEquipmentStationSingleton());
        }

        public void SetTaskEquipmentStation(TaskEquipmentStation tes)
        {
            _taskEquipmentStation = tes;
        }

        public int GetTaskId()
        {
            return _taskEquipmentStation.TaskId;
        }

        public string GetTaskName()
        {
            return _taskEquipmentStation.TaskName;
        }

        public string GetTaskSchedule()
        {
            return _taskEquipmentStation.TaskSchedule;
        }

        public string GetRegistration()
        {
            return _taskEquipmentStation.Registration;
        }

        public string GetTaskType()
        {
            return _taskEquipmentStation.TaskType;
        }

        public DateTime GetDueDate()
        {
            return _taskEquipmentStation.DueDate;
        }

        public DateTime? GetDoneDate()
        {
            return _taskEquipmentStation.DoneDate;
        }

        public string GetComment()
        {
            return _taskEquipmentStation.Comment;
        }

        public string GetDoneVar()
        {
            return _taskEquipmentStation.DoneVar;
        }

        public int GetEquipmentId()
        {
            return _taskEquipmentStation.EquipmentId;
        }

        public string GetEquipmentName()
        {
            return _taskEquipmentStation.EquipmentName;
        }

        public string GetStationName()
        {
            return _taskEquipmentStation.StationName;
        }
    }
}