
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

            if (_tickets.Count == 0)
                return;

            _firsDate = _tickets[0].Screening.DateAndTime;

            if (_firsDate.DayOfWeek != DayOfWeek.Monday)
            {
                do
                {
                    _firsDate = _firsDate.AddDays(-1.0);
                } while (_firsDate.DayOfWeek != DayOfWeek.Monday);
            }

            _firsDate = _firsDate.AddHours(-_firsDate.Hour);
            _firsDate = _firsDate.AddMinutes(-_firsDate.Minute);
            _firsDate = _firsDate.AddSeconds(-_firsDate.Second);
            _firsDate = _firsDate.AddMilliseconds(-_firsDate.Millisecond);

            int index = 0;
            foreach (var ticket in _tickets)
            {
                if (_weeklyOverviews == null)
                {
                    _weeklyOverviews = new ObservableCollection<WeeklyOverview>();
                }

                if (_weeklyOverviews.Count == 0)
                {
                    _weeklyOverviews.Add(new WeeklyOverview());
                    _weeklyOverviews[index].WeekCommencing = _firsDate;
                }

                if (ticket.Screening.DateAndTime.Date < _firsDate)
                {
                    throw new Exception("This should never happen as it means were losing data");
                }

                var maxdate = _firsDate.AddDays(7.0);

                if (ticket.Screening.DateAndTime.Date < maxdate.Date && ticket.Screening.DateAndTime.Date >= _firsDate.Date)
                {
                    var itemInList = _weeklyOverviews[index];
                    int dayOfWeek = (int)ticket.Screening.DateAndTime.DayOfWeek;
                    //Mon: 1 - 0
                    //Tue: 2 - 1
                    //Wed: 3 - 2
                    //Thu: 4 - 3
                    //Fri: 5 - 4
                    //Sat: 6 - 5
                    //Sun: 0 - 6
                    //so sub 1 off of dow and then if it is -1 set it to six
                    dayOfWeek--;
                    if (dayOfWeek < 0)
                        dayOfWeek = 6;

                    itemInList.TotalTicketsSold++;
                    itemInList.TotalRevenue += ticket.Price;

                    itemInList.Details[dayOfWeek].TotalTicketsSold++;
                    itemInList.Details[dayOfWeek].TotalRevenue += ticket.Price;

                    switch (ticket.TicketType)
                    {
                        case TicketType.Adult:
                            itemInList.TotalAdultTickets++;
                            itemInList.Details[dayOfWeek].TotalAdultTickets++;
                            break;
                        case TicketType.Child:
                            itemInList.TotalChildTickets++;
                            itemInList.Details[dayOfWeek].TotalChildTickets++;
                            break;
                        case TicketType.Concession:
                            itemInList.TotalConncessionTickets++;
                            itemInList.Details[dayOfWeek].TotalConncessionTickets++;
                            break;
                    }                 
                }
                else
                {
                    _firsDate = ticket.Screening.DateAndTime;

                    if (_firsDate.DayOfWeek != DayOfWeek.Monday)
                    {
                        do
                        {
                            _firsDate = _firsDate.AddDays(-1.0);
                        } while (_firsDate.DayOfWeek != DayOfWeek.Monday);
                    }

                    _firsDate = _firsDate.AddHours(-_firsDate.Hour);
                    _firsDate = _firsDate.AddMinutes(-_firsDate.Minute);
                    _firsDate = _firsDate.AddSeconds(-_firsDate.Second);
                    _firsDate = _firsDate.AddMilliseconds(-_firsDate.Millisecond);

                    _weeklyOverviews.Add(new WeeklyOverview());
                    index = _weeklyOverviews.Count - 1;
                    _weeklyOverviews[index].WeekCommencing = _firsDate;

                    _weeklyOverviews[index].TotalTicketsSold++;
                    _weeklyOverviews[index].TotalRevenue += ticket.Price;

                    var itemInList = _weeklyOverviews[index];
                    int dayOfWeek = (int)ticket.Screening.DateAndTime.DayOfWeek;
                    //Mon: 1 - 0
                    //Tue: 2 - 1
                    //Wed: 3 - 2
                    //Thu: 4 - 3
                    //Fri: 5 - 4
                    //Sat: 6 - 5
                    //Sun: 0 - 6
                    //so sub 1 off of dow and then if it is -1 set it to six
                    dayOfWeek--;
                    if (dayOfWeek < 0)
                        dayOfWeek = 6;

                    itemInList.Details[dayOfWeek].TotalTicketsSold++;
                    itemInList.Details[dayOfWeek].TotalRevenue += ticket.Price;

                    switch (ticket.TicketType)
                    {
                        case TicketType.Adult:
                            _weeklyOverviews[index].TotalAdultTickets++;
                            itemInList.Details[dayOfWeek].TotalAdultTickets++;
                            break;
                        case TicketType.Child:
                            _weeklyOverviews[index].TotalChildTickets++;
                            itemInList.Details[dayOfWeek].TotalChildTickets++;
                            break;
                        case TicketType.Concession:
                            _weeklyOverviews[index].TotalConncessionTickets++;
                            itemInList.Details[dayOfWeek].TotalConncessionTickets++;
                            break;
                    }
                }
            }

            WeeklyOverviews = _weeklyOverviews;
        }
    }
}
