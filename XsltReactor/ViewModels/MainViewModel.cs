using System.Windows.Input;

using XsltReactor.Infrastructure.Commands;
using XsltReactor.Models;
using XsltReactor.Services;

namespace XsltReactor.ViewModels;

internal class MainViewModel : ObservableObject
{
   private readonly TransformService _transform = new();

   private string? _xslFile;

   /// <summary>
   /// Gets or sets the path to the XSL file.
   /// </summary>
   public string? XslFile
   {
      get => _xslFile;
      set => Set(ref _xslFile, value);
   }

   private string? _xmlFile;

   /// <summary>
   /// Gets or sets the path to the XML file.
   /// </summary>
   public string? XmlFile
   {
      get => _xmlFile;
      set => Set(ref _xmlFile, value);
   }

   private string? _xslText;

   /// <summary>
   /// Gets or sets the text content of the XSL file.
   /// </summary>
   public string? XslText
   {
      get => _xslText;
      set
      {
         if (Set(ref _xslText, value))
         {
            Transform();
         }
      }
   }

   private string? _xmlText;

   /// <summary>
   /// Gets or sets the text content of the XML file.
   /// </summary>
   public string? XmlText
   {
      get => _xmlText;
      set
      {
         if (Set(ref _xmlText, value))
         {
            Transform();
         }
      }
   }

   private string? _htmlText;

   /// <summary>
   /// Gets or sets the HTML text resulting from the XSLT transformation.
   /// </summary>
   public string? HtmlText
   {
      get => _htmlText;
      set => Set(ref _htmlText, value);
   }

   /// <summary>
   /// Command to open a file.
   /// </summary>
   public ICommand OpenFile { get; }

   /// <summary>
   /// Command to open a template file.
   /// </summary>
   public ICommand OpenTemplate { get; }

   /// <summary>
   /// Initializes a new instance of the MainViewModel class.
   /// </summary>
   public MainViewModel()
   {
      OpenFile = new OpenFileCommand(this);
      OpenTemplate = new OpenTemplateCommand(this);
   }

   private async void Transform()
   {
      if (string.IsNullOrWhiteSpace(XmlText) || string.IsNullOrWhiteSpace(XslText))
      {
         HtmlText = string.Empty;
         return;
      }

      HtmlText = await _transform.Transform(XmlText, XslText);
   }
}