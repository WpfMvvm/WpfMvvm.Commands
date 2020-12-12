using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Интерфейс для команд с методом <see cref="ICommandRaise.RaiseCanExecuteChanged"/>
    /// и заданным типом параметра.</summary>
    /// <remarks>Если команда получает параметр в ином типе,
    /// то он сначала должен быть конвертирован в тип <see cref="ParameterType"/>
    /// и только потом передаваться в методы его обрабатывающие.<br/>
    /// Если <see cref="ParameterType"/>=<see langword="null"/>, то значит методы не принимают параметра.<br/>
    /// Если параметр нельзя конвертировать в заданный тип, то реализация интерфейса может выкинуть исключение,
    /// но не обязательно. Это поведение определяется в самой реализации.</remarks>
    public interface IRelayCommand : ICommandRaise, ICommand
    {
        /// <summary>Тип параметра.</summary>
        Type ParameterType { get; }
    }
}
