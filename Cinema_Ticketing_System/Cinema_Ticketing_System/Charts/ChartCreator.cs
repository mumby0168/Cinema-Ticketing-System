﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Models;
using OxyPlot.Series;

namespace Cinema_Ticketing_System.Charts
{
    public static class ChartCreator
    {

        public static PieSeries GetProportionOfTicketsPerScreeningPieChart(List<Ticket> tickets)
        {
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

    }
}