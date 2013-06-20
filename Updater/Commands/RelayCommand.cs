using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Updater.Commands
{
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        /// <summary>
        /// Action to be executed.
        /// </summary>
        private readonly Action<T> mExecute = null;

        /// <summary>
        /// Predicate to evaluate whether the action can be executed.
        /// </summary>
        private readonly Predicate<T> mCanExecute = null;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            mExecute = execute;
            mCanExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        /// <summary>
        /// Indicates whether the action can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return mCanExecute == null ? true : mCanExecute((T)parameter);
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mExecute((T)parameter);
        }

        #endregion // ICommand Members
    }
}
