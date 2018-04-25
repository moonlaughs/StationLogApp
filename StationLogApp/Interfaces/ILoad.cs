using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Interfaces
{
    public interface ILoad<T>
    {
        Task<List<T>> Load();
    }
}
