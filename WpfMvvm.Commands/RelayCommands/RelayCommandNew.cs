using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Реализация интерфейса <see cref="IRelayCommand"/>.</summary>
    public class RelayCommand : IRelayCommand, ICommandRaise, ICommand
    {
        /// <inheritdoc cref="ICommand.CanExecuteChanged"/>
        public virtual event EventHandler CanExecuteChanged;

        private readonly ExecuteCommandHandler execute;
        private readonly CanExecuteCommandHandler canExecute;

        /// <inheritdoc cref="ICommand.CanExecute(object)"/>>
        public virtual bool CanExecute(object parameter)
            => parameter == null && canExecute();

        /// <inheritdoc cref="ICommand.Execute(object)"/>>
        public virtual void Execute(object parameter)
        {
            if (parameter != null)
                throw NotNullParameter;

            execute();
        }


        /// <inheritdoc cref="ICommandRaise.RaiseCanExecuteChanged"/>>
        public virtual void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>Всегда возвращает <see langword="true"/>.</summary>
        /// <returns>Всегда <see langword="true"/>.</returns>
        /// <remarks>Используется как заглушка, если не передан делегат для <see cref="CanExecute(object)"/>.</remarks>
        public static bool AlwaysTrue() => true;

        /// <summary>Создаёт экземпляр команды для методов без параметра.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <param name="canExecuteHandler">Делегат метода проверяющего состояние команды.</param>
        public RelayCommand(ExecuteCommandHandler executeHandler, CanExecuteCommandHandler canExecuteHandler)
            : this((Type)null)
        {
            execute= executeHandler ?? throw ExecuteHandlerNullException;
            canExecute= canExecuteHandler ?? throw CanExecuteHandlerNullException;
        }

        /// <summary>Создаёт экземпляр команды для метода без параметра.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <remarks>Метод проверяющий состояние команды замещается методом <see cref="AlwaysTrue()"/>.</remarks>
        public RelayCommand(ExecuteCommandHandler executeHandler)
            : this(executeHandler, AlwaysTrue)
        { }

        /// <summary>Создаёт экземпляр команды.</summary>
        protected RelayCommand(Type parameterType)
            => ParameterType = parameterType;

        /// <summary>Ошибка возникающая при передаче в конструтор <see cref="RelayCommand"/> параметра executeHandler=<see langword="null"/>.</summary>
        public static ArgumentNullException ExecuteHandlerNullException = new ArgumentNullException("executeHandler");

        /// <summary>Ошибка возникающая при передаче в конструтор <see cref="RelayCommand"/> параметра canExecuteHandler=<see langword="null"/>.</summary>
        public static ArgumentNullException CanExecuteHandlerNullException = new ArgumentNullException("canExecuteHandler");

        /// <summary>Возникает в экземпляре команды для методов без параметра,
        /// если в метод <see cref="Execute(object)"/> был передан не <see langword="null"/>.</summary>
        public static ArgumentException NotNullParameter { get; } = new ArgumentException("Должен быть null.", "parameter");

        /// <inheritdoc cref="IRelayCommand.ParameterType"/>
        public Type ParameterType { get; }
    }
}
