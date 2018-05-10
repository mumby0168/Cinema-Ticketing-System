﻿using System;
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

        public List<Screening> GetFilmsOnDate(DateTime datetoGetwith)
        {
            string date = datetoGetwith.Date.ToString("dd/MM/yyyy");

            var screenings = m_DatabaseContext.Screenings.Include(f => f.Film).Include(s => s.Screen).ToList();

            return screenings.Where(e => e.DateAndTime.Date == datetoGetwith.Date).ToList();
        }
    }
}
