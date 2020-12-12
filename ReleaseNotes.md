# V.0.0.0.1 [Ещё не опубликован]
Версия первой публикации для темы [Библиотека элементов для реализации WPF MVVM Решений](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html).

В составе пакета:
1. [ExecuteCommandHandler](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Делегат исполнительного метода команды с object параметром.
2. [CanExecuteCommandHandler](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Делегат метода проверяющего состояние команды с object параметром.
3. [ExecuteCommandHandler<T>](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Делегат исполнительного метода команды с обобщённым параметром.
4. [CanExecuteCommandHandler<T>](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Делегат метода проверяющего состояние команды с обобщённым параметром.
5. [ICommandRaise](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Интерфейс добавляющий в интерфейс ICommand метод RaiseCanExecuteChanged().
7. [IRelayCommand](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Интерфейс добавляющий в интерфейс ICommandRaise свойство с типом параметра.
8. [RelayCommand](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Базовая реализация команды с интерфейсом IRelayCommand для методов без параметра.
9. [RelayCommand<T>](https://www.cyberforum.ru/wpf-silverlight/thread2738784.html#post15091219) - Производная от RelayCommand реализация команды для методов с обобщённым параметром.

