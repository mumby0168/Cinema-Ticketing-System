using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Models;
using Cinema_Ticketing_System.ViewModels;
using Cinema_Ticketing_System.Database;

namespace Cinema_Ticketing_System.ViewModels.Screen
{
    class ScreenViewModel : BaseViewModel
    {
        public ScreenViewModel()
        {
            
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
        public List<Ticket> PendingTicket
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
