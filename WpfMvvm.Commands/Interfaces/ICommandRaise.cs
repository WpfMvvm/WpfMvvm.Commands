using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Интерфейс добавляющий в интерфейс <see cref="ICommand"/>
    /// метод <see cref="RaiseCanExecuteChanged"/>, поднимающий событие <see cref="ICommand.CanExecuteChanged"/>.</summary>
    public interface ICommandRaise : ICommand
    {
        /// <summary>Подымает (создаёт) событие <see cref="ICommand.CanExecuteChanged"/>.</summary>
        void RaiseCanExecuteChanged();
    }
}
