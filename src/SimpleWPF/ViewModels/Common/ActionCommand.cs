using System;
using System.Windows.Input;

namespace SimpleWPF.ViewModels.Common;

public class ActionCommand : ICommand
{
    private readonly Action<object> _action;
    private readonly Predicate<object>? _predicate;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActionCommand"/> class.
    /// </summary>
    /// <param name="action">The action to invoke on command.</param>
    /// <param name="predicate">The predicate that determines if the action can be invoked.</param>
    public ActionCommand(Action<object> action, Predicate<object>? predicate = null)
    {
        this._action = action ?? throw new ArgumentNullException(nameof(action), @"You must specify an Action<T>.");
        this._predicate = predicate;
    }

    /// <summary>
    /// Occurs when the <see cref="System.Windows.Input.CommandManager"/> detects conditions that might change the ability of a command to execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>
    /// Determines whether the command can execute.
    /// </summary>
    /// <param name="parameter">A custom parameter object.</param>
    /// <returns>
    ///     Returns true if the command can execute, otherwise returns false.
    /// </returns>
    public bool CanExecute(object? parameter)
    {
        return parameter != null && (this._predicate == null || this._predicate(parameter));
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="parameter">A custom parameter object.</param>
    public void Execute(object? parameter = null)
    {
        if (parameter != null) this._action(parameter);
    }
}