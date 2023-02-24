using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Task3_14;

public partial class CreateAlbumWindow : Window
{
    private TextBox _albumName;
    public CreateAlbumWindow()
    {
        InitializeComponent();
        _albumName = this.Find<TextBox>("AlbumName");
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
       Close(_albumName.Text); 
    }
}