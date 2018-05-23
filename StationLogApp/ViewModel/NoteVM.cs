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
    public class NoteVm :NotifyPropertyChangedClass
    {
        private int _notesId;
        private string _note1;
        private int _stationId;
        private string _stationName;
        private DateTimeOffset _dueDate;
        private int _userId;
        private readonly NavigationHelperVm _currentUser = new NavigationHelperVm();
        private Station _selectedStationItem;
        private Notes _selectedNote;

        public RelayCommandClass SaveNote { get; set; }
        public RelayCommandClass RemoveNote { get; set; }
        
        public NoteHandler NoteHandler { get; set; }

        public ObservableCollection<Notes> NotesCatalog { get; }

        public ObservableCollection<Station> StationCollection { get; set; }

        public int NotesId
        {
            get => _notesId;
            set
            {
                _notesId = value;
                OnPropertyChanged(nameof(NotesId));
            }
        }

        public string Note1
        {
            get => _note1;
            set
            {
                _note1 = value;
                OnPropertyChanged(nameof(Note1));
            }
        }

        public DateTimeOffset DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public int StationId
        {
            get => _stationId;
            set
            {
                _stationId = value;
                OnPropertyChanged(nameof(StationId));
            }
        }

        public string StationName
        {
            get => _stationName;
            set
            {
                _stationName = value;
                OnPropertyChanged(nameof(StationName));
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        public Station SelectedStationItem
        {
            get => _selectedStationItem;
            set
            {
                _selectedStationItem = value;
                OnPropertyChanged();
                SaveNote.RaiseCanExecuteChanged();
            }
        }

        public Notes SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }

        public NoteVm()
        {
            NoteHandler = new NoteHandler(this);
            NotesCatalog = NoteHandler.NotesCollection;
            _dueDate = DateTimeOffset.Now;
            StationCollection = NoteHandler.StationCollection;
            _selectedStationItem = SelectedStationItem;
            _userId = _currentUser.UserId;
            SaveNote = new RelayCommandClass(NoteHandler.CreateAndSaveNote);
            SelectedNote = new Notes();
            RemoveNote = new RelayCommandClass(NoteHandler.RemoveNote);
            SelectedStationItem = new Station();
        }
    }
}