using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Интерфейс объединяющий <see cref="IRelayCommand"/> и <see cref="ICommandAction"/>.</summary>
    interface IRelayCommandAction : IRelayCommand, ICommandAction, ICommandRaise, ICommand
    { }

}
