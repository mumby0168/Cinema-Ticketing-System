using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Database;
using Cinema_Ticketing_System.Models;
using OxyPlot.Series;

namespace Cinema_Ticketing_System.Charts
{
    public static class ChartCreator
    {

        public static PieSeries GetTicketSoldAcrossADate(DateTime date1, DateTime date2)
        {

            List<Ticket> ticket = new List<Ticket>();

            using (var handler = new DataHandler())
            {
                ticket = handler.GetAllTickets();
            }

            sortedticke



                return new PieSeries();
        }

        public static PieSeries GetProportionOfTicketsPerScreeningPieChart(List<Ticket> tickets)
        {

            if (tickets.Count == 0)
            {
                var pieseries = new PieSeries()
                {
                    Title = "No Data"
                };

                return pieseries;
            }

            var pieSeries = new PieSeries
            {
                Title = "Propertions of ticket types for screening on: " + tickets[0].Screening.DateAndTime +
                        "watching: " + tickets[0].Screening.Film.Name
            };


            int AdultCount = 0;
            int ChildCount = 0;
            int ConcessionCount = 0;

            foreach (var ticket in tickets)
            {
                if (ticket.TicketType == TicketType.Adult)
                {
                    AdultCount++;
                }

                if (ticket.TicketType == TicketType.Child)
                    ChildCount++;
                if (ticket.TicketType == TicketType.Concession)
                    ConcessionCount++;
            }

            pieSeries.Slices.Add(new PieSlice("Adults", AdultCount));
            pieSeries.Slices.Add(new PieSlice("Childs", ChildCount));
            pieSeries.Slices.Add(new PieSlice("Conccesions", ConcessionCount));

            return pieSeries;
        }

        public static PieSeries GetPercetentageChartOfScreeningFilled(Screening screening, int ticketCount)
        {
            var pieSeries = new PieSeries()
            {
                Title = "Percentage Filled"
            };

            var screenCapacity = screening.Screen.Rows * screening.Screen.Columns;


            pieSeries.Slices.Add(new PieSlice("Filled", ticketCount));
            pieSeries.Slices.Add(new PieSlice("Empty", screenCapacity));

            return pieSeries;
        }

        public static PieSeries GetTicketTypeSplitOfGenre(Genre genre)
        {
            var series = new PieSeries()
            {
                Title = "Spread of ticket types Across Genres"
            };
            List<Ticket> TicketsofGenre;
            using (var handler = new DataHandler())
            {
                TicketsofGenre = new List<Ticket>(handler.GetAllTicketsFromGenre(genre));
            }

            series.Slices.Add(new PieSlice("Adults", TicketsofGenre.Count(t => t.TicketType == TicketType.Adult)));
            series.Slices.Add(new PieSlice("Childs", TicketsofGenre.Count(t => t.TicketType == TicketType.Child)));
            series.Slices.Add(new PieSlice("Concessions", TicketsofGenre.Count(t => t.TicketType == TicketType.Concession)));

            return series;
        }

    }
}
