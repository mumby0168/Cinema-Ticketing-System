using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema_Ticketing_System.ViewModels.Commands;

namespace Cinema_Ticketing_System.ViewModels
{
    public class HomePageViewModel
    {
        public ClickCommand OpenPdfClick { get; private set; }

        public HomePageViewModel()
        {
            OpenPdfClick = new ClickCommand(OpenPdf);
        }

        public void OpenPdf()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "../../../../UG.pdf";
            Process.Start(dir);
        }
    }
}