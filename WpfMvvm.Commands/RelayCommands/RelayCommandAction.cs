namespace WpfMvvm.Commands
{
    /// <summary>Производный от <see cref="RelayCommand"/> класс,
    /// реализующий интерфейс <see cref="ICommandAction"/>.</summary>
    public class RelayCommandAction : RelayCommand, ICommandAction
    {

        /// <inheritdoc cref="RelayCommand.CanExecute(object)"/>
        public override bool CanExecute(object parameter)
            => parameter == null && CanExecute();

        /// <inheritdoc cref="RelayCommand.Execute(object)"/>
        public override void Execute(object parameter)
        {
            if (parameter == null)
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
    }
}
