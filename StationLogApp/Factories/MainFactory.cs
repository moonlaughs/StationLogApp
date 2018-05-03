using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using TaskClass = StationLogApp.Model.TaskClass;

namespace StationLogApp.Factories
{
    public class MainFactory : IMainFactory
    {
        public TaskClass Create()
        {
            return new TaskClass();
        }
    }
}
