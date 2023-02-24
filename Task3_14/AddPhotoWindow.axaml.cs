using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Task3_14;

public partial class AddPhotoWindow : Window
{
    private TextBox _photoName;
    public AddPhotoWindow()
    {
        InitializeComponent();
        _photoName = this.Find<TextBox>("photoName");
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog();
        dlg.Filters.Add(new FileDialogFilter() { Name = "Image Files", Extensions = { "png", "jpg", "jpeg" } });
        dlg.Filters.Add(new FileDialogFilter() { Name = "All Files", Extensions = { "*" } });
        var result = await dlg.ShowAsync(this);
        if (result != null)
        {
            Close(new string[] {result[0], _photoName.Text}); 
        }
    }
}