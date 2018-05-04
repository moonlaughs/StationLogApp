using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class TaskHandler
    {
        
        private ISave<TaskClass> _savedTaskClass = new SaveM<TaskClass>();
        private TaskVm _taskVm;

        public TaskClass SelectedTask
        {
            get { return _taskVm.SelectedTaskClass;}
        }

        public TaskHandler(TaskVm taskVm)
        {
            _taskVm = taskVm;
        }

        public static ObservableCollection<TaskClass> LoadCatalog()
        {
            LoadM<TaskClass> retrievedCatalog = new LoadM<TaskClass>();
            Task<ObservableCollection<TaskClass>> sth = retrievedCatalog.Load("Tasks");
            ObservableCollection<TaskClass> col = sth.Result;
            return col;
        }

        public void SaveTaskClass()
        {
            DateTime updatedDate = DateTime.Now;

            TaskClass becomeDoneTask = new TaskClass(
                _taskVm.SelectedTaskClass.TaskId, 
                _taskVm.SelectedTaskClass.TaskName, 
                _taskVm.SelectedTaskClass.TaskSchedule, 
                _taskVm.SelectedTaskClass.Registration, 
                _taskVm.SelectedTaskClass.TaskType, 
                updatedDate, 
                _taskVm.SelectedTaskClass.Comment, 
                _taskVm.SelectedTaskClass.DoneVar, 
                _taskVm.SelectedTaskClass.EquipmentID
                );
            
            _savedTaskClass.Save(becomeDoneTask, "Tasks");
        }
    }
}
