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

       public NavigationHelperVm Bvm { get; set; } = new NavigationHelperVm();

       public CreateTaskVm Ctvm { get; set; } = new CreateTaskVm();

       public LoginVm Lvm { get; set; } = new LoginVm();

       public UpdateTaskVm Uvm { get; set; } = new UpdateTaskVm();
   }
}