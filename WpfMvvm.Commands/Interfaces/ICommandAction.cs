using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Интерфейс добавляющий в интерфейс <see cref="ICommand"/>
    /// методы <see cref="CanExecute()"/> и <see cref="Execute()"/>.</summary>
    /// <remarks>При реализации интерфейса методы <see cref="ICommand.CanExecute(object)"/> и <see cref="ICommand.Execute(object)"/>
    /// должны обращаться к методам <see cref="CanExecute()"/> и <see cref="Execute()"/>.</remarks>
    interface ICommandAction : ICommand
    {
        /// <summary>Mетод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <returns>Значение true, если эту команду можно выполнить; в противном случае — значение false.</returns>
        /// <remarks>При реализации интерфейса метод <see cref="ICommand.CanExecute(object)"/>
        /// должен обращаться к этому методу.</remarks>
        bool CanExecute();

        /// <summary>Определяет метод, вызываемый при исполнении данной команды.</summary>
        /// <remarks>При реализации интерфейса метод <see cref="ICommand.Execute(object)"/>
        /// должен обращаться к этому методу.</remarks>
        void Execute();
    }

}
