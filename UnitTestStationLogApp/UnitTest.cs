
using System;
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
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
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

            Assert.AreEqual(myValue, realValue);
        }

        [TestMethod]
        public void RemoveNoteTest()
        {
            NoteVm.SelectedNote.NotesId = 34;

            Delete.Delete("Notes", NoteVm.SelectedNote.NotesId);

            int myValue = 4;
            int realValue = Collection.LoadNotes().Count;

            Assert.AreEqual(myValue, realValue);
        }
    }
}
