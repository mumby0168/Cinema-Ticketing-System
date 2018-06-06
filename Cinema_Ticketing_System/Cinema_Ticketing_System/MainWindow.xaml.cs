using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Cinema_Ticketing_System.ViewModels.Screen;

namespace Cinema_Ticketing_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate void Test();

        private void Deb()
        {
            System.Diagnostics.Debug.WriteLine("Data Gen Finished");
        }

        private void Method(Test t)
        {
            using (var handle = new Database.DataHandler())
            {
                handle.GenerateData(14);
            }

            t();
        }

        public MainWindow()
        {
            Test a = new Test(Deb);


            Thread t = new Thread(new ThreadStart(() => 
            {
                Method(a);
            }));

            t.Name = "Data Generator";
            t.Start();

            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("Loaded");
        }
    }
}