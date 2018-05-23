
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.ViewModel;

namespace UnitTestStationLogApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    [TestClass]
    public class CollectionsTest
    {
        public Collections CollectionsClass { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            CollectionsClass = new Collections();
        }

        [TestMethod]
        public void TestLoadToDo()
        {
            int myValue = 43;
            int numberOfItems = CollectionsClass.LoadToDo().Count;

            Assert.AreEqual(myValue, numberOfItems); //passed
        }

        [TestMethod]
        public void TestLoadDone()
        {
            int myValue = 45;
            int realValue = CollectionsClass.LoadDone().Count;

            Assert.AreEqual(myValue, realValue); //passed
        }
    }

    [TestClass]
    public class SaveMTest
    {
        public TaskClass NewTask;
        public ISave<TaskClass> Save = new SaveM<TaskClass>();
        public Collections CollectionsClass { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            NewTask = new TaskClass(232, "taskNAmeUnitTest", "Every week", null, "check", DateTime.Now, null, null, "N", 4);
            CollectionsClass = new Collections();
        }

        [TestMethod]
        public void SavePersistancyTest()
        {
            Save.Save(NewTask, "Tasks");

            int myValue = 43;
            int realValue = CollectionsClass.LoadToDo().Count;

            Assert.AreEqual(myValue, realValue);   //Passed
        }
    }

    [TestClass]
    public class DeleteMTest
    {
        public TaskClass DeleteTask;
        public IDelete<TaskClass> Delete = new DeleteM<TaskClass>();
        public Collections CollectionsClass { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            DeleteTask = new TaskClass(233, "taskNAmeUnitTest", "Every week", null, "check", DateTime.Now, null, null, "N", 4);
            CollectionsClass = new Collections();
        }

        [TestMethod]
        public void DeletePersistancyTest()
        {
            Delete.Delete("Tasks", 232);

            int myValue = 42;
            int realValue = CollectionsClass.LoadToDo().Count;

            Assert.AreEqual(myValue, realValue);    //Passed
        }
    }

    [TestClass]
    public class UpdateMTest
    {
        public TaskClass UpdateTask;
        public IUpdate<TaskClass> Update = new UpdateM<TaskClass>();
        public Collections CollectionsClass { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            UpdateTask = new TaskClass(233, "updated name", "Every week", null, "check", DateTime.Now, null, null, "N", 4);
            CollectionsClass = new Collections();
        }

        [TestMethod]
        public void UpdatePersistancyTest()
        {
            Update.Update(UpdateTask, "Tasks", 233);

            string myValue = "updated name";
            string realValue = null;
            foreach (var item in CollectionsClass.LoadToDo())
            {
                if (item.TaskName == "updated name")
                {
                    realValue = item.TaskName;
                }
            } 

            Assert.AreEqual(myValue, realValue);    //passed
        }
    }

    [TestClass]
    public class NoteHandlerTest
    {
           
    }

    [TestClass]
    public class UnitTestDeleteTaskHandler
    {
        public DeleteTaskHandler DeleteTaskHandler { get; set; }
        public TaskVm TaskVm { get; set; }
        public Collections Collections { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            TaskVm = new TaskVm();
            DeleteTaskHandler = new DeleteTaskHandler(TaskVm);
            Collections = new Collections();
        }

        [TestMethod]
        public void TestDeleteTask()
        {
            TaskVm.SelectedItem.TaskId = 221;
            var b = TaskVm.SelectedItem != null;
            DeleteTaskHandler.DeleteTask(TaskVm.SelectedItem.TaskId);
            int myValue = 80;
            int realValue = Collections.LoadToDo().Count;
            Assert.AreNotEqual(myValue, realValue);

        }
    }
}