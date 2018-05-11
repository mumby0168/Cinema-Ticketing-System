using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.ViewModels.Commands;
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

        private ChartLandingPageViewModel chartLandingPageViewModel;

        private HomePageViewModel homePageView;


        public ClickCommand goToBook { get; private set; }

        public ClickCommand GoToCharts { get; private set; }

        public ShellViewModel()
        {            

            goToBook = new ClickCommand(BookTicketClicked);
            GoToCharts = new ClickCommand(ViewCharts);
            screenViewModel = new ScreenViewModel();
            homePageView = new HomePageViewModel();

            InitialTicketBookingFormViewModel = new InitialTicketBookingFormViewModel((screeningId, tickets) =>
                {
                    screenViewModel.ScreeningId = screeningId;
                    screenViewModel.PendingTickets = new ObservableCollection<Cinema_Ticketing_System.Models.Ticket>(tickets);
                    ChangeContextToScreen();
                });

            chartLandingPageViewModel = new ChartLandingPageViewModel();

            ViewModel = homePageView;

        }

        public void ChangeContextToScreen()
        {
            ViewModel = screenViewModel;
        }

        public void BookTicketClicked()
        {
            ViewModel = InitialTicketBookingFormViewModel;
        }

        public void ViewCharts()
        {
            ViewModel = chartLandingPageViewModel;
        }
    }
}
