using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.ViewModels.Screen;

namespace Cinema_Ticketing_System.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        private object _viewModel;

        public object ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnPropertyChanged();
            }
        }


        private ScreenViewModel screenViewModel;

        private InitialTicketBookingFormViewModel InitialTicketBookingFormViewModel;





        public ShellViewModel()
        {            
            screenViewModel = new ScreenViewModel();

            InitialTicketBookingFormViewModel = new InitialTicketBookingFormViewModel((screeningId, tickets, visibilty) =>
                {
                    screenViewModel.ScreeningId = screeningId;
                    screenViewModel.PendingTickets = tickets;
                    screenViewModel.ScreenVisibility = visibilty;
                    ChangeContextToScreen();
                });

            ViewModel = InitialTicketBookingFormViewModel;

        }

        public void ChangeContextToScreen()
        {
            ViewModel = screenViewModel;
        }
    }
}
