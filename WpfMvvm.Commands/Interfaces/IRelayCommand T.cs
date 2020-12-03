using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Интерфейс объединяющий <see cref="IRelayCommand"/> и <see cref="ICommand{T}"/>.</summary>
    /// <typeparam name="T">Тип параметра команды.</typeparam>
    interface IRelayCommand<T> : IRelayCommand, ICommand<T>, ICommandRaise, ICommand
    { }
}
