using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Интерфейс добавляющий в интерфейс <see cref="ICommand"/>
    /// методы <see cref="CanExecute(T)"/> и <see cref="Execute(T)"/> с парамерами типа <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">Тип парамера методов <see cref="CanExecute(T)"/> и <see cref="Execute(T)"/>.</typeparam>
    /// <remarks>При реализации интерфейса методы <see cref="ICommand.CanExecute(object)"/> и <see cref="ICommand.Execute(object)"/>
    /// должны обращаться к методам <see cref="CanExecute(T)"/> и <see cref="Execute(T)"/>.</remarks>
    interface ICommand<T> : ICommand
    {
        /// <summary>Mетод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <param name="parameter">Данные, используемые данной командой. 
        /// Если команда не требует передачи данных, этому параметру может быть присвоено значение default.</param>
        /// <returns>Значение true, если эту команду можно выполнить; в противном случае — значение false.</returns>
        /// <remarks>При реализации интерфейса метод <see cref="ICommand.CanExecute(object)"/>
        /// должен обращаться к этому методу.</remarks>
        bool CanExecute(T parameter);

        /// <summary>Определяет метод, вызываемый при исполнении данной команды.</summary>
        /// <param name="parameter">Данные, используемые данной командой. 
        /// Если команда не требует передачи данных, этому параметру может быть присвоено значение default.</param>
        /// <remarks>При реализации интерфейса метод <see cref="ICommand.Execute(object)"/>
        /// должен обращаться к этому методу.</remarks>
        void Execute(T parameter);
    }

}
