
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationLogApp.Common;
using StationLogApp.Converters;
using StationLogApp.Handlers;
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

    [TestClass]
    public class UnitTestCreateTaskHandler
    {
        public CreateTaskHandler CreateTaskHandler { get; set; }
        public CreateTaskVm CreateTaskVm { get; set; }
        public DateConverter DateConverter { get; set; }
        public Collections Collections { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            CreateTaskVm = new CreateTaskVm();
            CreateTaskHandler = new CreateTaskHandler(CreateTaskVm);
            DateConverter = new DateConverter();
            Collections = new Collections();
        }

        [TestMethod]
        public void TestCreateTask()
        {
            //CreateTaskVm.NewItem.TaskId = 
        }

    }
}
