using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Model;

namespace StationLogApp.ViewModel
{
    public class NoteVM :NotifyPropertyChangedClass
    {
        private int _notesID;
        private string _note1;
        private int _stationID;
        private DateTimeOffset _dueDate;
        private int _userID;
        private ObservableCollection<Station> _stationCollection;
        private ButtonsVm _currentUser = new ButtonsVm();
        private Station _selectedNote;
        private Notes _notes;

        public ObservableCollection<Station> StationCollection
        {
            get
            {
                return _stationCollection;
            }
            set { _stationCollection = value; }
        }

        public int NotesID
        {
            get { return _notesID; }
            set
            {
                _notesID = value;
                OnPropertyChanged(nameof(NotesID));
            }
        }

        public string Note1
        {
            get
            {
                return _note1;
            }
            set
            {
                _note1 = value;
                OnPropertyChanged(nameof(Note1));
            }
        }

        public DateTimeOffset DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public int StationID
        {
            get { return _stationID; }
            set
            {
                _stationID = value;
                OnPropertyChanged(nameof(StationID));
            }
        }

        public int UserID
        {
            get { return _userID; }
            set
            {
                _userID = value;
                OnPropertyChanged(nameof(UserID));
            }
        }

        public Station SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
                SaveNote.RaiseCanExecuteChanged();
            }
        }

        public RelayCommandClass SaveNote { get; set; }


        public NoteHandler NoteHandler { get; set; }

        public NoteVM()
        {
            _dueDate = DateTimeOffset.Now;
            _stationCollection = NoteHandler.LoadStations();
            _selectedNote = SelectedNote;
            _userID = _currentUser.UserID;
            NoteHandler = new NoteHandler(this);
            SaveNote = new RelayCommandClass(NoteHandler.CreateAndSaveNote);
        }

    }
}
