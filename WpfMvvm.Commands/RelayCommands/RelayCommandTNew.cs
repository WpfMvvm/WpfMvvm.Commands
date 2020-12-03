using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Производный от <see cref="RelayCommand"/> класс
    /// с обобщённым параметром.</summary>
    /// <typeparam name="T">Тип параметра методов команды.</typeparam>
    public class RelayCommand<T> : RelayCommand, IRelayCommand, ICommandRaise, ICommand
    {

        /// <summary>Метод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <param name="parameter">Команда требует данных в типе приводимом по шаблону к типу <typeparamref name="T"/>.<br/>
        /// Если оно не приводится к <typeparamref name="T"/>, всегда будет возвращаться <see langword="false"/>.<br/>
        /// Если передан <see langword="null"/>, то проверяется тип <see cref="RelayCommand.ParameterType"/> на принадлежность к значимым типам.<br/>
        /// Если принадлежит - возвращается <see langword="false"/>; иначе - результат выполнения метода переданного по делегату с default параметром.</param>
        /// <returns> Значение <see langword="true"/>, если эту команду можно выполнить; в противном случае — значение <see langword="false"/>.</returns>
        /// <remarks>После приведения <paramref name="parameter"/> к <typeparamref name="T"/> вызывается метод из переданного делегата.</remarks>
        public override bool CanExecute(object parameter)
            => parameter == null
                ? isNullableParameterType && canExecute(default)
                : parameter is T t && canExecute(t);

        private readonly bool isNullableParameterType;

        /// <summary>Метод, вызываемый при исполнении данной команды</summary>
        /// <param name="parameter">Команда требует данных в типе явно приводимом к типу <typeparamref name="T"/>.
        /// Если оно не приводится к <typeparamref name="T"/> возникнет исключение кастования.</param>
        /// <returns> Значение true, если эту команду можно выполнить; в противном случае — значение false.</returns>
        /// <remarks>После приведения <paramref name="parameter"/> к <typeparamref name="T"/> <c>(T)parameter</c>
        /// вызывается исполнительный метод по полученному делегату.</remarks>
        /// <exception cref="InvalidCastException">Если недопустимо явное приведение параметра к типу <typeparamref name="T"/>.</exception>
        public override void Execute(object parameter)
            => execute((T)parameter);

        /// <summary>Делегат метода исполняющего команду.</summary>
        private readonly ExecuteCommandHandler<T> execute;

        /// <summary>Делегат метода проверяющего состояние команды.</summary>
        private readonly CanExecuteCommandHandler<T> canExecute;

        /// <summary>Всегда возвращает <see langword="true"/>.</summary>
        /// <param name="parameter">Параметр команды. Не используется.</param>
        /// <returns>Всегда <see langword="true"/>.</returns>
        /// <remarks>Используется как заглушка, если не передан делегат для метода проверяющего состояние команды.</remarks>
        public static bool AlwaysTrue(T parameter) => true;

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <param name="canExecuteHandler">Делегат метода проверяющео состояние команды.</param>
        public RelayCommand(ExecuteCommandHandler<T> executeHandler, CanExecuteCommandHandler<T> canExecuteHandler)
            : base(typeof(T))
        {
            execute = executeHandler ?? throw ExecuteHandlerNullException;
            canExecute = canExecuteHandler ?? throw CanExecuteHandlerNullException;

            isNullableParameterType = default(T) == null;
        }

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <remarks>Метод проверяющий состояние команды замещается методом <see cref="AlwaysTrue(T)"/>.</remarks>
        public RelayCommand(ExecuteCommandHandler<T> executeHandler)
            : this(executeHandler, AlwaysTrue)
        { }

    }
}
