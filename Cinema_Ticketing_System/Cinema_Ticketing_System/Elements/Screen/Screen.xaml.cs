using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Cinema_Ticketing_System.Annotations;
using Cinema_Ticketing_System.Models;

namespace Cinema_Ticketing_System.Elements.Screen
{
    /// <summary>
    /// Interaction logic for Screen.xaml
    /// </summary>
    public partial class Screen : UserControl
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty CanInitializeProperty = DependencyProperty.Register("CanInitialize", typeof(bool), typeof(Screen), new PropertyMetadata(false));
        public bool CanInitialize
        {
            get
            {
                return (bool)GetValue(CanInitializeProperty);
            }
            set
            {
                SetValue(CanInitializeProperty, value);
            }
        }


        public static readonly DependencyProperty NumberOfColumnsProperty = DependencyProperty.Register("NumberOfColumns", typeof(int), typeof(Screen), new PropertyMetadata(0));
        public int NumberOfColumns
        {
            set
            {
                SetValue(NumberOfColumnsProperty, value);
            }
            get
            {
                return (int)GetValue(NumberOfColumnsProperty);
            }
        }

        public static readonly DependencyProperty NumberOfRowsProperty = DependencyProperty.Register("NumberOfRows", typeof(int), typeof(Screen), new PropertyMetadata(0));
        public int NumberOfRows
        {
            set
            {
                SetValue(NumberOfRowsProperty, value);
            }
            get
            {
                return (int)GetValue(NumberOfRowsProperty);
            }
        }

        public static readonly DependencyProperty ExsistingTicketsProperty = DependencyProperty.Register("ExsistingTickets", typeof(List<Ticket>), typeof(Screen), new PropertyMetadata(null));
        public List<Ticket> ExsistingTickets
        {
            set
            {
                SetValue(ExsistingTicketsProperty, value);
                OnPropertyChanged();
            }
            get
            {
                return (List<Ticket>)GetValue(ExsistingTicketsProperty);
            }
        }

        public static readonly DependencyProperty PendingTicketsProperty = DependencyProperty.Register("PendingTickets", typeof(ObservableCollection<Ticket>), typeof(Screen), new PropertyMetadata(null));
        public ObservableCollection<Ticket> PendingTickets
        {
            set
            {
                SetValue(ExsistingTicketsProperty, value);
                OnPropertyChanged();
            }
            get
            {
                return (ObservableCollection<Ticket>)GetValue(PendingTicketsProperty);
            }
        }

        private ObservableCollection<Ticket> _StagedTickets = new ObservableCollection<Ticket>();
        public ObservableCollection<Ticket> StagedTickets
        {
            get { return _StagedTickets; }
            private set { _StagedTickets = value; }
        }

        public Screen()
        {
            InitializeComponent();
            Seat.OnClick += new Seat.SeatClickedCallback(OnSeatClicked);
        }

        private Ticket _NextTicketToAdd = null;
        public void OnSeatClicked(Seat seatObj)
        {
            if (PendingTickets == null)
                return;

            if (seatObj.Selected && _NextTicketToAdd != null)
            {
                seatObj.AssociatedTicket = _NextTicketToAdd;
                PendingTickets.Remove(_NextTicketToAdd);
                StagedTickets.Add(_NextTicketToAdd);
                _NextTicketToAdd = null;
            }
            else if (seatObj.Selected == false && seatObj.AssociatedTicket != null)
            {
                PendingTickets.Add(seatObj.AssociatedTicket);
                StagedTickets.Remove(seatObj.AssociatedTicket);
                _NextTicketToAdd = seatObj.AssociatedTicket;
                seatObj.AssociatedTicket = null;
            }

            if (PendingTickets != null)
            {
                if (PendingTickets.Count > 0)
                {
                    Seat.AddingEnabled = true;
                    if (_NextTicketToAdd == null)
                    {
                        _NextTicketToAdd = PendingTickets[0];
                    }
                }
                else
                {
                    Seat.AddingEnabled = false;
                    _NextTicketToAdd = null;
                }
            }
        }

        private List<Seat> _Seats = new List<Seat>();

        public void CreateSeatingGrid()
        {
            StagedListView.ItemsSource = StagedTickets;
            if (PendingTickets != null)
            {
                if (PendingTickets.Count > 0)
                {
                    Seat.AddingEnabled = true;
                    PendingListView.Visibility = Visibility.Visible;
                    StagedListView.Visibility = Visibility.Visible;
                    SaveButton.Visibility = Visibility.Visible;

                    _NextTicketToAdd = PendingTickets[0];
                }
                else
                {
                    PendingListView.Visibility = Visibility.Collapsed;
                    StagedListView.Visibility = Visibility.Collapsed;
                    SaveButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                Seat.AddingEnabled = false;
                PendingListView.Visibility = Visibility.Collapsed;
                StagedListView.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Collapsed;
            }

            //Add the column and row defs for the col row labels
            SeatingGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            SeatingGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            //Add all the cols for the seats
            for(int i = 0; i < NumberOfColumns; i++)
            {
                SeatingGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            //Generate the seats
            for (int y = 1; y < NumberOfRows + 1; y++)
            {
                //Add the row label
                Label rowLabel = new Label();
                rowLabel.Content = y;
                rowLabel.SetValue(Grid.ColumnProperty, 0);
                rowLabel.SetValue(Grid.RowProperty, y);
                SeatingGrid.Children.Add(rowLabel);

                //Add the row def for this row of seats
                SeatingGrid.RowDefinitions.Add(new RowDefinition());

                for (int x = 1; x < NumberOfColumns + 1; x++)
                {
                    //Add the column label
                    Label colLabel = new Label();
                    colLabel.Content = x;
                    colLabel.SetValue(Grid.ColumnProperty, x);
                    colLabel.SetValue(Grid.RowProperty, 0);
                    SeatingGrid.Children.Add(colLabel);

                    bool bNewSeat = true;
                    if (ExsistingTickets != null)
                    {
                        foreach (Ticket t in ExsistingTickets)
                        {
                            if (t.RowNumber == y && t.ColumnNumber == x)
                            {
                                Seat seat = new Seat();
                                seat.SetValue(Grid.ColumnProperty, x);
                                seat.SetValue(Grid.RowProperty, y);
                                seat.RowNumber = y;
                                seat.ColumnNumber = x;
                                seat.CanEdit = false;
                                seat.Selected = true;
                                seat.NewSeat = bNewSeat;
                                _Seats.Add(seat);
                                SeatingGrid.Children.Add(seat);

                                bNewSeat = false;
                            }
                        }
                    }

                    if (bNewSeat)
                    {
                        //Create seat
                        Seat seat = new Seat();
                        seat.SetValue(Grid.ColumnProperty, x);
                        seat.SetValue(Grid.RowProperty, y);
                        seat.CanEdit = true;
                        seat.Selected = false;
                        seat.RowNumber = y;
                        seat.ColumnNumber = x;
                        seat.NewSeat = bNewSeat;
                        _Seats.Add(seat);
                        SeatingGrid.Children.Add(seat);
                    }
                }
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            CreateSeatingGrid();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(PendingTickets.Count == 0 && StagedTickets.Count > 0)
            {
                using (Database.DataHandler handle = new Database.DataHandler())
                {
                    foreach (Ticket t in StagedTickets)
                    {
                        foreach(Seat s in _Seats)
                        {
                            if(s.AssociatedTicket == t)
                            {
                                s.CanEdit = false;
                            }
                        }

                        t.Screening = null;
                        handle.AddTicket(t);
                    }
                    StagedTickets.Clear();
                    StagedListView.Visibility = Visibility.Collapsed;
                    PendingListView.Visibility = Visibility.Collapsed;
                    SaveButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
