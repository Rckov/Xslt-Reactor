using ICSharpCode.AvalonEdit.Search;

using Wpf.Ui.Controls;

using XsltReactor.ViewModels;

namespace XsltReactor.Views;

public partial class MainView : FluentWindow
{
   public MainView()
   {
      InitializeComponent();

      InstallSearchPanels();
      InstallWebViewBrowser();
   }

   /// <summary>
   /// Installs search panels on the XSLEditor and XMLEditor text editors, enabling search functionality.
   /// </summary>
   private void InstallSearchPanels()
   {
      SearchPanel.Install(XSLEditor);
      SearchPanel.Install(XMLEditor);
   }

   /// <summary>
   /// Ensures the WebView component is properly initialized and sets up an event handler to update
   /// the WebView's content whenever the HtmlText property of the MainViewModel changes.
   /// </summary>
   private async void InstallWebViewBrowser()
   {
      await WebView.EnsureCoreWebView2Async(null);

      if (DataContext is MainViewModel viewModel)
      {
         viewModel.PropertyChanged += (s, e) =>
         {
            if (e.PropertyName == nameof(viewModel.HtmlText))
            {
               WebView.NavigateToString(viewModel.HtmlText);
            }
         };
      }
   }
}