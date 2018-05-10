using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.Models;
using System.Data.Entity;

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
            }
            
            if(m_DatabaseContext.Screens.ToList().Count == 0)
            {
                PopulateScreens();
            }

            if (m_DatabaseContext.Screenings.Where(S => S.DateAndTime.Date == DateTime.Today).ToList().Count == 0)
            {
                PopulateTodaysScreenings();
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
            m_DatabaseContext.Screens.Add(screen);
        }

        public void PopulateTodaysScreenings()
        {
            Screening screening = new Screening();
            screening.ScreenId = 1;
            screening.FilmId = 1;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening();
            screening.ScreenId = 2;
            screening.FilmId = 2;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening();
            screening.ScreenId = 1;
            screening.FilmId = 3;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);

            screening = new Screening();
            screening.ScreenId = 2;
            screening.FilmId = 4;
            screening.DateAndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);
            m_DatabaseContext.Screenings.Add(screening);
        }

        public List<Ticket> GetTicketsWithScreenByScreeningID(int iScreeningID)
        {
            return m_DatabaseContext.Tickets.Include(T => T.Screening).Where(T => T.ScreeningId == iScreeningID).ToList();
        }
    }
}
