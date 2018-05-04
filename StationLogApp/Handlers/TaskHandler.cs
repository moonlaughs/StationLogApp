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
        private ISave<TaskClass> _savedTaskClass;
        private TaskVm _taskVm;

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


            //LoadM<Station> retrivedStationCatalog = new LoadM<Station>();
            //Task<ObservableCollection<Station>> st = retrivedStationCatalog.Load("Stations");
            //ObservableCollection<Station> stations = st.Result;

            //LoadM<Equipment> retrivedEquipment = new LoadM<Equipment>();
            //Task<ObservableCollection<Equipment>> eq = retrivedEquipment.Load("Equipments");
            //ObservableCollection<Equipment> equipments = eq.Result;

            //var query = from task in col
            //            join e in equipments
            //            on task.

        }


        public void SaveTaskClass()
        {
            _savedTaskClass.Save(_taskVm.SelectedEvent, "Tasks");
        }
    }
}
