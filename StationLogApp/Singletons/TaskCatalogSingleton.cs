﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using StationLogApp.Factories;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;

namespace StationLogApp.Singletons
{
    public class TaskCatalogSingleton
    {

        private static TaskCatalogSingleton _instance;
        

        // List of Task

        public ObservableCollection<ITaskFactory> TaskCatalog { get; set; }


        // Constructor of the Singleton

        private TaskCatalogSingleton()
        {
            
            TaskCatalog = new ObservableCollection<ITaskFactory>(); 
            
            //ILoad<User> loaded = new LoadM<User>();
            //Task<ObservableCollection<User>> sth = loaded.Load();
            //await sth;
            //ObservableCollection<User> col = sth.Result;

           
            
            
            //TaskCatalog = new ObservableCollection<ITaskFactory>(retrievedCatalog.Load()); 
            TaskCatalog = retrievedCatalog.Load();
        }
        
        public static async Task<ObservableCollection<TaskClass>> LoadCatalog()
        {
            ILoad<TaskClass> retrievedCatalog = new LoadTask<TaskClass>();
            Task<ObservableCollection<TaskClass>> sth = retrievedCatalog.Load();
            await sth;
            ObservableCollection<TaskClass> col = sth.Result;
            
        }

        // Singleton Method

        public static TaskCatalogSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaskCatalogSingleton();
                }
                return _instance;
            }
        }
    }
}
