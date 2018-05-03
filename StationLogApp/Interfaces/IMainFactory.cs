using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Model;

namespace StationLogApp.Interfaces
{
    public interface IMainFactory
    {
        TaskClass Create();
    }
}
