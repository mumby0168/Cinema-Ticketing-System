using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Ticketing_System.Models.Application_Models
{
    public class DailyOverview
    {
        public DayOfWeek Day { get; set; }

        public int TotalTicketsSold { get; set; }

        public int TotalAdultTickets { get; set; }

        public int TotalChildTickets { get; set; }

        public int TotalConncessionTickets { get; set; }

        public double TotalRevenue { get; set; }
    }
}
