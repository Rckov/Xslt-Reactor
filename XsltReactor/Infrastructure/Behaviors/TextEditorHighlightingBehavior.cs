using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

using Microsoft.Xaml.Behaviors;

using System.Reflection;
using System.Windows;
using System.Xml;

namespace XsltReactor.Infrastructure.Behaviors;

/// <summary>
/// A behavior for the ICSharpCode.AvalonEdit.TextEditor control that allows dynamic loading of
/// syntax highlighting definitions from embedded resources.
/// </summary>
internal class TextEditorHighlightingBehavior : Behavior<TextEditor>
{
   /// <summary>
   /// Dependency property for the highlighting definition resource name.
   /// </summary>
   public static readonly DependencyProperty HighlightingDefinitionProperty = DependencyProperty.Register(
      nameof(HighlightingDefinition),
      typeof(string),
      typeof(TextEditorHighlightingBehavior),
      new PropertyMetadata(null, OnHighlightingDefinitionChanged));

   /// <summary>
   /// Gets or sets the name of the embedded resource containing the syntax highlighting definition.
   /// </summary>
   public string HighlightingDefinition
   {
      get => (string)GetValue(HighlightingDefinitionProperty);
      set => SetValue(HighlightingDefinitionProperty, value);
   }

   /// <summary>
   /// Called when the behavior is attached to the TextEditor.
   /// </summary>
   protected override void OnAttached()
   {
      base.OnAttached();
      LoadHighlightingDefinition();
   }

   /// <summary>
   /// Loads the syntax highlighting definition from the embedded resource.
   /// </summary>
   private void LoadHighlightingDefinition()
   {
      if (AssociatedObject is null || string.IsNullOrEmpty(HighlightingDefinition))
      {
         return;
      }

      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = HighlightingDefinition;

      using var stream = assembly.GetManifestResourceStream(resourceName);

      if (stream is null)
      {
         return;
      }

      using var reader = XmlReader.Create(stream);

      var customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
      AssociatedObject.SyntaxHighlighting = customHighlighting;
   }

   /// <summary>
   /// Called when the HighlightingDefinition property changes.
   /// </summary>
   /// <param name="d">The dependency object.</param>
   /// <param name="e">The event arguments.</param>
   private static void OnHighlightingDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
   {
      var behavior = (TextEditorHighlightingBehavior)d;
      behavior.LoadHighlightingDefinition();
   }
}