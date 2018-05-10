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
        private static readonly DependencyProperty ExsistingTicketsProperty = DependencyProperty.Register("ExsistingTickets", typeof(ObservableCollection<Ticket>), typeof(Screen), new PropertyMetadata(null));

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

        public Screen()
        {
            InitializeComponent();
        }
    }
}
