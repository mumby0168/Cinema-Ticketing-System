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
using Cinema_Ticketing_System.Models;

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

                if(Selected == true)
                {
                    if (NewSeat)
                    {
                        Colour = Brushes.Aqua;
                    }
                    else
                    {
                        Colour = Brushes.Green;
                    }
                    PersonVisibility = Visibility.Visible;
                }

                OnPropertyChanged();
            }
        }
        private Ticket _AssociatedTicket = null;
        public Ticket AssociatedTicket
        {
            get
            {
                return _AssociatedTicket;
            }
            set
            {
                _AssociatedTicket = value;
                if (AssociatedTicket != null)
                {
                    AssociatedTicket.ColumnNumber = ColumnNumber;
                    AssociatedTicket.RowNumber = RowNumber;
                    AssociatedTicket.SeatNumber = RowNumber.ToString() + ColumnNumber.ToString();
                }
                OnPropertyChanged();
            }
        }
        public delegate void SeatClickedCallback(Seat seatObj);
        private static SeatClickedCallback _OnClick;
        public static SeatClickedCallback OnClick
        {
            get
            {
                return _OnClick;
            }
            set
            {
                _OnClick = value;
            }
        }
        public Seat()
        {
            InitializeComponent();
        }

        private int _RowNumber = 0;
        public int RowNumber
        {
            get
            {
                return _RowNumber;
            }
            set
            {
                _RowNumber = value;
                OnPropertyChanged();
            }
        }

        private int _ColumnNumber = 0;
        public int ColumnNumber
        {
            get
            {
                return _ColumnNumber;
            }
            set
            {
                _ColumnNumber = value;
                OnPropertyChanged();
            }
        }

        private bool _NewSeat = false;
        public bool NewSeat
        {
            get
            {
                return _NewSeat;
            }
            set
            {
                _NewSeat = value;
                OnPropertyChanged("NewSeat");
            }
        }

        private static bool _AddingEnabled = false;
        public static bool AddingEnabled
        {
            get
            {
                return _AddingEnabled;
            }
            set
            {
                _AddingEnabled = value;
            }
        }

        private bool _CanEdit = false;
        public bool CanEdit
        {
            get
            {
                return _CanEdit;
            }
            set
            {
                _CanEdit = value;
                OnPropertyChanged();
            }
        }

        #pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private void IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true && !Selected && AddingEnabled)
            {
                Colour = Brushes.Orange;
            }
            else if ((bool)e.NewValue == false && !Selected)
            {
                Colour = Brushes.Red;
            }
        }
        #pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CanEdit)
            {
                if (Selected)
                {
                    Selected = false;
                    Colour = Brushes.Red;
                    PersonVisibility = Visibility.Hidden;
                }
                else if (!Selected && AddingEnabled)
                {
                    Selected = true;
                    if (NewSeat)
                    {
                        Colour = Brushes.Aqua;
                    }
                    else
                    {
                        Colour = Brushes.Green;
                    }
                    PersonVisibility = Visibility.Visible;
                }

                OnClick.Invoke(this);
            }
        }
    }
}
