using XsltReactor.Infrastructure.Commands.Base;
using XsltReactor.Services;
using XsltReactor.ViewModels;

namespace XsltReactor.Infrastructure.Commands;

/// <summary>
/// Command to open a template file from resources and update the MainViewModel with its content.
/// </summary>
internal class OpenTemplateCommand : BaseCommand
{
   private readonly MainViewModel _viewModel;
   private readonly FileService _fileService;

   /// <summary>
   /// Initializes a new instance of the OpenTemplateCommand class.
   /// </summary>
   /// <param name="viewModel">The MainViewModel to update with the file content.</param>
   public OpenTemplateCommand(MainViewModel viewModel)
   {
      _viewModel = viewModel;
      _fileService = new FileService();
   }

   /// <summary>
   /// Executes the command to open a file from resources asynchronously.
   /// </summary>
   /// <param name="parameter">The resource identifier of the file to open.</param>
   public override async void Execute(object? parameter)
   {
      if (parameter is string fileResource)
      {
         await OpenFileAsync(fileResource);
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
   /// Opens a file from resources asynchronously and updates the MainViewModel with its content.
   /// </summary>
   /// <param name="fileResource">The resource identifier of the file to open.</param>
   private async Task OpenFileAsync(string fileResource)
   {
      _viewModel.XslText = await _fileService.LoadFileFromResourcesAsync(fileResource);
   }
}