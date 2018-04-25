using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Interfaces
{
    public interface ITaskFactory
    {
        int TaskId { get; set; }
        string TaskName { get; set; }
        string TaskSchedule { get; set; }
        string Registration { get; set; }
        string TaskType { get; set; }
        DateTime DoneDate { get; set; }
        string Comment { get; set; }
        string DoneVar { get; set; }
    }
}
