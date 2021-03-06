﻿using System.Windows.Input;

namespace WpfMvvm.Commands
{
    /// <summary>Делегат метода <see cref="ICommand.Execute(object)"/>.</summary>
    /// <param name="parameter">Параметр команды.</param>
    public delegate void ExecuteCommandHandler(object parameter);

    /// <summary>Делегат метода <see cref="ICommand.CanExecute(object)"/>.</summary>
    /// <param name="parameter">Параметр команды.</param>
    /// <returns>Значение <see langword="true"/>, если эту команду можно выполнить;<br/>
    /// в противном случае — значение <see langword="false"/>.</returns>
    public delegate bool CanExecuteCommandHandler(object parameter);

    /// <summary>Делегат исполнительного метода команды с обобщённым параметром.</summary>
    /// <typeparam name="T">Тип параметра команды</typeparam>
    /// <param name="parameter">Параметр команды.</param>
    public delegate void ExecuteCommandHandler<T>(T parameter);

    /// <summary>Делегат метода состояния команды с обобщённым параметром.</summary>
    /// <typeparam name="T">Тип параметра команды</typeparam>
    /// <param name="parameter">Параметр команды.</param>
    /// <returns>Значение <see langword="true"/>, если эту команду можно выполнить;<br/>
    /// в противном случае — значение <see langword="false"/>.</returns>
    public delegate bool CanExecuteCommandHandler<T>(T parameter);
}
