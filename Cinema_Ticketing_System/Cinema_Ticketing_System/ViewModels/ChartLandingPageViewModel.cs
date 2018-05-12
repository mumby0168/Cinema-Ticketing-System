using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization.Configuration;
using Cinema_Ticketing_System.Charts;
using Cinema_Ticketing_System.Database;
using Cinema_Ticketing_System.Models;
using Cinema_Ticketing_System.ViewModels.Commands;
using OxyPlot;

namespace Cinema_Ticketing_System.ViewModels
{
    public class ChartLandingPageViewModel : BaseViewModel
    { 

        public ChartLandingPageViewModel()
        {
            DateChosenChart1 = DateTime.Now;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            InitCommands();
            using (var handler = new DataHandler())
            {
                ChosenScreening = handler.GetAScreening();
            }

            ChosenGenre = Genre.Action;

            Model.TitleHorizontalAlignment = TitleHorizontalAlignment.CenteredWithinView;
        }

        #region private members (not related to a property)

        #endregion

        #region Properties        

        private PlotModel _model = new PlotModel();
        public PlotModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dateChosenChart1;
        public DateTime DateChosenChart1
        {
            get => _dateChosenChart1;
            set
            {
                _dateChosenChart1 = value;
                DateChosen();
                OnPropertyChanged();
            }
        }

        private Screening _chosenScreening;
        public Screening ChosenScreening
        {
            get => _chosenScreening;
            set
            {
                _chosenScreening = value;
                Chart1Clicked.Execute(this);
                OnPropertyChanged();
            }
        }

        private DateTime _startDate;
        private DateTime _endDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        private List<Screening> _screenings;

        public List<Screening> Screenings
        {
            get => _screenings;
            set
            {
                _screenings = value;
                OnPropertyChanged();
            }
        }

        private Visibility _chart1DetailsVisibility;

        public Visibility Chart1DetailsVisibilty
        {
            get => _chart1DetailsVisibility;
            set
            {
                _chart1DetailsVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _chart2DetailsVisibility;

        public Visibility Chart2DetailVisibility
        {
            get => _chart2DetailsVisibility;
            set
            {
                _chart2DetailsVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _chart3Visibility;
        public Visibility Chart3Visibility { get => _chart3Visibility;
            set
            {
                _chart3Visibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _chart4Visibility;
        public Visibility Chart4Visibility
        {
            get => _chart4Visibility;
            set
            {
                _chart4Visibility = value;
                OnPropertyChanged();
            }
        }
        public List<string> Genres
        {
            get { return Enum.GetNames(typeof(Genre)).ToList(); }
        }

        private Genre _chosenGenre;
        public Genre ChosenGenre
        {
            get => _chosenGenre;
            set
            {
                _chosenGenre = value;
                Chart3Clicked.Execute(this);
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        public ClickCommand Chart1Clicked { get; private set; }        
        
        public ClickCommand Chart2Clicked { get; private set; }

        public ClickCommand Chart3Clicked { get; private set; }

        public ClickCommand Chart4Clicked { get; private set; }
        #endregion

        #region CommandMethods

        public void InitCommands()
        {
            Chart1Clicked = new ClickCommand(LoadTicketDataForScreening);
            Chart2Clicked = new ClickCommand(LoadFilledDataForScreening);
            Chart3Clicked = new ClickCommand(LoadTicketProportionsForGenre);
            Chart4Clicked = new ClickCommand(LoadTicketSoldOverTimePeriod);
        }

        public void LoadTicketDataForScreening()
        {
            Chart1DetailsVisibilty = Visibility.Visible;
            Chart2DetailVisibility = Visibility.Collapsed;
            Chart3Visibility = Visibility.Collapsed;
            Chart4Visibility = Visibility.Collapsed;


            List<Ticket> tickets = new List<Ticket>();
            using (var handler = new DataHandler())
            {
                tickets = new List<Ticket>(handler.GetTicketsFromScreening(_chosenScreening.Id));
            }

            var series = ChartCreator.GetProportionOfTicketsPerScreeningPieChart(tickets);

            Model = new PlotModel();
            Model.Series.Add(series);
            Model.InvalidatePlot(true);
        }

        public void LoadFilledDataForScreening()
        {

            Chart1DetailsVisibilty = Visibility.Collapsed;
            Chart2DetailVisibility = Visibility.Visible;
            Chart3Visibility = Visibility.Collapsed;
            Chart4Visibility = Visibility.Collapsed;
            Screening Screening = null;
            List<Ticket> Tickets;
            int ticketCount = 0;
            using (var handler = new DataHandler())
            {
                if (_chosenScreening != null)
                {
                    Screening = handler.GetSecreeningWithScreenByScreeningId(_chosenScreening.Id);
                    Tickets = handler.GetTicketsFromScreening(_chosenScreening.Id);
                    ticketCount = Tickets.Count;
                }
            }
            
            var series = ChartCreator.GetPercetentageChartOfScreeningFilled(Screening, ticketCount);

            Model = new PlotModel();
            Model.Series.Add(series);

            Model.InvalidatePlot(true);

        }

        public void LoadTicketProportionsForGenre()
        {
            Chart1DetailsVisibilty = Visibility.Collapsed;
            Chart2DetailVisibility = Visibility.Collapsed;
            Chart3Visibility = Visibility.Visible;
            Chart4Visibility = Visibility.Collapsed;
            var series = ChartCreator.GetTicketTypeSplitOfGenre(_chosenGenre);

            Model = new PlotModel();
            Model.Series.Add(series);
            Model.InvalidatePlot(true);

        }

        public void LoadTicketSoldOverTimePeriod()
        {
            Chart1DetailsVisibilty = Visibility.Collapsed;
            Chart2DetailVisibility = Visibility.Collapsed;
            Chart3Visibility = Visibility.Collapsed;
            Chart4Visibility = Visibility.Visible;
        }

        #endregion

        #region METHODS

        public void DateChosen()
        {
            using (var handler = new DataHandler())
            {
                Screenings = handler.GetScreeningsOnDate(_dateChosenChart1);
            }
        }

        #endregion

    }
}
