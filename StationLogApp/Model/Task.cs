using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Model
{
    public class Task
    {
        
        // Fields

        private int _taskId;
        private string _taskName;
        private string _taskSchedule;
        private string _registration;
        private string _taskType;
        private DateTime _doneDate;
        private string _comment;
        private string _doneVar;


        // Properties

        public int TaskId { get => _taskId; set => _taskId = value; }
        public string TaskName { get => _taskName; set => _taskName = value; }
        public string TaskSchedule { get => _taskSchedule; set => _taskSchedule = value; }
        public string Registration { get => _registration; set => _registration = value; }
        public string TaskType { get => _taskType; set => _taskType = value; }
        public DateTime DoneDate { get => _doneDate; set => _doneDate = value; }
        public string Comment { get => _comment; set => _comment = value; }
        public string DoneVar { get => _doneVar; set => _doneVar = value; }


        // Constructors

        public Task(int taskId, string taskName, string taskSchedule, string registration, string taskType, DateTime doneDate, string comment, string doneVar)
        {
            _taskId = taskId;
            _taskName = taskName;
            _taskSchedule = taskSchedule;
            _registration = registration;
            _taskType = taskType;
            _doneDate = doneDate;
            _comment = comment;
            _doneVar = doneVar;
        }

        public Task()
        {
            
        }


        // Methods

        public override string ToString()
        {
            return $"{_taskId}, {_taskName}, {_taskSchedule}, {_registration}, {_taskType}, {_doneDate}, {_comment}, {_doneVar}";
        }
    }
}
