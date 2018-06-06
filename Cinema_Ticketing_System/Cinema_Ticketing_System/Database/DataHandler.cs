using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Models;

namespace Cinema_Ticketing_System.Database
{
    class DataHandler : IDisposable
    {
        private CinemaContext m_DatabaseContext;
        void IDisposable.Dispose()
        {
            m_DatabaseContext.SaveChangesAsync();
            m_DatabaseContext.Dispose();
        }

        public List<Ticket> GetAllTickets()
        {
            return m_DatabaseContext.Tickets.Include(s => s.Screening).ToList();
        }

        public DataHandler()
        {
            m_DatabaseContext = new CinemaContext();
        }

        public void GenerateData()
        {
            if (m_DatabaseContext.Films.ToList().Count == 0)
            {
                PopulateFilms();
                m_DatabaseContext.SaveChanges();
            }

            if (m_DatabaseContext.Screens.ToList().Count == 0)
            {
                PopulateScreens();
                m_DatabaseContext.SaveChanges();
            }

            DateTime start = DateTime.Now.AddDays(-10);
            while (start.DayOfWeek != DayOfWeek.Monday)
            {
                start = start.AddDays(-1);
            }

            GenerateScreengins(start, DateTime.Now.AddDays(14));
            GenerateHistoricData(start, DateTime.Now.AddDays(5));

            m_DatabaseContext.SaveChanges();
        }

        

        public void GenerateHistoricData(DateTime start, DateTime end)
        {

            var screenings = m_DatabaseContext.Screenings.Include(s => s.Screen).ToList();
            var tickets = m_DatabaseContext.Tickets.ToList();


            if (start > end)
                throw new Exception("You plonker");

            var curr = start;
            while (curr < end)
            {
                foreach (Screening s in screenings.Where(S => S.DateAndTime.Day == curr.Day && S.DateAndTime.Month == curr.Month && S.DateAndTime.Year == curr.Year).ToList())
                {
                    if (tickets.Where(T => T.ScreeningId == s.Id).ToList().Count == 0)
                    {
                        PopulateScreening(s);
                    }
                }

                curr = curr.AddDays(1);
            }

            m_DatabaseContext.Tickets.AddRange(ticketsToAdd);
        }
        private List<Ticket> ticketsToAdd = new List<Ticket>();

        public void PopulateScreening(Screening s)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            var screen = s.Screen;// m_DatabaseContext.Screenings.Include(S => S.Screen).Where(S => S.Id == s.Id).FirstOrDefault().Screen;
            int num = rand.Next(10, screen.Columns * screen.Rows);
            int row = 1;
            int col = 1;

            for (int i = 0; i < num; i++)
            {
                List<Ticket> tickets = m_DatabaseContext.Tickets.Where(T => T.ScreeningId == s.Id).ToList();
                bool bAdd = false;

                do
                {
                    if (tickets.Count == 0)
                        bAdd = true;

                    row = rand.Next(0, screen.Rows);
                    col = rand.Next(0, screen.Columns);
                    if (tickets.Where(T => T.RowNumber == row && T.ColumnNumber == col).ToList().Count == 0)
                    {
                        bAdd = true;
                    }
                } while (bAdd == false);

                Ticket t = new Ticket {TicketType = (TicketType) rand.Next(0, 3)};
                switch (t.TicketType)
                {
                    case TicketType.Adult:
                        t.Price = 7.0;
                        break;
                    case TicketType.Child:
                        t.Price = 3.0;
                        break;
                    case TicketType.Concession:
                        t.Price = 5.0;
                        break;
                    default:
                        throw new Exception("Critical Error");
                }

                t.RowNumber = row;
                t.ColumnNumber = col;
                t.SeatNumber = "";
                t.SeatNumber += row + col;
                t.ScreeningId = s.Id;
                ticketsToAdd.Add(t);
            }

            return;
        }

        public void GenerateScreengins(DateTime start, DateTime end)
        {
            if (start > end)
                throw new Exception("You plonker");

            var curr = start;
            while (curr < end)
            {
                if (m_DatabaseContext.Screenings.Where(S => S.DateAndTime.Day == curr.Day && S.DateAndTime.Month == curr.Month && S.DateAndTime.Year == curr.Year).ToList().Count == 0)
                {
                    PopulateScreenings(curr);
                }
                curr = curr.AddDays(1);
            }
        }

        public Screen GetScreenByID(int iScreenId)
        {
            return m_DatabaseContext.Screens.First(s => s.Id == iScreenId);
        }

        public void PopulateFilms()
        { 
            Film film = new Film
            {
                Genre = Genre.Action,
                Name = "Die Hard"
            };
            m_DatabaseContext.Films.Add(film);

            film = new Film
            {
                Genre = Genre.Comedy,
                Name = "Happy Gilmore"
            };
            m_DatabaseContext.Films.Add(film);

            film = new Film
            {
                Genre = Genre.Drama,
                Name = "Interstellar"
            };
            m_DatabaseContext.Films.Add(film);

            film = new Film
            {
                Genre = Genre.Thriller,
                Name = "Women In Black"
            };
            m_DatabaseContext.Films.Add(film);
            
        }

        public void PopulateScreens()
        {
            Screen screen = new Screen
            {
                Columns = 10,
                Rows = 5,
                Number = 1
            };

            m_DatabaseContext.Screens.Add(screen);

            screen = new Screen
            {
                Columns = 10,
                Rows = 5,
                Number = 2
            };
            m_DatabaseContext.Screens.Add(screen);
        }

        public void PopulateScreenings(DateTime date)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Screening screening = new Screening();
            var films = m_DatabaseContext.Films.ToList();
            screening.ScreenId = m_DatabaseContext.Screens.ToList()[0].Id;
            screening.FilmId = films[rand.Next(0, films.Count)].Id;
            screening.DateAndTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening
            {
                ScreenId = m_DatabaseContext.Screens.ToList()[0].Id,
                FilmId = films[rand.Next(0, films.Count)].Id,
                DateAndTime = new DateTime(date.Year, date.Month, date.Day, 12, 0, 0)
            };
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening
            {
                ScreenId = m_DatabaseContext.Screens.ToList()[0].Id,
                FilmId = films[rand.Next(0, films.Count)].Id,
                DateAndTime = new DateTime(date.Year, date.Month, date.Day, 15, 0, 0)
            };
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening
            {
                ScreenId = m_DatabaseContext.Screens.ToList()[0].Id,
                FilmId = films[rand.Next(0, films.Count)].Id,
                DateAndTime = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0)
            };
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening
            {
                ScreenId = m_DatabaseContext.Screens.ToList()[1].Id,
                FilmId = films[rand.Next(0, films.Count)].Id,
                DateAndTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0)
            };
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening
            {
                ScreenId = m_DatabaseContext.Screens.ToList()[1].Id,
                FilmId = films[rand.Next(0, films.Count)].Id,
                DateAndTime = new DateTime(date.Year, date.Month, date.Day, 12, 0, 0)
            };
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening
            {
                ScreenId = m_DatabaseContext.Screens.ToList()[1].Id,
                FilmId = films[rand.Next(0, films.Count)].Id,
                DateAndTime = new DateTime(date.Year, date.Month, date.Day, 15, 0, 0)
            };
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening
            {
                ScreenId = m_DatabaseContext.Screens.ToList()[1].Id,
                FilmId = films[rand.Next(0, films.Count)].Id,
                DateAndTime = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0)
            };
            m_DatabaseContext.Screenings.Add(screening);
        }

        public Screening GetScreeningFromDateFilmTimeScreen(DateTime _Date, Screen _SelectedScreen, Film _SelectedFilm, DateTime _SelectedTime)
        {
            var l = m_DatabaseContext.Screenings.Where(S => S.ScreenId == _SelectedScreen.Id && S.FilmId == _SelectedFilm.Id).ToList();
            return l.Where(S => S.DateAndTime.Date == _Date && S.DateAndTime.TimeOfDay == _SelectedTime.TimeOfDay).First();
        }
        public List<Screening> GetFilmsOnDate(DateTime datetoGetwith)
        {
            string date = datetoGetwith.Date.ToString("dd/MM/yyyy");

            var screenings = m_DatabaseContext.Screenings.Include(f => f.Film).Include(s => s.Screen).ToList();

            return screenings.Where(e => e.DateAndTime.Date == datetoGetwith.Date).ToList();
        }
        public List<Ticket> GetTicketsWithScreenByScreeningID(int iScreeningID)
        {
            return m_DatabaseContext.Tickets.Include(T => T.Screening).Where(T => T.ScreeningId == iScreeningID).ToList();
        }

        public Screening GetSecreeningWithScreenByScreeningId(int iScreeningID)
        {
            return m_DatabaseContext.Screenings.Include(S => S.Screen).FirstOrDefault(S => S.Id == iScreeningID);
        }

        public List<Ticket> GetTicketsFromScreening(int id)
        {
            var tickets = m_DatabaseContext.Tickets.Include(s => s.Screening).Where(t => t.ScreeningId == id).ToList();

            if (tickets == null)
            {
                throw new Exception("No tickets found for that screening.");
            }

            return tickets;
        }

        public List<Screening> GetScreeningsOnDate(DateTime Date)
        {
            var screenings = m_DatabaseContext.Screenings.ToList();

            List<Screening> sOnDate = screenings.Where(s => s.DateAndTime.Date == Date.Date).ToList();

            return sOnDate;
        }

        public List<Screening> GetScreeningsWithScreenOnDate(DateTime Date)
        {
            var screenings = m_DatabaseContext.Screenings.Include(S => S.Screen).ToList();

            List<Screening> sOnDate = screenings.Where(s => s.DateAndTime.Date == Date.Date).ToList();

            return sOnDate;
        }

        public List<Screen> GetScreens()
        {
            return m_DatabaseContext.Screens.ToList();
        }

        public List<Film> GetFilmsInScreen(Screen s)
        {
            List<Film> films = new List<Film>();
            List<Screening> screenings = m_DatabaseContext.Screenings.Include(S => S.Film).Where(S => S.ScreenId == s.Id).ToList();

            for(int i = 0; i < screenings.Count; i++)
            {
                bool bAdd = true;
                if(films.Count > 0)
                {
                    for (int j = 0; j < films.Count; j++)
                    {
                        if(films[j] == screenings[i].Film)
                        {
                            bAdd = false;
                        }
                    }
                }

                if(bAdd)
                {
                    films.Add(screenings[i].Film);
                }
            }

            return films;
        }

        public List<DateTime> TimesFromScreensAndDate(DateTime dt, Screen s)
        {
            var ret = m_DatabaseContext.Screenings.Where(S => S.ScreenId == s.Id).ToList();
            List<DateTime> times = new List<DateTime>();

            foreach (var t in ret.Where(S => S.DateAndTime.Date == dt.Date))
            {
                times.Add(t.DateAndTime);
            }

            return times;
        }

        public void AddTicket(Ticket t)
        {
            m_DatabaseContext.Tickets.Add(t);
            m_DatabaseContext.SaveChanges();
        }

        public List<Ticket> GetAllTicketsFromGenre(Genre genre)
        {
            return m_DatabaseContext.Tickets.Include(s => s.Screening).Where(t => t.Screening.Film.Genre == genre)
                .ToList();
        }

        public Screening GetAScreening()
        {
            return m_DatabaseContext.Screenings.FirstOrDefault();
        }
    }
}
