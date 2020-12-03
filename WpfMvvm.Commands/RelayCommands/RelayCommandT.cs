using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Производный от <see cref="RelayCommand"/> класс,
    /// реализующий интерфейс <see cref="ICommand{T}"/>.</summary>
    /// <typeparam name="T">Тип параметра методов команды.</typeparam>
    public class RelayCommand<T> : RelayCommand, IRelayCommand<T>, IRelayCommand, ICommand<T>, ICommandRaise, ICommand
    {

        /// <summary>Метод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <param name="parameter">Команда требует данных в типе приводимом по шаблону к типу <typeparamref name="T"/>.
        /// Если значение null или оно не приводится к <typeparamref name="T"/>, всегда будет возвращаться <see langword="false"/>.</param>
        /// <returns> Значение true, если эту команду можно выполнить; в противном случае — значение false.</returns>
        /// <remarks>После приведения <paramref name="parameter"/> к <typeparamref name="T"/> вызывается метод <see cref="CanExecute(T)"/>.</remarks>
        public override bool CanExecute(object parameter)
            => parameter is T t && CanExecute(t);

        /// <summary>Метод, вызываемый при исполнении данной команды</summary>
        /// <param name="parameter">Команда требует данных в типе явно приводимом к типу <typeparamref name="T"/>.
        /// Если оно не приводится к <typeparamref name="T"/> возникнет исключение кастования.</param>
        /// <returns> Значение true, если эту команду можно выполнить; в противном случае — значение false.</returns>
        /// <remarks>После приведения <paramref name="parameter"/> к <typeparamref name="T"/> <c>(T)parameter</c>
        /// вызывается метод <see cref="CanExecute(T)"/>.</remarks>
        public override void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        /// <summary>Делегат метода исполняющего команду.</summary>
        private readonly ExecuteCommandHandler<T> execute;

        /// <summary>Делегат метода возвращающего состояние команды.</summary>
        private readonly CanExecuteCommandHandler<T> canExecute;

        /// <inheritdoc cref="ICommand{T}.CanExecute(T)"/>
        public bool CanExecute(T parameter) => canExecute(parameter);

        /// <inheritdoc cref="ICommand{T}.Execute(T)"/>
        public void Execute(T parameter) => execute(parameter);

        /// <summary>Всегда возвращает <see langword="true"/>.</summary>
        /// <param name="parameter">Параметр команды. Не используется.</param>
        /// <returns>Всегда <see langword="true"/>.</returns>
        /// <remarks>Используется как заглушка, если не передан делегат для <see cref="CanExecute(T)"/>.</remarks>
        public static bool AllTrue(T parameter) => true;

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <param name="canExecuteHandler">Делегат метода возвращающего состояние команды.</param>
        public RelayCommand(ExecuteCommandHandler<T> executeHandler, CanExecuteCommandHandler<T> canExecuteHandler)
        {
            execute = executeHandler ?? throw ExecuteHandlerNullException;
            canExecute = canExecuteHandler ?? throw CanExecuteHandlerNullException;
        }

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        public RelayCommand(ExecuteCommandHandler<T> executeHandler)
            : this(executeHandler, AllTrue)
        { }

        /// <inheritdoc cref="IRelayCommand.ParameterType"/>
        public override Type ParameterType => parameterType;
        private static readonly Type parameterType = typeof(T);
    }
}
