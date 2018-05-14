
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Database;
using Cinema_Ticketing_System.Models;
using Cinema_Ticketing_System.Models.Application_Models;

namespace Cinema_Ticketing_System.ViewModels
{
    public class OverCinemaPerformanceViewModel : BaseViewModel
    {
        private ObservableCollection<WeeklyOverview> _weeklyOverviews;
        public ObservableCollection<WeeklyOverview> WeeklyOverviews
        {
            get => _weeklyOverviews;
            set
            {
                _weeklyOverviews = value;
                OnPropertyChanged();
                
            }
        }

        private List<Ticket> _tickets = new List<Ticket>();

        private DateTime _firsDate = new DateTime();

        public OverCinemaPerformanceViewModel()
        {
            //using (var handler = new DataHandler())
            //{
            //    _tickets = handler.GetAllTickets();
            //}

            //Sort();
        }

        //private void Sort()
        //{
        //    _tickets.OrderByDescending(d => d.Screening.DateAndTime.Date);

        //    _firsDate = _tickets[0].Screening.DateAndTime;

        //    if (_firsDate.DayOfWeek != DayOfWeek.Monday)
        //    {
        //        do
        //        {
        //            _firsDate.AddDays(1.0);
        //        } while (_firsDate.DayOfWeek != DayOfWeek.Monday);
        //    }

        //    foreach (var ticket in _tickets)
        //    {
        //        var indexOfList = 0;

        //        if (ticket.Screening.DateAndTime != _firsDate.AddDays(7))
        //        {
        //            _weeklyOverviews[indexOfList].TotalTicketsSold++;

        //            if(ticket.TicketType == TicketType.Adult)
        //                _weeklyOverviews[indexOfList].TotalAdultTickets++;
        //            if (ticket.TicketType == TicketType.Child)
        //                _weeklyOverviews[indexOfList].TotalChildTickets++;
        //            if (ticket.TicketType == TicketType.Concession)
        //                _weeklyOverviews[indexOfList].TotalConncessionTickets++;

        //            _weeklyOverviews[indexOfList].TotalRevenue += ticket.Price;

        //        }

        //        if (ticket.Screening.DateAndTime == _firsDate.AddDays(7))
        //        {
        //            _firsDate = _firsDate.AddDays(7);
        //            var count = _weeklyOverviews.Count;
        //            if (_weeklyOverviews.Count != 0)
        //                indexOfList = count - 1;
        //            _weeklyOverviews.Add(new WeeklyOverview() { WeekCommencing = _firsDate });
        //        }
                
        //    }


        //}
    }
}
