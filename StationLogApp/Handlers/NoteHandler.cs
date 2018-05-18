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

        public Collections Col { get; set; }
        public ObservableCollection<Station> StationCollection { get; set; }
        public ObservableCollection<Notes> NotesCollection { get; set; }

        public NoteHandler(NoteVM noteVM)
        {
            _noteVM = noteVM;
            Col = new Collections();
            StationCollection = Col.LoadStation();
            NotesCollection = Col.LoadNotes();
        }

        public async void CreateAndSaveNote()
        {
            DateTime convertedDate = _dateConverter.ConvertToDate(_noteVM.DueDate);
            int convertedStationName = _noteVM.SelectedStationItem.StationID;
            _noteObj = new Notes(_noteVM.NotesID, _noteVM.Note1, convertedDate, convertedStationName, _noteVM.UserID);
            await _savedNote.Save(_noteObj, "Notes");
            FrameNavigateClass _frame = new FrameNavigateClass();
            _frame.ActivateFrameNavigation(typeof(TaskPage));
            MessageDialog msg = new MessageDialog("You just created a note!");
            await msg.ShowAsync();
        }

        public async void RemoveNote()
        {
            await _deleteNote.Delete("Notes", _noteVM.SelectedNote.NotesID);
            FrameNavigateClass _frame = new FrameNavigateClass();
            _frame.ActivateFrameNavigation(typeof(TaskPage));
            MessageDialog msg = new MessageDialog("Note deleted!");
            await msg.ShowAsync();
        }
    }
}
