using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema_Ticketing_System.ViewModels.Commands
{
    public class ClickCommand : ICommand
    {
        private Action func;

        public ClickCommand(Action f)
        {
            func = f;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            func.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
