using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Ticketing_System.ViewModels.Screen
{
    class ScreenViewModel : IDisposable
    {
        private DatabaseContext m_Context = new DatabaseContext();
        void IDisposable.Dispose()
        {
            DatabaseContext.Dispose();
        }

        public ScreenViewModel()
        {

        }
    }
}
