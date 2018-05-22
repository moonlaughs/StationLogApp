using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Model;

namespace StationLogApp.Interfaces
{
    public interface ISave<T>
    {
        Task<T> Save(T obj, string apiId);
    }
}
