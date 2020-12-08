using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Методы расширения</summary>
    public static class ExtensionMethods
    {
        /// <summary>Выполняет команду, если возможно.</summary>
        /// <param name="command">Команда.</param>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns><see langword="true"/> если комада выполнена; иначе - <see langword="false"/>.</returns>
        /// <remarks>Сначала проверяется результат метода <see cref="ICommand.CanExecute(object)"/> и,
        /// если он <see langword="true"/>, вызываеся метод <see cref="ICommand.CanExecuteChanged"/>.</remarks>
        public static bool ExecuteIfCan(this ICommand command, object parameter)
        {
            if (!command.CanExecute(parameter))
                return false;

            command.Execute(parameter);
            return true;
        }

        /// <summary>Выполняет команду, если возможно.</summary>
        /// <param name="command">Команда.</param>
        /// <returns><see langword="true"/> если комада выполнена; иначе - <see langword="false"/>.</returns>
        /// <remarks>Сначала проверяется результат метода <see cref="ICommand.CanExecute(object)"/> и,
        /// если он <see langword="true"/>, вызываеся метод <see cref="ICommand.CanExecuteChanged"/>.<br/>
        /// В параметр методов передаётся <see langword="null"/>.</remarks>
        public static bool ExecuteIfCan(this ICommand command)
           => ExecuteIfCan(command, null);
   }
}
