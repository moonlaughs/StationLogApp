using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Interfaces;
using StationLogApp.Model;

namespace StationLogApp.ViewModel
{
    public class DeleteTaskVm
    {
        private IDeleteTask DeleteTaskHandler { get; }

        public TaskEquipmentStation SelectedItem { get; set; }

        public RelayCommandClass DoDeleteTask { get; set; }

        public DeleteTaskVm()
        {
            DeleteTaskHandler = new DeleteClassHandler(this);
            SelectedItem = new TaskEquipmentStation();
            DoDeleteTask = new RelayCommandClass(DeleteTaskHandler.DeleteTask);

        }
    }
}
