using ICSharpCode.AvalonEdit;

using Microsoft.Xaml.Behaviors;

using System.IO;
using System.Windows;

namespace XsltReactor.Infrastructure.Behaviors;

/// <summary>
/// A behavior for the ICSharpCode.AvalonEdit.TextEditor control that synchronizes the text content
/// with a dependency property and allows loading text from a file.
/// </summary>
internal class TextEditorTextBehavior : Behavior<TextEditor>
{
   /// <summary>
   /// Dependency property for the text content of the TextEditor.
   /// </summary>
   public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
      nameof(Text),
      typeof(string),
      typeof(TextEditorTextBehavior),
      new PropertyMetadata(string.Empty, OnTextChanged));

   /// <summary>
   /// Dependency property for the file name from which to load the text content.
   /// </summary>
   public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(
      nameof(FileName),
      typeof(string),
      typeof(TextEditorTextBehavior),
      new PropertyMetadata(null, OnFileNameChanged));

   /// <summary>
   /// Gets or sets the file name from which to load the text content.
   /// </summary>
   public string FileName
   {
      get { return (string)GetValue(FileNameProperty); }
      set { SetValue(FileNameProperty, value); }
   }

   /// <summary>
   /// Gets or sets the text content of the TextEditor.
   /// </summary>
   public string Text
   {
      get => (string)GetValue(TextProperty);
      set => SetValue(TextProperty, value);
   }

   /// <summary>
   /// Called when the behavior is attached to the TextEditor.
   /// </summary>
   protected override void OnAttached()
   {
      base.OnAttached();

      if (AssociatedObject is null)
      {
         return;
      }

      AssociatedObject.TextChanged += OnTextEditorTextChanged;
   }

   /// <summary>
   /// Called when the behavior is detaching from the TextEditor.
   /// </summary>
   protected override void OnDetaching()
   {
      base.OnDetaching();

      if (AssociatedObject is null)
      {
         return;
      }

      AssociatedObject.TextChanged -= OnTextEditorTextChanged;
   }

   /// <summary>
   /// Handles the TextChanged event of the TextEditor to update the Text property.
   /// </summary>
   private void OnTextEditorTextChanged(object? sender, EventArgs e)
   {
      if (AssociatedObject is null || AssociatedObject.Text == Text)
      {
         return;
      }

      Text = AssociatedObject.Text;
   }

   /// <summary>
   /// Called when the Text property changes.
   /// </summary>
   private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   {
      var behavior = (TextEditorTextBehavior)d;

      if (behavior.AssociatedObject is null)
      {
         return;
      }

      var newText = (string)e.NewValue;

      if (newText == behavior.AssociatedObject.Text)
      {
         return;
      }

      behavior.AssociatedObject.Text = newText;
   }

   /// <summary>
   /// Called when the FileName property changes.
   /// </summary>
   private static void OnFileNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   {
      if (d is TextEditorTextBehavior behavior && behavior.AssociatedObject != null)
      {
         behavior.OpenFile();
      }
   }

   /// <summary>
   /// Opens the file specified by the FileName property and loads its content into the TextEditor.
   /// </summary>
   private void OpenFile()
   {
      if (File.Exists(FileName))
      {
         AssociatedObject.Load(FileName);
      }
   }
}