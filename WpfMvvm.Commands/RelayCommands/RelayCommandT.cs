namespace WpfMvvm.Commands
{
    /// <summary>Производный от <see cref="RelayCommand"/> класс,
    /// реализующий интерфейс <see cref="ICommand{T}"/>.</summary>
    /// <typeparam name="T">Тип параметра методов команды.</typeparam>
    public class RelayCommand<T> : RelayCommand, ICommand<T>
    {

        /// <inheritdoc cref="RelayCommand.CanExecute(object)"/>
        public override bool CanExecute(object parameter)
            => parameter is T t && CanExecute(t);

        /// <inheritdoc cref="RelayCommand.Execute(object)"/>
        public override void Execute(object parameter)
        {
            if (parameter is T t)
                Execute(t);
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
    }
}
