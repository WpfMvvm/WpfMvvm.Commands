using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Реализация интерфейса <see cref="ICommandRaise"/>.</summary>
    public class RelayCommand : ICommandRaise
    {
        /// <inheritdoc cref="ICommand.CanExecuteChanged"/>
        public event EventHandler CanExecuteChanged;

        private readonly ExecuteCommandHandler execute;

        private readonly CanExecuteCommandHandler canExecute;

        /// <inheritdoc cref="ICommand.CanExecute(object)"/>>
        public virtual bool CanExecute(object parameter)
            => canExecute(parameter);

        /// <inheritdoc cref="ICommand.Execute(object)"/>>
        public virtual void Execute(object parameter)
            => execute(parameter);

        /// <inheritdoc cref="ICommandRaise.RaiseCanExecuteChanged"/>>
        public virtual void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>Всегда возвращает <see langword="true"/>.</summary>
        /// <param name="parameter">Параметр команды. Не используется.</param>
        /// <returns>Всегда <see langword="true"/>.</returns>
        /// <remarks>Используется как заглушка, если не передан делегат для <see cref="CanExecute(object)"/>.</remarks>
        public static bool AllTrue(object parameter) => true;

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <param name="canExecuteHandler">Делегат метода возвращающего состояние команды.</param>
        public RelayCommand(ExecuteCommandHandler executeHandler, CanExecuteCommandHandler canExecuteHandler)
        {
            execute = executeHandler ?? throw ExecuteHandlerNullException;
            canExecute = canExecuteHandler ?? throw CanExecuteHandlerNullException;
        }

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        public RelayCommand(ExecuteCommandHandler executeHandler)
              : this(executeHandler, AllTrue)
        { }

        /// <summary>Создаёт экземпляр команды.</summary>
        protected RelayCommand() { }

        /// <summary>Ошибка возникающая при передаче в конструтор <see cref="RelayCommand"/> параметра executeHandler=<see langword="null"/>.</summary>
        public static ArgumentNullException ExecuteHandlerNullException = new ArgumentNullException("executeHandler");

        /// <summary>Ошибка возникающая при передаче в конструтор <see cref="RelayCommand"/> параметра canExecuteHandler=<see langword="null"/>.</summary>
        public static ArgumentNullException CanExecuteHandlerNullException = new ArgumentNullException("canExecuteHandler");
    }
}
