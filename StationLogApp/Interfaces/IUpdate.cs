﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Interfaces
{
   public interface IUpdate<T>
    {
        Task Update(T obj, string apiID, int key);
    }
}
