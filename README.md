**Xslt Reactor** - a simple XSLT editor. The application allows you to edit XSL and XML files and view the transformation results using the integrated WebView.

## 📦 Installation
1. Ensure you have .NET SDK version 6.0 or higher installed.
2. Clone the repository:
    ```sh
    git clone https://github.com/Rckov/XsltReactor.git
    ```
3. Navigate to the project directory:
    ```sh
    cd XsltReactor
    ```
4. Restore dependencies and build the project:
    ```sh
    dotnet restore
    dotnet build
    ```
5. Run the application:
    ```sh
    dotnet run --project XsltReactor
    ```

## 🚀 Usage
- **Open XSL or XML file**: Use the "Open XSL file" or "Open XML file" menu to load files.
- **Edit code**: Use the text editors to make changes to the XSL and XML files.
- **View results**: Transformation results will be displayed in the WebView.
- **Save changes**: Use the standard save commands to save your changes.

## ⚙️ Configuration
To build the project on operating systems other than Windows, set the `EnableWindowsTargeting` property to `true` in the `.csproj` files.

## 📋 Dependencies
- [AvalonEdit](https://github.com/icsharpcode/AvalonEdit)
- [Microsoft.Web.WebView2](https://www.nuget.org/packages/Microsoft.Web.WebView2/)
- [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/)
- [WPF-UI](https://github.com/lepoco/wpfui)
