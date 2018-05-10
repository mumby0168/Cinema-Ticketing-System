using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Ticketing_System.Database
{
    class DataHandler : IDisposable
    {
        private CinemaContext m_DatabaseContext;
        void IDisposable.Dispose()
        {
            m_DatabaseContext.SaveChangesAsync();
            m_DatabaseContext.Dispose();
        }

        public DataHandler()
        {
            m_DatabaseContext = new CinemaContext();
        }
    }
}
