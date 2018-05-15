using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Ticketing_System.Models.Application_Models
{
    public class WeeklyOverview
    {
        public WeeklyOverview()
        {
            Details = new List<DailyOverview>();
            //Sunday is 0 but we want it to be last so skip it for now...
            for (int i = 1; i < 7; i++)
            {
                Details.Add(new DailyOverview() { Day = (DayOfWeek)i });
            }
            //..add it later
            Details.Add(new DailyOverview() { Day = DayOfWeek.Sunday });
        }

        public DateTime WeekCommencing { get; set; }

        public int TotalTicketsSold { get; set; }

        public int TotalAdultTickets { get; set; }

        public int TotalChildTickets { get; set; }

        public int TotalConncessionTickets { get; set; }

        public double TotalRevenue { get; set; }

        public List<DailyOverview> Details { get; set; }
    }
}
