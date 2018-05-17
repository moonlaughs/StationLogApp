﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.ViewModel
{
   public class VMContainer
   {
       private TaskVm _tVm = new TaskVm();
       private NoteVM _nVm = new NoteVM();

       public TaskVm TVm
       {
           get { return _tVm; }
           set
           {
               _tVm = value;
           }
       }

       public NoteVM NVm
       {
           get { return _nVm; }
           set
           {
               _nVm = value;
           }
       }
    }
}