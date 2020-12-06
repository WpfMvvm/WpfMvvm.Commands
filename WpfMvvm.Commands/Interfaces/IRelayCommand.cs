using System;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Интерфейс для команд с меодм <see cref="ICommandRaise.RaiseCanExecuteChanged"/>
    /// и заданным типом параметра.</summary>
    /// <remarks>Если комада получает параметр в ином типе,
    /// то он сначала должен быть конвертирован в тип <see cref="ParameterType"/>
    /// и только потом передаваться в методы его обрабатывающие.<br/>
    /// Еслл <see cref="ParameterType"/>=<see langword="null"/>, то значит методы не принимают параметра.<br/>
    /// Если параметр нельзя конвертировать в заданный тип, то реализация интерфейса может выкинуть исключение,
    /// но не обязательно. Это поведение определяется в самой реализации.</remarks>
    public interface IRelayCommand : ICommandRaise, ICommand
    {
        /// <summary>Тип парметра.</summary>
        Type ParameterType { get; }
    }
}
