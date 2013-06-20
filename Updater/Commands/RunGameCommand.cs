using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Updater.Commands
{
    /// <summary>
    /// A command which executes the game executable.
    /// </summary>
    public class RunGameCommand : ICommand
    {

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Returns <code>true</code> if the Play command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Boolean CanExecute(object parameter)
        {
            if (parameter is Boolean)
            {

            }

            return false;
        }

        /// <summary>
        /// Executes the Play command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
