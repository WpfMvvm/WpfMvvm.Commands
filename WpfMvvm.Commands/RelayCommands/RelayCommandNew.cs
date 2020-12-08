using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Реализация интерфейса <see cref="IRelayCommand"/> для методов без параметра.</summary>
    public class RelayCommand : IRelayCommand, ICommandRaise, ICommand
    {
        /// <inheritdoc cref="ICommand.CanExecuteChanged"/>
        public event EventHandler CanExecuteChanged;

        private readonly ExecuteCommandHandler executeObject;
        private readonly CanExecuteCommandHandler canExecuteObject;

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        /// <summary>Метод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <param name="parameter">Значение должно быть в типе приводимом к <see cref="ParameterType"/>.
        /// Если значение нельзя привести, то сразу возвращается <see langword="false"/>.<br/>
        /// Если <see cref="ParameterType"/> = <see langword="null"/>, значит в команду переданны методы без параметра.
        /// Тогда <paramref name="parameter"/> должен быть <see langword="null"/>. Иначе всегда возвращается <see langword="false"/>.</param>
        /// <returns> Значение <see langword="true"/>, если эту команду можно выполнить; в противном случае — значение <see langword="false"/>.</returns>
        /// <remarks>После проверки <paramref name="parameter"/> на <see langword="null"/> вызывается метод из переданного делегата.</remarks>
        public virtual bool CanExecute(object parameter)
            => canExecuteObject(parameter);

        // Метод для делегата canExecuteObject для переданных методов бех параметра.
        private bool CanExecuteNoParameter(object parameter)
            => parameter == null && canExecute();

        /// <summary>Метод, вызываемый при исполнении данной команды.</summary>
        /// <param name="parameter">Значение должно быть в типе приводимом к <see cref="ParameterType"/>.
        /// Если оно не приводится - возникает исключение <see cref="ParameterInvalidCastException"/><br/>
        /// Если <see cref="ParameterType"/> = <see langword="null"/> - значит в команду переданы методы без параметра.
        /// Тогда <paramref name="parameter"/> должен быть <see langword="null"/>.
        /// Иначе исключение <see cref="NotNullParameterException"/>.</param>
        /// <exception cref="NotNullParameterException">Если параметр не <see langword="null"/>.</exception>
        /// <exception cref="ParameterInvalidCastException">Если параметр не приводится к типу <see cref="ParameterType"/>.</exception>
        /// <exception cref="ParameterTypeNotAcceptNullException">Если в команду передан <see langword="null"/>,
        /// а тип <see cref="ParameterType"/> не может быть null.</exception>
        public virtual void Execute(object parameter) => executeObject(parameter);


        /// <summary>Метод, вызываемый при исполнении данной команды.</summary>
        /// <param name="parameter">Должен быть <see langword="null"/>. Иначе исключение <see cref="NotNullParameterException"/>.</param>
        /// <exception cref="NotNullParameterException">Если параметр не <see langword="null"/>.</exception>
        private void ExecuteNoParameter(object parameter)
        {
            if (parameter != null)
                throw NotNullParameterException;

            execute();
        }

        /// <inheritdoc cref="ICommandRaise.RaiseCanExecuteChanged"/>>
        public virtual void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>Всегда возвращает <see langword="true"/>.</summary>
        /// <returns>Всегда <see langword="true"/>.</returns>
        /// <remarks>Используется как заглушка, если не передан делегат для <see cref="CanExecute(object)"/>.</remarks>
        public static bool AlwaysTrue() => true;

        /// <summary>Всегда возвращает <see langword="true"/>.</summary>
        /// <param name="parameter">Параметерю Не используется.</param>
        /// <returns>Всегда <see langword="true"/>.</returns>
        /// <remarks>Используется как заглушка, если не передан делегат для <see cref="CanExecute(object)"/>.</remarks>
        public static bool AlwaysTrue(object parameter) => true;

        /// <inheritdoc cref="IRelayCommand.ParameterType"/>
        public Type ParameterType { get; }

        /// <summary>Создаёт экземпляр команды для методов object параметром.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <param name="canExecuteHandler">Делегат метода проверяющего состояние команды.</param>
        public RelayCommand(ExecuteCommandHandler executeHandler, CanExecuteCommandHandler canExecuteHandler)
            : this(typeof(object))
        {
            executeObject = executeHandler ?? throw ExecuteHandlerNullException;
            canExecuteObject = canExecuteHandler ?? throw CanExecuteHandlerNullException;
        }

        /// <summary>Создаёт экземпляр команды для методов без параметра.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <param name="canExecuteHandler">Делегат метода проверяющего состояние команды.</param>
        public RelayCommand(Action executeHandler, Func<bool> canExecuteHandler)
            : this((Type)null)
        {
            execute = executeHandler ?? throw ExecuteHandlerNullException;
            canExecute = canExecuteHandler ?? throw CanExecuteHandlerNullException;

            executeObject = ExecuteNoParameter;
            canExecuteObject = CanExecuteNoParameter;
        }

        /// <summary>Создаёт экземпляр команды для метода object параметром.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <remarks>Метод проверяющий состояние команды замещается методом <see cref="AlwaysTrue(object)"/>.</remarks>
        public RelayCommand(ExecuteCommandHandler executeHandler)
            : this(executeHandler, AlwaysTrue)
        { }

        /// <summary>Создаёт экземпляр команды для метода без параметра.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <remarks>Метод проверяющий состояние команды замещается методом <see cref="AlwaysTrue()"/>.</remarks>
        public RelayCommand(Action executeHandler)
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
        public static ArgumentException NotNullParameterException { get; } = new ArgumentException("Должен быть null.", "parameter");

        /// <summary>Возникает в <see cref="Execute(object)"/>, 
        /// если в передан тип не приводимый по шаблону к типу команды. </summary>
        public static InvalidCastException ParameterInvalidCastException { get; }
            = new InvalidCastException("Тип parameter не приводится по шаблону к типу команды.");

        /// <summary>Возникает в <see cref="Execute(object)"/>, 
        /// если в передан <see langword="null"/>, а тип <see cref="ParameterType"/> не может принять <see langword="null"/>.</summary>
        public static ArgumentException ParameterTypeNotAcceptNullException { get; }
            = new ArgumentException("ParameterType не может принять null.", "parameter");
    }
}
