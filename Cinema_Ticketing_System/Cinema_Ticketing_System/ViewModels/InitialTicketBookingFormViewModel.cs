using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Cinema_Ticketing_System.Database;
using Cinema_Ticketing_System.Models;

namespace Cinema_Ticketing_System.ViewModels
{
    public class InitialTicketBookingFormViewModel : BaseViewModel
    {

        private List<Screening> _currentPossibleScreenings = new List<Screening>();

        private int _adultTickets;
        public int AdultTickets
        {
            get => _adultTickets;
            set
            {
                _adultTickets = value;
                OnPropertyChanged();
            }
        }

        private int _childTickets;

        public int ChildTickets
        {
            get => _childTickets;
            set
            {
                _childTickets = value;
                OnPropertyChanged();
            }
        }

        private int _concessionTickets;
        public int ConcessionTickets
        {
            get => _concessionTickets;
            set
            {
                _concessionTickets = value;
                OnPropertyChanged();
            } }

        private List<Film> _films;
        public List<Film> Films
        {
            get => _films;
            set
            {
                _films = value;
                OnPropertyChanged();
            }
        }

        private List<DateTime> _filmTimes;
        public List<DateTime> FilmTimes
        {
            get => _filmTimes;
            set
            {
                _filmTimes = value;
                OnPropertyChanged();
            }
        }

        private Visibility _timesVisibilty;
        public Visibility TimesVisibility
        {
            get => _timesVisibilty;
            set
            {
                _timesVisibilty = value;
                OnPropertyChanged();
            }
        }

        private Screening _screening;
        public Screening Screening
        {
            get => _screening;
            set
            {
                _screening = value;
                OnPropertyChanged();
            }
        }        

        private DateTime _selectedDateTime;
        public DateTime SelectedDateTime { get => _selectedDateTime;
            set
            {
                _selectedDateTime = value;    
                                SetFilmsOnDate();
                OnPropertyChanged();
            }
        }

        private DateTime _selectedFilmTime;
        public DateTime FilmTimeSelected
        {
            get => _selectedFilmTime;
            set
            {
                _selectedFilmTime = value;
                OnPropertyChanged();
            }
        }

        private Film _selectedFilm;

        public Film SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                SetFilmTimes();
                OnPropertyChanged();
            }
        }

        public void SetFilmsOnDate()
        {

            using (var handler = new DataHandler())
            {
                _currentPossibleScreenings = handler.GetFilmsOnDate(_selectedDateTime);
            }

            var films = new List<Film>();
            foreach (var currentPossibleScreening in _currentPossibleScreenings)
            {
                if (films.Count != 0)
                {
                    bool Add = true;
                    foreach (var film in films)
                    {
                        if (film == currentPossibleScreening.Film)
                        {
                            Add = false;
                            break;
                        }
                    }

                    if (Add)
                    {
                        films.Add(currentPossibleScreening.Film);
                    }
                }
                else
                {
                    films.Add(currentPossibleScreening.Film);
                }
            }

            Films = films;
        }


        public void SetFilmTimes()
        {
            var screenings = _currentPossibleScreenings.Where(s => s.Film == _selectedFilm);

            var times = new List<DateTime>();

            foreach (var screening in screenings)
            {
                times.Add(screening.DateAndTime);
            }

            FilmTimes = times;

        }


        public InitialTicketBookingFormViewModel()
        {        
            SelectedDateTime = DateTime.Now;
        }
    }
}
