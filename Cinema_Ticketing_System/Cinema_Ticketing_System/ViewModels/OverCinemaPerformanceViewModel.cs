
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
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
            using (var handler = new DataHandler())
            {
                _tickets = handler.GetAllTickets();
            }

            Sort();
        }

        private void Sort()
        {
            _tickets = _tickets.OrderBy(d => d.Screening.DateAndTime.Date).ToList();

            _firsDate = _tickets[0].Screening.DateAndTime;

            if (_firsDate.DayOfWeek != DayOfWeek.Monday)
            {
                do
                {
                    _firsDate = _firsDate.AddDays(-1.0);
                } while (_firsDate.DayOfWeek != DayOfWeek.Monday);
            }



            foreach (var ticket in _tickets)
            {
                int index = 0;

                if (_weeklyOverviews == null)
                {
                    _weeklyOverviews = new ObservableCollection<WeeklyOverview>();
                }

                if (_weeklyOverviews.Count == 0)
                {
                    _weeklyOverviews.Add(new WeeklyOverview());
                    index = _weeklyOverviews.Count - 1;
                    _weeklyOverviews[index].WeekCommencing = _firsDate;
                }

                if (ticket.Screening.DateAndTime.Date < _firsDate)
                {
                    continue;
                }

                var maxdate = _firsDate.AddDays(7.0);

                if (ticket.Screening.DateAndTime.Date < maxdate.Date && ticket.Screening.DateAndTime.Date >= _firsDate.Date)
                {
                    var itemInList = _weeklyOverviews[index];

                    itemInList.TotalTicketsSold++;
                    itemInList.TotalRevenue += ticket.Price;

                    switch (ticket.TicketType)
                    {
                        case TicketType.Adult:
                            itemInList.TotalAdultTickets++;
                            break;
                        case TicketType.Child:
                            itemInList.TotalChildTickets++;
                            break;
                        case TicketType.Concession:
                            itemInList.TotalConncessionTickets++;
                            break;
                    }

                }
                else
                {
                    _firsDate = ticket.Screening.DateAndTime;

                    _weeklyOverviews.Add(new WeeklyOverview());
                    index = _weeklyOverviews.Count - 1;
                    _weeklyOverviews[index].WeekCommencing = _firsDate;

                    _weeklyOverviews[index].TotalTicketsSold++;
                    _weeklyOverviews[index].TotalRevenue += ticket.Price;

                    switch (ticket.TicketType)
                    {
                        case TicketType.Adult:
                            _weeklyOverviews[index].TotalAdultTickets++;
                            break;
                        case TicketType.Child:
                            _weeklyOverviews[index].TotalChildTickets++;
                            break;
                        case TicketType.Concession:
                            _weeklyOverviews[index].TotalConncessionTickets++;
                            break;
                    }
                }
            }

            WeeklyOverviews = _weeklyOverviews;
        }
    }
}
