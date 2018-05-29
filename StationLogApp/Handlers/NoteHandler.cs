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
        private readonly NoteVm _noteVm;
        private readonly ISave<Notes> _savedNote = new SaveM<Notes>();
        private readonly IDelete<Notes> _deleteNote = new DeleteM<Notes>();
        private readonly DateConverter _dateConverter = new DateConverter();
        private Notes _noteObj;

        public Collections Col { get; set; }
        public ObservableCollection<Station> StationCollection { get; set; }
        public ObservableCollection<Notes> NotesCollection { get; set; }

        public NoteHandler(NoteVm noteVm)
        {
            _noteVm = noteVm;
            Col = new Collections();
            StationCollection = Col.LoadStation();
            NotesCollection = Col.LoadNotes();
        }

        public async void CreateAndSaveNote()
        {
            if (!string.IsNullOrEmpty(_noteVm.Note1) && _noteVm.SelectedStationItem.StationName != null)
            {
                DateTime convertedDate = _dateConverter.ConvertToDate(_noteVm.DueDate);
                int convertedStationName = _noteVm.SelectedStationItem.StationId;
                _noteObj = new Notes(_noteVm.NotesId, _noteVm.Note1, convertedDate, convertedStationName, _noteVm.UserId);
                await _savedNote.Save(_noteObj, "Notes");
                FrameNavigateClass _frame = new FrameNavigateClass();
                _frame.ActivateFrameNavigation(typeof(MenuPage));
                MessageDialog msg = new MessageDialog("You just created a note!");
                await msg.ShowAsync();
            }
            else
            {
                var msg = new MessageDialog("Please fill in the information");
                await msg.ShowAsync();
            }
        }

        public async void RemoveNote()
        {
            if (_noteVm.SelectedNote.NotesId != 0)
            {
                await _deleteNote.Delete("Notes", _noteVm.SelectedNote.NotesId);
                FrameNavigateClass _frame = new FrameNavigateClass();
                _frame.ActivateFrameNavigation(typeof(MenuPage));
                MessageDialog msg = new MessageDialog("Note deleted!");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("Please select the note.");
                await msg.ShowAsync();
            }
        }
    }
}
