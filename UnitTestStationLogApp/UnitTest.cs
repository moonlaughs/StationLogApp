
using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationLogApp.Common;
using StationLogApp.Converters;
using StationLogApp.Handlers;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.ViewModel;

namespace UnitTestStationLogApp
{
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
        public NoteVm NoteVm { get; set; }
        public NoteHandler NoteHandler { get; set; }
        public DateConverter DateConverter { get; set; }
        public ISave<Notes> Save = new SaveM<Notes>();
        public IDelete<Notes> Delete = new DeleteM<Notes>();
        public Collections Collection { get; set; }


        [TestInitialize]
        public void BeforeTest()
        {
            NoteVm = new NoteVm();
            NoteHandler = new NoteHandler(NoteVm);
            DateConverter = new DateConverter();
            Collection = new Collections();
        }

        [TestMethod]
        public void CreateAndSaveNoteTest()
        {
            NoteVm.Note1 = "blah";
            NoteVm.SelectedStationItem.StationName = "Anholt";
            NoteVm.DueDate = DateTimeOffset.Now;
            DateTime convertedDate = DateConverter.ConvertToDate(NoteVm.DueDate);
            NoteVm.SelectedStationItem.StationId = 6001;
            NoteVm.UserId = 1;
            Notes note = new Notes(NoteVm.NotesId, NoteVm.Note1, convertedDate, NoteVm.SelectedStationItem.StationId,
                NoteVm.UserId);
            Save.Save(note, "Notes");

            int myValue = 5;
            int realValue = Collection.LoadNotes().Count;

            Assert.AreEqual(myValue, realValue); //passed
        }

        [TestMethod]
        public void RemoveNoteTest()
        {
            NoteVm.SelectedNote.NotesId = 34;

            Delete.Delete("Notes", NoteVm.SelectedNote.NotesId);

            int myValue = 4;
            int realValue = Collection.LoadNotes().Count;

            Assert.AreEqual(myValue, realValue); //passed
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
            TaskVm.SelectedItem = new TaskEquipmentStation("SMPS", "HCAB");
        }

        //changed a little Didi's code and it delets the task but the test is still somehow not completed...
        [TestMethod]
        public void TestDeleteTask()
        {
            TaskVm.SelectedItem.TaskId = 230;
            DeleteTaskHandler.DeleteTask(TaskVm.SelectedItem.TaskId);
            int myValue = 42;
            int realValue = Collections.LoadToDo().Count;
            Assert.AreEqual(myValue, realValue);
        }
    }

    [TestClass]
    public class UnitTestCreateTaskHandler
    {
        public CreateTaskHandler CreateTaskHandler { get; set; }
        public CreateTaskVm Cvm { get; set; }
        public TaskClass CreateTask;
        public Collections Collections { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            CreateTask = new TaskClass(90, "new name", "Every week", null, "check", DateTime.Now, null, null, "N", 4);
            Cvm = new CreateTaskVm();
            CreateTaskHandler = new CreateTaskHandler(Cvm);
            Collections = new Collections();
        }

        [TestMethod]
        public void CreateTaskTest()
        {
            Cvm.NewItem.TaskName = CreateTask.TaskName;
            Cvm.NewItem.TaskSchedule = CreateTask.TaskSchedule;
            Cvm.NewItem.TaskType = CreateTask.TaskType;
            Cvm.NewItem.EquipmentId = CreateTask.EquipmentId;

            CreateTaskHandler.CreateTask();

            int myValue = 54;
            int realValue = Collections.LoadToDo().Count;

            Assert.AreEqual(myValue, realValue);
        }
    }
}