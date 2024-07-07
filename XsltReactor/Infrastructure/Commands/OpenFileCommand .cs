using XsltReactor.Infrastructure.Commands.Base;
using XsltReactor.Services;
using XsltReactor.ViewModels;

namespace XsltReactor.Infrastructure.Commands;

/// <summary>
/// Command to open a file with a specified extension and update the MainViewModel accordingly.
/// </summary>
internal class OpenFileCommand : BaseCommand
{
   private readonly MainViewModel _viewModel;
   private readonly FileService _fileService;

   /// <summary>
   /// Initializes a new instance of the OpenFileCommand class.
   /// </summary>
   /// <param name="viewModel">The MainViewModel to update with the file path.</param>
   public OpenFileCommand(MainViewModel viewModel)
   {
      _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
      _fileService = new FileService();
   }

   /// <summary>
   /// Executes the command to open a file with the specified extension.
   /// </summary>
   /// <param name="parameter">The file extension (e.g., ".xsl", ".xml").</param>
   public override void Execute(object? parameter)
   {
      if (parameter is string extension)
      {
         OpenFile(extension);
      }
   }

   /// <summary>
   /// Determines whether the command can execute in its current state.
   /// </summary>
   /// <param name="parameter">The parameter is not used in this implementation.</param>
   /// <returns>Always returns true.</returns>
   public override bool CanExecute(object? parameter)
   {
      return true;
   }

   /// <summary>
   /// Opens a file with the specified extension and updates the MainViewModel.
   /// </summary>
   /// <param name="extension">The file extension (e.g., ".xsl", ".xml").</param>
   private void OpenFile(string extension)
   {
      var pathFile = _fileService.OpenFile(extension);

      if (string.IsNullOrWhiteSpace(pathFile))
      {
         return;
      }

      switch (extension)
      {
         case ".xsl":
         _viewModel.XslFile = pathFile;
         break;

         case ".xml":
         _viewModel.XmlFile = pathFile;
         break;

         default:
         throw new InvalidOperationException($"Unsupported file extension: {extension}");
      }
   }
}