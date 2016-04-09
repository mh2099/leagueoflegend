namespace lolUI.Class
{
    using System;
    using System.Windows.Input;

    /// <summary>
    ///     No WPF project is complete without it's own version of this.
    /// </summary>
    public class AnotherCommandImplementation : ICommand
    {
        private readonly Func<Object, Boolean> _canExecute;
        private readonly Action<Object> _execute;

        public AnotherCommandImplementation(Action<Object> execute) : this(execute, null) {}

        public AnotherCommandImplementation(Action<Object> execute, Func<Object, Boolean> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(Object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}