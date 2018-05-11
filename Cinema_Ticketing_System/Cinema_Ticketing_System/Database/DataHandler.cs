using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public DataHandler()
        {
            m_DatabaseContext = new CinemaContext();

            if(m_DatabaseContext.Films.ToList().Count == 0)
            {
                PopulateFilms();
                m_DatabaseContext.SaveChanges();
            }
            
            if(m_DatabaseContext.Screens.ToList().Count == 0)
            {
                PopulateScreens();
                m_DatabaseContext.SaveChanges();
            }

            if (m_DatabaseContext.Screenings.Where(S =>
                    S.DateAndTime.Day == DateTime.Today.Day && S.DateAndTime.Month == DateTime.Today.Month &&
                    S.DateAndTime.Year == DateTime.Today.Year).ToList().Count == 0)
            {
                PopulateTodaysScreenings();
                m_DatabaseContext.SaveChanges();
            }
        }

        public void PopulateFilms()
        {
            Film film = new Film();
            film.Genre = Genre.Action;
            film.Name = "Die Hard";
            m_DatabaseContext.Films.Add(film);

            film = new Film();
            film.Genre = Genre.Comedy;
            film.Name = "Happy Gilmore";
            m_DatabaseContext.Films.Add(film);

            film = new Film();
            film.Genre = Genre.Drama;
            film.Name = "Interstellar";
            m_DatabaseContext.Films.Add(film);

            film = new Film();
            film.Genre = Genre.Thriller;
            film.Name = "Women In Black";
            m_DatabaseContext.Films.Add(film);
            
        }

        public void PopulateScreens()
        {
            Screen screen = new Screen();
            screen.Columns = 10;
            screen.Rows = 5;
            screen.Number = 50;

            m_DatabaseContext.Screens.Add(screen);

            screen = new Screen();
            screen.Columns = 10;
            screen.Rows = 5;
            screen.Number = 50;
            m_DatabaseContext.Screens.Add(screen);
        }

        public void PopulateTodaysScreenings()
        {
            Screening screening = new Screening();
            screening.ScreenId = m_DatabaseContext.Screens.ToList()[0].Id;
            screening.FilmId = m_DatabaseContext.Films.ToList()[0].Id;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening();
            screening.ScreenId = m_DatabaseContext.Screens.ToList()[1].Id;
            screening.FilmId = m_DatabaseContext.Films.ToList()[1].Id;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening();
            screening.ScreenId = m_DatabaseContext.Screens.ToList()[0].Id;
            screening.FilmId = m_DatabaseContext.Films.ToList()[2].Id;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening();
            screening.ScreenId = m_DatabaseContext.Screens.ToList()[1].Id;
            screening.FilmId = m_DatabaseContext.Films.ToList()[3].Id;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);
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
