using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Common
{
    public class TaskListSingleton
    {

        private static TaskListSingleton _instance;


        // List of Task 
        public ObservableCollection<Task> TaskList { get; set;}


        // Constructor of the Singleton

        private TaskListSingleton()
        {
            TaskList = new ObservableCollection<Task>();
            TaskList = new ObservableCollection<Task>(new Facade.GetTaskList());
        }


        // Singleton Method

        public static TaskListSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskListSingleton();
                }
                return _instance;
            }
        }
    }
}
