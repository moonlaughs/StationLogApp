using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Interfaces
{
   public interface ICreate<T>
   {
       Task Create(T obj);
    }
}
