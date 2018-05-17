using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Popups;
using StationLogApp.Common;
using StationLogApp.Converters;
using StationLogApp.Interfaces;
using StationLogApp.Model;
using StationLogApp.Persistancy;
using StationLogApp.View;
using StationLogApp.ViewModel;

namespace StationLogApp.Handlers
{
    public class NoteHandler
    {
        private NoteVM _noteVM;
        private ISave<Notes> _savedNote = new SaveM<Notes>();
        private IDelete<Notes> _deleteNote = new DeleteM<Notes>();
        private DateConverter _dateConverter = new DateConverter();
        private Notes _noteObj;

        //public Notes SelectedNote
        //{
        //    get { return _noteVM.SelectedNote; }
        //}

        public NoteHandler(NoteVM noteVM)
        {
            _noteVM = noteVM;
        }

        public void CreateAndSaveNote()
        {
            DateTime convertedDate = _dateConverter.ConvertToDate(_noteVM.DueDate);
            int convertedStationName = _noteVM.SelectedStationItem.StationID;
            _noteObj = new Notes(_noteVM.NotesID, _noteVM.Note1, convertedDate, convertedStationName, _noteVM.UserID);
            _savedNote.Save(_noteObj, "Notes");
        }

        public void RemoveNote()
        {
            _deleteNote.Delete("Notes", _noteVM.SelectedNote.NotesID);
        }

        //public async void RefreshPage()
        //{
        //    FrameNavigateClass _frame = new FrameNavigateClass();
        //    _frame.ActivateFrameNavigation(typeof(TaskPage));
        //    MessageDialog msg = new MessageDialog("Note deleted!");
        //    await msg.ShowAsync();
        //}

        //public async void Wait()
        //{
        //    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(20));
        //    FrameNavigateClass _frame = new FrameNavigateClass();
        //    _frame.ActivateFrameNavigation(typeof(TaskPage));
        //    MessageDialog msg = new MessageDialog("Note deleted!");
        //    await msg.ShowAsync();
        //}

        //public void Final()
        //{
        //    RemoveNote();
        //    Wait();
        //}

        public static ObservableCollection<Station> LoadStations()
        {
            LoadM<Station> retrievedCatalog = new LoadM<Station>();
            Task<ObservableCollection<Station>> retrievedStationsTask = retrievedCatalog.Load("Stations");
            ObservableCollection<Station> collectionOfStations = retrievedStationsTask.Result;
            return collectionOfStations;
        }

        public ObservableCollection<Notes> LoadNotes()
        {

            ObservableCollection<Notes> list = new ObservableCollection<Notes>();

            ILoad<Notes> loadedNotes = new LoadM<Notes>();
            ObservableCollection<Notes> notesCollection = loadedNotes.RetriveCollection("Notes");

            var query = (from n in notesCollection
                select n).ToList();
            foreach (var item in query)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
