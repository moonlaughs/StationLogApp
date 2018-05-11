using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;
using StationLogApp.Common;
using StationLogApp.Handlers;
using StationLogApp.Model;

namespace StationLogApp.ViewModel
{
    public class NoteVM : NotifyPropertyChangedClass
    {
        private string _note;
        private int _stationID;
        private DateTimeOffset _dueDate;
        private int _userID;
        private ObservableCollection<Station> _stationCollection;
        
        public ObservableCollection<Station> StationCollection
        {
            get
            {
                return _stationCollection;
            }
            set { _stationCollection = value; }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value; 
                OnPropertyChanged(nameof(Note));
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

        public DateTimeOffset DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
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

        public RelayCommandClass SaveNote { get; set; }
        public NoteHandler NoteHandler { get; set; }

        public NoteVM()
        {
            NoteHandler = new NoteHandler(this);
            SaveNote = new RelayCommandClass(NoteHandler.CreateNote);
            _stationCollection = NoteHandler.LoadStations();
        }

    }
}
