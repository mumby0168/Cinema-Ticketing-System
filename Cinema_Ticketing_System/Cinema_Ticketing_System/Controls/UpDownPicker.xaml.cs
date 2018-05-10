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
using Cinema_Ticketing_System.Annotations;

namespace Cinema_Ticketing_System.Controls
{
    /// <summary>
    /// Interaction logic for UpDownPicker.xaml
    /// </summary>
    public partial class UpDownPicker : UserControl
    {
        public UpDownPicker()
        {
            InitializeComponent();
            ValueTextBox.Text = Value.ToString();
        }

        public static DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(int), typeof(UpDownPicker), new PropertyMetadata(null));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set
            {
                SetValue(ValueProperty, value);
                ValueTextBox.Text = value.ToString();
            }
        }

        //private int _amount;

        //public string AmountStr
        //{
        //    get => _amount.ToString();
        //    set
        //    {
        //        _amount = int.Parse(value);
        //        Value = _amount;
        //        OnPropertyChanged(nameof(AmountStr));
        //    }
        //}

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            //_amount++;s
            //AmountStr = _amount.ToString();
            Value++;
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {

            //if (_amount != 0)
            //{
            //    _amount--;
            //    AmountStr = _amount.ToString();
            //}

            if (Value != 0)
            {
                Value--;
            }
        }

        //private static void OnValueChanged(
        //DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    UpDownPicker picker = sender as UpDownPicker;
        //    if (picker != null)
        //    {
        //        picker.Value = (int)e.NewValue;
        //    }

        //}


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
