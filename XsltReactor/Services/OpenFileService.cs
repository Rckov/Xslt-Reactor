using Microsoft.Win32;

using System.IO;
using System.Reflection;

namespace XsltReactor.Services;

/// <summary>
/// Service class for handling file operations such as opening files and loading files from resources.
/// </summary>
internal class FileService
{
   /// <summary>
   /// Opens a file dialog to select a file with a specified filter and returns the selected file path.
   /// </summary>
   /// <param name="filter">The filter string to use in the file dialog.</param>
   /// <returns>The path of the selected file, or an empty string if no file was selected.</returns>
   public string OpenFile(string filter)
   {
      var dialog = new OpenFileDialog
      {
         Filter = $"File (*{filter})|*{filter}"
      };

      return dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
   }

   /// <summary>
   /// Loads a file from the application's resources asynchronously and returns its content as a string.
   /// </summary>
   /// <param name="fileName">The name of the resource file to load.</param>
   /// <returns>
   /// The content of the resource file as a string, or an empty string if the resource was not found.
   /// </returns>
   public async Task<string> LoadFileFromResourcesAsync(string fileName)
   {
      var assembly = Assembly.GetExecutingAssembly();
      using var stream = assembly.GetManifestResourceStream(fileName);

      if (stream is null)
      {
         return string.Empty;
      }

      using var reader = new StreamReader(stream);
      return await reader.ReadToEndAsync();
   }
}