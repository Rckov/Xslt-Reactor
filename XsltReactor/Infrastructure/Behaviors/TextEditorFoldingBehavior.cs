using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;

using Microsoft.Xaml.Behaviors;

namespace XsltReactor.Infrastructure.Behaviors;

/// <summary>
/// A behavior for the ICSharpCode.AvalonEdit.TextEditor control that enables folding (collapsible
/// regions) for XML content.
/// </summary>
internal class TextEditorFoldingBehavior : Behavior<TextEditor>
{
   private FoldingManager? _foldingManager;
   private XmlFoldingStrategy? _foldingStrategy;

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

      _foldingManager = FoldingManager.Install(AssociatedObject.TextArea);
      _foldingStrategy = new XmlFoldingStrategy();

      AssociatedObject.TextChanged += OnTextEditorTextChanged;
   }

   /// <summary>
   /// Called when the behavior is detaching from the TextEditor.
   /// </summary>
   protected override void OnDetaching()
   {
      base.OnDetaching();

      if (_foldingManager is not null)
      {
         FoldingManager.Uninstall(_foldingManager);
         _foldingManager = null;
      }
   }

   /// <summary>
   /// Updates the foldings in the TextEditor based on the current text content.
   /// </summary>
   private void UpdateFolding()
   {
      if (_foldingManager is null || _foldingStrategy is null)
      {
         return;
      }

      _foldingStrategy.UpdateFoldings(_foldingManager, AssociatedObject.Document);
   }

   /// <summary>
   /// Handles the TextChanged event of the TextEditor to update the foldings.
   /// </summary>
   private void OnTextEditorTextChanged(object? sender, EventArgs e)
   {
      UpdateFolding();
   }
}