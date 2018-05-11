using Cinema_Ticketing_System.Annotations;
using Cinema_Ticketing_System.Models;
using Cinema_Ticketing_System.ViewModels.Screen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema_Ticketing_System.Views
{
    public partial class ViewAScreenPage : UserControl
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewAScreenPage()
        {
            InitializeComponent();
        }

        private DateTime _Date = DateTime.Now;
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
                ScreenCombo.IsEnabled = true;
                using (Database.DataHandler handle = new Database.DataHandler())
                {
                    foreach (Screening S in handle.GetScreeningsWithScreenOnDate(_Date))
                    {
                        Screens.Add(S.Screen);
                    }
                }
                OnPropertyChanged();
            }
        }

        private Screen _SelectedScreen = null;
        private List<Screen> _Screens = null;
        public List<Screen> Screens
        {
            get
            {
                return _Screens;
            }
            set
            {
                _Screens = value;
                OnPropertyChanged();
            }
        }

        private Film _SelectedFilm = null;
        private List<Film> _Films = null;
        public List<Film> Films
        {
            get
            {
                return _Films;
            }
            set
            {
                _Films = value;
                OnPropertyChanged();
            }
        }

        private DateTime _SelectedTime = new DateTime();
        private List<DateTime> _Times = null;
        public List<DateTime> Times
        {
            get
            {
                return _Times;
            }
            set
            {
                _Times = value;
                OnPropertyChanged();
            }
        }

        private Screening _SelectedScreening = null;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Database.DataHandler handle = new Database.DataHandler())
            {
                _SelectedScreening = handle.GetScreeningFromDateFilmTimeScreen(_Date, _SelectedScreen, _SelectedFilm, _SelectedTime);
            }

            FormGrid.Visibility = Visibility.Collapsed;
            ScreenView.DataContext = new ScreenViewModel() { NumberOfColumns = _SelectedScreen.Columns, NumberOfRows = _SelectedScreen.Rows, ScreeningId = _SelectedScreening.Id, PendingTickets = null };
        }

        private void ScreenCombo_Selected(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            _SelectedScreen = (Screen)combo.SelectedItem;
            FilmCombo.IsEnabled = true;
            using (Database.DataHandler handle = new Database.DataHandler())
            {
                Films = handle.GetFilmsInScreen(_SelectedScreen);
            }
        }

        private void FilmCombo_Selected(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            _SelectedFilm = (Film)combo.SelectedItem;
            TimeCombo.IsEnabled = true;
            using (Database.DataHandler handle = new Database.DataHandler())
            {
                Times = handle.TimesFromScreensAndDate(_Date, _SelectedScreen);
            }
        }

        private void TimeCombo_Selected(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            _SelectedTime = (DateTime)combo.SelectedItem;
            SubmitButton.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ScreenView.Visibility = Visibility.Collapsed;
            FormGrid.Visibility = Visibility.Visible;
        }
    }
}
