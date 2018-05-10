using Cinema_Ticketing_System.Annotations;
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

namespace Cinema_Ticketing_System.Elements.Screen
{
    /// <summary>
    /// Interaction logic for Seat.xaml
    /// </summary>
    public partial class Seat : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty ColourProperty = DependencyProperty.Register("Colour", typeof(Brush), typeof(Seat), new PropertyMetadata(Brushes.Red));
        public Brush Colour
        {
            get
            {
                return (Brush)GetValue(ColourProperty);
            }
            set
            {
                SetValue(ColourProperty, value);
                OnPropertyChanged();
            }
        }
        public static readonly DependencyProperty PersonVisibilityProperty = DependencyProperty.Register("PersonVisibility", typeof(Visibility), typeof(Seat), new PropertyMetadata(Visibility.Hidden));
        public Visibility PersonVisibility
        {
            get
            {
                return (Visibility)GetValue(PersonVisibilityProperty);
            }
            set
            {
                SetValue(PersonVisibilityProperty, value);
                OnPropertyChanged();
            }
        }
        private bool _Selected = false;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
                OnPropertyChanged();
            }
        }

        public Seat()
        {
            InitializeComponent();

            if(Selected)
            {
                Colour = Brushes.Green;
                PersonVisibility = Visibility.Visible;
            }
            else if (IsMouseOver)
            {
                Colour = Brushes.Orange;
                PersonVisibility = Visibility.Hidden;
            }
            else
            {
                Colour = Brushes.Red;
                PersonVisibility = Visibility.Hidden;
            }
        }

        #pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private void IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if((bool)e.NewValue == true && !Selected)
            {
                Colour = Brushes.Orange;
            }
            else if (!Selected)
            {
                Colour = Brushes.Red;
            }
        }
        #pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            Selected = !Selected;
            if(Selected)
            {
                Colour = Brushes.Green;
                PersonVisibility = Visibility.Visible;
            }
            else if (IsMouseOver)
            {
                Colour = Brushes.Orange;
                PersonVisibility = Visibility.Hidden;
            }
            else
            {
                Colour = Brushes.Red;
                PersonVisibility = Visibility.Hidden;
            }
        }
    }
}
