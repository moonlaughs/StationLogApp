using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;

namespace StationLogApp.Model
{
    public class TaskClass
    {

        #region // Properties
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskSchedule { get; set; }
        public string Registration { get; set; }
        public string TaskType { get; set; }
        public DateTime DoneDate { get; set; }
        public string Comment { get; set; }
        public string DoneVar { get; set; }
        public int EquipmentID { get; set; }
        #endregion

        #region // Constructors
        public TaskClass()
        {
            
        }

        public TaskClass(int taskId, string taskName, string taskSchedule, string registration, string taskType, DateTime doneDate, string comment, string doneVar, int equipmentID)
        {
            TaskId = taskId;
            TaskName = taskName;
            TaskSchedule = taskSchedule;
            Registration = registration;
            TaskType = taskType;
            DoneDate = doneDate;
            Comment = comment;
            DoneVar = doneVar;
            EquipmentID = equipmentID;
        }


        #endregion


        #region // ToString() Method
        public override string ToString()
        {
            return $"{TaskId}, {TaskName}, {TaskSchedule}, {Registration}, {TaskType}, {DoneDate}, {Comment}, {DoneVar}, {EquipmentID}";
        }
        #endregion
    }
}
