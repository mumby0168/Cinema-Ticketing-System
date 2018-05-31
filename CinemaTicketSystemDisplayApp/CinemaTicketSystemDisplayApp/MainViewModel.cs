using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CinemaTicketSystemDisplayApp.Annotations;

namespace CinemaTicketSystemDisplayApp
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private DateTime _dateChosen;

        public DateTime DateChosen
        {
            get { return _dateChosen; }
            set
            {
                _dateChosen = value;
                DateChosenSelected();
                OnPropertyChanged();
                
            }
        }

        public MainViewModel()
        {
            DateChosen = DateTime.Now;
        }

        private List<Screening> _screenings;

        public List<Screening> Screenings
        {
            get { return _screenings; }
            set
            {
                _screenings = value;
                OnPropertyChanged();
            }
        }


        private void DateChosenSelected()
        {
            Entities entities = new Entities();


            Screenings = entities.Screenings.ToList();

            Screenings = Screenings.Where(s => s.DateAndTime.Date == _dateChosen.Date).ToList();

        }













        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
          {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
