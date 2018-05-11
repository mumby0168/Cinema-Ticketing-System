using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for Screen.xaml
    /// </summary>
    public partial class Screen : UserControl
    {
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

        public static readonly DependencyProperty ExsistingTicketsProperty = DependencyProperty.Register("ExsistingTickets", typeof(ObservableCollection<Ticket>), typeof(Screen), new PropertyMetadata(null));
        public ObservableCollection<Ticket> ExsistingTickets
        {
            set
            {
                SetValue(ExsistingTicketsProperty, value);
            }
            get
            {
                return (ObservableCollection<Ticket>)GetValue(ExsistingTicketsProperty);
            }
        }

        public static readonly DependencyProperty PendingTicketsProperty = DependencyProperty.Register("PendingTickets", typeof(ObservableCollection<Ticket>), typeof(Screen), new PropertyMetadata(null));
        public ObservableCollection<Ticket> PendingTickets
        {
            set
            {
                SetValue(ExsistingTicketsProperty, value);
            }
            get
            {
                return (ObservableCollection<Ticket>)GetValue(PendingTicketsProperty);
            }
        }

        private ObservableCollection<Ticket> _StagedTickets = null;
        public ObservableCollection<Ticket> StagedTickets
        {
            get
            {
                return _StagedTickets;
            }
            private set
            {
                _StagedTickets = value;
            }
        }

        public Screen()
        {
            InitializeComponent();
            Seat.OnClick += new Seat.SeatClickedCallback(OnSeatClicked);
        }
        private Ticket _NextTicketToAdd = null;
        public void OnSeatClicked(Seat seatObj)
        {
            if (_NextTicketToAdd != null && seatObj.AssociatedTicket == null)
            {
                seatObj.AssociatedTicket = _NextTicketToAdd;
                StagedTickets.Add(_NextTicketToAdd);
                PendingTickets.Remove(_NextTicketToAdd);
                _NextTicketToAdd = null;
            }
            else if (seatObj.AssociatedTicket != null)
            {
                PendingTickets.Add(_NextTicketToAdd);
                StagedTickets.Remove(_NextTicketToAdd);
                _NextTicketToAdd = seatObj.AssociatedTicket;
                seatObj.AssociatedTicket = null;
            }

            if (PendingTickets.Count > 0)
            {
                _NextTicketToAdd = PendingTickets[0];
            }
            else
            {
                Seat.AddingEnabled = false;
            }
        }

        private void CreateSeatingGrid()
        {
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
                    foreach (Ticket t in ExsistingTickets)
                    {
                        if(t.RowNumber == y && t.ColumnNumber == x)
                        {
                            Seat seat = new Seat();
                            seat.SetValue(Grid.ColumnProperty, x);
                            seat.SetValue(Grid.RowProperty, y);
                            seat.AssociatedTicket = t;
                            seat.CanEdit = false;
                            seat.Selected = true;
                            SeatingGrid.Children.Add(seat);

                            bNewSeat = false;
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
                        SeatingGrid.Children.Add(seat);
                    }
                }
            }

            if (PendingTickets.Count > 0)
            {
                Seat.AddingEnabled = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreateSeatingGrid();
        }
    }
}
