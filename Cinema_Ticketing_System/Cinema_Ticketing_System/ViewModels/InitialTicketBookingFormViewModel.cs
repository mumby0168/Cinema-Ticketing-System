using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cinema_Ticketing_System.Models;

namespace Cinema_Ticketing_System.ViewModels
{
    public class InitialTicketBookingFormViewModel : BaseViewModel
    {
        private Visibility _timesVisibilty;
        public Visibility Visibily
        {
            get => _timesVisibilty;
            set
            {
                _timesVisibilty = value;
                OnPropertyChanged();
            }
        }

        private Screening _screening;
        public Screening Screening
        {
            get => _screening;
            set
            {
                _screening = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDateTime;
        public DateTime SelectedDateTime { get => _selectedDateTime;
            set
            {
                _selectedDateTime = value;
                OnPropertyChanged();
            }
        }


        public InitialTicketBookingFormViewModel()
        {
            
        }
    }
}
