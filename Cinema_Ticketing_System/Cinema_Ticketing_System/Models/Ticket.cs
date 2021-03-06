﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Ticketing_System.Models
{
    public enum TicketType
    {
        Adult,
        Child,
        Concession
    }

    public class Ticket
    {
        public int Id { get; set; }

        public TicketType TicketType { get; set; }

        public double Price { get; set; }

        [MaxLength(50)]
        public String SeatNumber { get; set; }

        public int RowNumber { get; set; }

        public int ColumnNumber { get; set; }

        public int ScreeningId { get; set; }

        public Screening Screening { get; set; }
    }
}
