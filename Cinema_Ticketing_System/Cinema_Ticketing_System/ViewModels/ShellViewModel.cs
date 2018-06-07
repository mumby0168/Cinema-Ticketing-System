using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.ViewModels.Commands;
using Cinema_Ticketing_System.ViewModels.Screen;
using Cinema_Ticketing_System.Views;
using System.Windows;
using System.Threading;

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

        private Visibility _visibility = Visibility.Visible;
        public Visibility visibility
        {
            get
            {
                return _visibility;
            }

            set
            {
                System.Diagnostics.Debug.WriteLine(value);
                _visibility = value;
                OnPropertyChanged("visibility");
            }
        }

        private ScreenViewModel screenViewModel;

        private InitialTicketBookingFormViewModel InitialTicketBookingFormViewModel;

        private ChartLandingPageViewModel chartLandingPageViewModel;

        private HomePageViewModel homePageView;

        private OverCinemaPerformanceViewModel _overCinemaPerformance;

        private ViewAScreeningViewModel _ViewAScreeningViewModel;


        public ClickCommand goToBook { get; private set; }
        public ClickCommand goToViewAScreening { get; private set; }

        public ClickCommand GoToCharts { get; private set; }

        public ClickCommand GoToViewAScreen { get; private set; }

        public ClickCommand GoToOverviewPage { get; private set; }

        public ShellViewModel()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                using (var handle = new Database.DataHandler())
                {
                    handle.GenerateData(7);
                }

                visibility = Visibility.Collapsed;
            }));

            t.Name = "Data Generator";
            t.Start();

            GoToOverviewPage = new ClickCommand(GoToOverCinemaPerformance);
            goToBook = new ClickCommand(BookTicketClicked);
            goToViewAScreening = new ClickCommand(ViewAScreen);
            GoToCharts = new ClickCommand(ViewCharts);
            GoToViewAScreen = new ClickCommand(ViewAScreen);
            screenViewModel = new ScreenViewModel();
            homePageView = new HomePageViewModel();
            _overCinemaPerformance = new OverCinemaPerformanceViewModel();

            InitialTicketBookingFormViewModel = new InitialTicketBookingFormViewModel((screeningId, tickets) =>
                {
                    screenViewModel.ScreeningId = screeningId;
                    screenViewModel.PendingTickets = new ObservableCollection<Cinema_Ticketing_System.Models.Ticket>(tickets);
                    ChangeContextToScreen();
                });

            _ViewAScreeningViewModel = new ViewAScreeningViewModel((screeningId) =>
            {
                screenViewModel.ScreeningId = screeningId;
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
            ViewModel = _ViewAScreeningViewModel;
        }
    }
}
