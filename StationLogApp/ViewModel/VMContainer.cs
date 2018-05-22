using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.ViewModel
{
   public class VmContainer
   {
       public TaskVm Tvm { get; set; } = new TaskVm();

       public NoteVm Nvm { get; set; } = new NoteVm();

       public ButtonsVm Bvm { get; set; } = new ButtonsVm();
   }
}