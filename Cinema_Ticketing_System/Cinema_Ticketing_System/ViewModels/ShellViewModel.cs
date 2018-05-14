using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.ViewModels.Commands;
using Cinema_Ticketing_System.ViewModels.Screen;
using Cinema_Ticketing_System.Views;

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

        private ViewAScreenPage _ViewAScreen;

        private OverCinemaPerformanceViewModel _overCinemaPerformance;


        public ClickCommand goToBook { get; private set; }

        public ClickCommand GoToCharts { get; private set; }

        public ClickCommand GoToViewAScreen { get; private set; }

        public ClickCommand GoToOverviewPage { get; private set; }

        public ShellViewModel()
        {            
            GoToOverviewPage = new ClickCommand(GoToOverCinemaPerformance);
            goToBook = new ClickCommand(BookTicketClicked);
            GoToCharts = new ClickCommand(ViewCharts);
            GoToViewAScreen = new ClickCommand(ViewAScreen);
            screenViewModel = new ScreenViewModel();
            homePageView = new HomePageViewModel();
            _ViewAScreen = new ViewAScreenPage();
            _overCinemaPerformance = new OverCinemaPerformanceViewModel();

            InitialTicketBookingFormViewModel = new InitialTicketBookingFormViewModel((screeningId, tickets) =>
                {
                    screenViewModel.ScreeningId = screeningId;
                    screenViewModel.PendingTickets = new ObservableCollection<Cinema_Ticketing_System.Models.Ticket>(tickets);
                    ChangeContextToScreen();
                });

            chartLandingPageViewModel = new ChartLandingPageViewModel();

            ViewModel = homePageView;

        }

        public void GoToOverCinemaPerformance()
        {
            ViewModel = _overCinemaPerformance;
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

        public void ViewAScreen()
        {
            ViewModel = _ViewAScreen;
        }
    }
}
