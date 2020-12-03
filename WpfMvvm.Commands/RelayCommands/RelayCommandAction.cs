using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Производный от <see cref="RelayCommand"/> класс,
    /// реализующий интерфейс <see cref="ICommandAction"/>.</summary>
    public class RelayCommandAction : RelayCommand, IRelayCommandAction, IRelayCommand, ICommandAction, ICommandRaise, ICommand
    {

        /// <summary>Метод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <param name="parameter">Команда не требует передачи данных.
        /// Должно быть присвоено значение null, иначе всегда будет возвращаться <see langword="false"/>.</param>
        /// <returns> Значение true, если эту команду можно выполнить; в противном случае — значение false.</returns>
        /// <remarks>После проверки <paramref name="parameter"/> на <see langword="null"/> вызывается метод <see cref="CanExecute()"/>.</remarks>
        public override bool CanExecute(object parameter)
            => parameter == null && CanExecute();

        /// <summary>Метод, вызываемый при исполнении данной команды</summary>
        /// <param name="parameter">Команда не требует передачи данных.
        /// Должно быть присвоено значение null, иначе исключение <see cref="NotNullParameter"/>.</param>
        /// <exception cref="NotNullParameter">Если <paramref name="parameter"/> != <see langword="null"/>.</exception>
        /// <remarks>После проверки <paramref name="parameter"/> на <see langword="null"/> вызывается метод <see cref="Execute()"/>.</remarks>
        public override void Execute(object parameter)
        {
            if (parameter != null)
                throw NotNullParameter;

            Execute();
        }

        /// <summary>Делегат метода исполняющего команду.</summary>
        private readonly ExecuteCommandActionHandler execute;

        /// <summary>Делегат метода возвращающего состояние команды.</summary>
        private readonly CanExecuteCommandActionHandler canExecute;

        /// <inheritdoc cref="ICommandAction.CanExecute()"/>
        public bool CanExecute() => canExecute();

        /// <inheritdoc cref="ICommandAction.Execute()"/>
        public void Execute() => execute();

        /// <summary>Всегда возвращает <see langword="true"/>.</summary>
        /// <returns>Всегда <see langword="true"/>.</returns>
        /// <remarks>Используется как заглушка, если не передан делегат для <see cref="CanExecute()"/>.</remarks>
        public static bool AllTrue() => true;

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        /// <param name="canExecuteHandler">Делегат метода возвращающего состояние команды.</param>
        public RelayCommandAction(ExecuteCommandActionHandler executeHandler, CanExecuteCommandActionHandler canExecuteHandler)
        {
            execute = executeHandler ?? throw ExecuteHandlerNullException;
            canExecute = canExecuteHandler ?? throw CanExecuteHandlerNullException;
        }

        /// <summary>Создаёт экземпляр команды.</summary>
        /// <param name="executeHandler">Делегат метода исполняющего команду.</param>
        public RelayCommandAction(ExecuteCommandActionHandler executeHandler)
            : this(executeHandler, AllTrue)
        { }

        /// <inheritdoc cref="IRelayCommand.ParameterType"/>
        public override Type ParameterType => null;

        /// <summary>Возникает если в метод <see cref="Execute(object)"/> был передан не <see langword="null"/>.</summary>
        public static ArgumentException NotNullParameter { get; } = new ArgumentException("Должен быть null.", "parameter");
    }
}
