using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XsltReactor.Models;

/// <summary>
/// A base class for implementing the INotifyPropertyChanged interface, providing helper methods for
/// property change notifications.
/// </summary>
internal class ObservableObject : INotifyPropertyChanged
{
   /// <summary>
   /// <inheritdoc/>
   /// </summary>
   public event PropertyChangedEventHandler? PropertyChanged;

   /// <summary>
   /// Raises the PropertyChanged event to notify the change of the property.
   /// </summary>
   /// <param name="propertyName">
   /// The name of the property that changed. This parameter is optional and will be filled
   /// automatically by the compiler if not provided.
   /// </param>
   protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
   {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }

   /// <summary>
   /// Sets the value of a field and raises the PropertyChanged event if the value has changed.
   /// </summary>
   /// <typeparam name="T">The type of the field and value.</typeparam>
   /// <param name="field">A reference to the field that is to be set.</param>
   /// <param name="value">The new value to set the field to.</param>
   /// <param name="propertyName">
   /// The name of the property that is being set. This parameter is optional and will be filled
   /// automatically by the compiler if not provided.
   /// </param>
   /// <returns>
   /// True if the value was changed (i.e., the new value was different from the current field
   /// value); otherwise, false.
   /// </returns>
   protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
   {
      if (EqualityComparer<T>.Default.Equals(field, value))
      {
         return false;
      }

      field = value;
      OnPropertyChanged(propertyName);

      return true;
   }
}