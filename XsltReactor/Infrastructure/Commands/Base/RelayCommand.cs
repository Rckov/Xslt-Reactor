using System.Windows.Input;

namespace XsltReactor.Infrastructure.Commands.Base;

/// <summary>
/// Base class for implementing the ICommand interface, providing a foundation for command implementations.
/// </summary>
internal abstract class BaseCommand : ICommand
{
   /// <summary>
   /// <inheritdoc/>
   /// </summary>
   public event EventHandler? CanExecuteChanged
   {
      add => CommandManager.RequerySuggested += value;
      remove => CommandManager.RequerySuggested -= value;
   }

   /// <summary>
   /// Defines the method to be called when the command is invoked.
   /// </summary>
   /// <param name="parameter">
   /// Data used by the command. If the command does not require data to be passed, this object can
   /// be set to null.
   /// </param>
   public abstract void Execute(object? parameter);

   /// <summary>
   /// Defines the method that determines whether the command can execute in its current state.
   /// </summary>
   /// <param name="parameter">
   /// Data used by the command. If the command does not require data to be passed, this object can
   /// be set to null.
   /// </param>
   /// <returns>True if this command can be executed; otherwise, false.</returns>
   public abstract bool CanExecute(object? parameter);
}

/// <summary>
/// A command whose sole purpose is to relay its functionality to other objects by invoking
/// delegates. The default return value for the CanExecute method is 'true'.
/// </summary>
internal class RelayCommand : BaseCommand
{
   private readonly Action<object?> _execute;
   private readonly Func<object?, bool>? _canExecute;

   /// <summary>
   /// Initializes a new instance of the RelayCommand class.
   /// </summary>
   /// <param name="execute">The execution logic.</param>
   /// <param name="canExecute">The execution status logic. If null, the command is always executable.</param>
   public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
   {
      _execute = execute ?? throw new ArgumentNullException(nameof(execute));
      _canExecute = canExecute;
   }

   /// <summary>
   /// Executes the command.
   /// </summary>
   /// <param name="parameter">
   /// Data used by the command. If the command does not require data to be passed, this object can
   /// be set to null.
   /// </param>
   public override void Execute(object? parameter) => _execute(parameter);

   /// <summary>
   /// Determines whether the command can execute in its current state.
   /// </summary>
   /// <param name="parameter">
   /// Data used by the command. If the command does not require data to be passed, this object can
   /// be set to null.
   /// </param>
   /// <returns>True if this command can be executed; otherwise, false.</returns>
   public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
}