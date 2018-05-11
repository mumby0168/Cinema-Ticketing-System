using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;
using Cinema_Ticketing_System.Charts;
using Cinema_Ticketing_System.Database;
using Cinema_Ticketing_System.Models;
using OxyPlot;

namespace Cinema_Ticketing_System.ViewModels
{
    public class ChartLandingPageViewModel : BaseViewModel
    { 

        public ChartLandingPageViewModel()
        {
            DateChosenChart1 = DateTime.Now;
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

        #endregion

        #region Commands

        #endregion

        #region CommandMethods

        public void LoadTicketDataForScreening()
        {
            List<Ticket> tickets;
            using (var handler = new DataHandler())
            {
                tickets = handler.GetTicketsFromScreening(_chosenScreening.Id);
            }

            var series = ChartCreator.GetProportionOfTicketsPerScreeningPieChart(tickets);

            Model = new PlotModel();
            Model.Series.Add(series);
        }

        public void LoadFilledDataForScreening()
        {
            Screening Screening;
            List<Ticket> Tickets;
            int ticketCount = 0;
            using (var handler = new DataHandler())
            {
                Screening = handler.GetSecreeningWithScreenByScreeningId(_chosenScreening.Id);
                Tickets = handler.GetTicketsFromScreening(_chosenScreening.Id);
                ticketCount = Tickets.Count;
            }

            var series = ChartCreator.GetPercetentageChartOfScreeningFilled(Screening, ticketCount);

            Model = new PlotModel();
            Model.Series.Add(series);

        }

        public void LoadTicketProportionsForGenre()
        {

        }

        public void LoadTicketSoldOverTimePeriod()
        {

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
