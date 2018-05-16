using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class InfoHandler
    {
        private TaskVm _taskVm;

        public InfoHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
        }

        public async void Info()
        {
            if (_taskVm.SelectedItem.TaskId != 0)
            {
                string info =
                    $"Equipment name: {_taskVm.SelectedItem.EquipmentName} \nTaskId: {_taskVm.SelectedItem.TaskId} \nEquipmentId: {_taskVm.SelectedItem.EquipmentID} \nTask Schedule: {_taskVm.SelectedItem.TaskSchedule} \nStation name: {_taskVm.SelectedItem.StationName}";
                MessageDialog msg = new MessageDialog(info, "More Information");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Select item to see the details");
                await msg.ShowAsync();
            }
        }
    }
}
