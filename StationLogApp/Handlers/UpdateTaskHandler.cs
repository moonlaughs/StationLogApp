﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Automation.Peers;
using StationLogApp.Common;
using StationLogApp.Converters;
using StationLogApp.Convertor;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class UpdateTaskHandler : IUpdateTask
    {
        private readonly UpdateTaskVm _updateVm;
        private readonly IUpdate<TaskClass> _update = new UpdateM<TaskClass>();
        private ButtonsVm Bvm { get; }

        public DateConverter Dc { get; }
        public RelayCommandClass DoGoTask { get; set; }

        public UpdateTaskHandler(UpdateTaskVm updateVm)
        {
            _updateVm = updateVm;
            Bvm = new ButtonsVm();
            DoGoTask = new RelayCommandClass(GoTask);
            Dc = new DateConverter();
        }
        
        public async void UpdateTask()
        {
            if (_updateVm.TaskId != 0)
            {
                TaskClass updatedItem = new TaskClass(
                _updateVm.TaskId,
                _updateVm.TaskName,
                _updateVm.TaskSchedule,
                _updateVm.Registration,
                _updateVm.TaskType,
                Dc.ConvertToDate(_updateVm.DueDate),
                null,
                _updateVm.Comment,
                _updateVm.DoneVar,
                _updateVm.EquipmentId
                );

                await _update.Update(updatedItem, "Tasks", _updateVm.TaskId);

                GoTask();

                MessageDialog msg = new MessageDialog("Task updated");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select item");
                await msg.ShowAsync();
            }
        }

        public void GoTask()
        {
            Bvm.DoTask();
        }
    }
}
