using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Models;
using Cinema_Ticketing_System.ViewModels;
using Cinema_Ticketing_System.Database;
using System.Windows;

namespace Cinema_Ticketing_System.ViewModels.Screen
{
    public class ScreenViewModel : BaseViewModel
    {
        public ScreenViewModel()
        {
            NumberOfColumns = 10;
            NumberOfRows = 10;
            ScreeningId = 1;
            if(ScreenVisibility == Visibility.Visible)
            {
                LoadDataFromDB();
            }
        }

        private int _ScreeningId = 0;
        public int ScreeningId
        {
            get
            {
                return _ScreeningId;
            }
            set
            {
                _ScreeningId = value;
                OnPropertyChanged();
            }
        }

        private int _NumberOfRows = 0;
        public int NumberOfRows
        {
            get
            {
                return _NumberOfRows;
            }
            set
            {
                _NumberOfRows = value;
                OnPropertyChanged();
            }
        }

        private int _NumberOfColumns = 0;
        public int NumberOfColumns
        {
            get
            {
                return _NumberOfColumns;
            }
            set
            {
                _NumberOfColumns = value;
                OnPropertyChanged();
            }
        }

        private Visibility _ScreenVisibility = Visibility.Visible;
        public Visibility ScreenVisibility
        {
            get
            {
                return _ScreenVisibility;
            }
            set
            {
                _ScreenVisibility = value;
                LoadDataFromDB();
                OnPropertyChanged();
            }
        }

        private void LoadDataFromDB()
        {
            using (DataHandler handle = new DataHandler())
            {
                ExsistingTickets = handle.GetTicketsWithScreenByScreeningID(ScreeningId);
            }
        }

        private List<Ticket> _ExsistingTickets = null;
        public List<Ticket> ExsistingTickets
        {
            get
            {
                return _ExsistingTickets;
            }
            set
            {
                _ExsistingTickets = value;
                OnPropertyChanged();
            }
        }

        private List<Ticket> _PendingTickets = null;
        public List<Ticket> PendingTickets
        {
            get
            {
                return _PendingTickets;
            }
            set
            {
                _PendingTickets = value;
                OnPropertyChanged();
            }
        }
    }
}
