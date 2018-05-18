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
        private string _stationName;
        private DateTimeOffset _dueDate;
        private int _userID;
        private ObservableCollection<Station> _stationCollection;
        private ButtonsVm _currentUser = new ButtonsVm();
        private Station _selectedStationItem;
        private Notes _notes;
        private Notes _selectedNote;


        public ObservableCollection<Notes> NotesCatalog { get; set; }

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

        public string StationName
        {
            get { return _stationName; }
            set
            {
                _stationName = value;
                OnPropertyChanged(nameof(StationName));
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

        public Station SelectedStationItem
        {
            get { return _selectedStationItem; }
            set
            {
                _selectedStationItem = value;
                OnPropertyChanged();
                SaveNote.RaiseCanExecuteChanged();
            }
        }

        public Notes SelectedNote
        {
            get
            {
                return _selectedNote;
            }
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }

        public RelayCommandClass SaveNote { get; set; }
        public RelayCommandClass RemoveNote { get; set; }


        public NoteHandler NoteHandler { get; set; }

        public NoteVM()
        {
            NoteHandler = new NoteHandler(this);
            NotesCatalog = NoteHandler.NotesCollection;
            _dueDate = DateTimeOffset.Now;
            _stationCollection = NoteHandler.StationCollection;
            _selectedStationItem = SelectedStationItem;
            //_stationName = _selectedStationItem.StationName;
            _userID = _currentUser.UserID;
            SaveNote = new RelayCommandClass(NoteHandler.CreateAndSaveNote);
            SelectedNote = new Notes();
            RemoveNote = new RelayCommandClass(NoteHandler.RemoveNote);
        }

    }
}
