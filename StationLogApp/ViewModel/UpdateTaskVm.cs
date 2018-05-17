using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Model;

namespace StationLogApp.ViewModel
{
    public class UpdateTaskVm : NotifyPropertyChangedClass
    {
        private DateTimeOffset _dueDate;

        private TaskVm _taskVm;

        public UpdateTaskVm(TaskVm taskVm)
        {
            _taskVm = new TaskVm();
        }

        public DateTimeOffset DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public TaskEquipmentStation SelectedItem
        {
            get { return _taskVm.SelectedItem; }
            set
            {
                _taskVm.SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
    }
}
