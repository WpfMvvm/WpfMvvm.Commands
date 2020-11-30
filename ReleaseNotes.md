# V.0.0.0.1 [Ещё не опубликован]
Версия первой публикации дяя темы [Библиотека элементов для реализации WPF MVVM Решений](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html).

В составе пакета:
1. ExecuteCommandHandler - Делегат метода ICommand.Execute(object)
2. CanExecuteCommandHandler - Делегат метода ICommand.CanExecute(object)
3. public delegate void ExecuteCommandHandler<T> - Делегат метода ICommand<T>.Execute(T).
4. CanExecuteCommandHandler<T> - Делегат метода ICommand<T>.CanExecute(T).
5. ExecuteCommandActionHandler - Делегат метода ICommandAction.Execute().
6. CanExecuteCommandActionHandler - Делегат метода ICommandAction.CanExecute().
7. ICommandRaise - Интерфейс добавляющий в интерфейс ICommand метод RaiseCanExecuteChanged(), поднимающий событие ICommand.CanExecuteChanged.
8. interface ICommand<T> - добавляюший методы с обобщённым параметром.
9. interface ICommandAction - добавляюший методы без параметра.
10. RelayCommand - Базовая реализация команды с интерфесом ICommandRaise.
11. RelayCommand<T> - Производная от RelayCommand реализация команды с интерфесом ICommand<T>.
12. RelayCommandAction - Производная от RelayCommand реализация команды с интерфесом ICommandAction.

