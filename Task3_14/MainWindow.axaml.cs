using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using StackPanel = Avalonia.Controls.StackPanel;

namespace Task3_14;

public partial class MainWindow : Window
{
    public PhotoAlbum Album { get; set; }
    private int createPageIndex = 1;
    private int currentPageIndex = 1;
    private delegate void ShowPhotosHandler();
    private event ShowPhotosHandler ShowPhotosEvent;
    public MainWindow()
    {
        InitializeComponent();
        ShowPhotosEvent += ShowPhotosOnPage;
    }

    private void ShowPhotosOnPage()
    {
        photos.Children.Clear();
        if (Album.Pages.Count != 0)
        {
            photos.Children.Clear();
            foreach (var page in Album.Pages)
            {
                if (page.PageNumber == currentPageIndex)
                {
                    foreach (var photo in page.Photos)
                    {
                        StackPanel panel = new StackPanel();
                        TextBlock name = new TextBlock();
                        name.Text = photo.Description;
                        name.HorizontalAlignment = HorizontalAlignment.Center;
                        panel.Children.Add(name);
                        Image image = new Image();
                        image.Source = new Bitmap(photo.RealPhotoPath);
                        panel.Children.Add(image);
                        photos.Children.Add(panel);
                    } 
                } 
            }
        }
    }

    private async void MenuItemCreateAlbum_OnClick(object? sender, RoutedEventArgs e)
    {
        var createDlg = new CreateAlbumWindow();
        var albumName = await createDlg.ShowDialog<string>(this);
        Album = new PhotoAlbum(albumName);
        Album.AlbumPath = albumName;
        albumNameblock.Text = "Альбом: " + albumName;
    }

    private void LoadAlbum(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        Album = new PhotoAlbum(path.Split('/')[^1]);
        Album.AlbumPath = dir.FullName;
        albumNameblock.Text = Album.AlbumName;
        int photoNumber = 1, pageIndex = 1;
        PhotoPage page = null;
        var photoFiles = dir.GetFiles();
        foreach (var file in photoFiles)
        {
            string[] tokens = file.Name.Split(".");
            if (photoNumber == 1) {
                page = new PhotoPage(pageIndex);
                page.Photos.Add(new Photo(tokens[0], file.FullName));
                photoNumber++;
            }
            else if (photoNumber == 3)
            {
                page.Photos.Add(new Photo(tokens[0], file.FullName));
                Album.Pages.Add(page);
                photoNumber = 1;
                pageIndex++;
            }
            else
            {
                page.Photos.Add(new Photo(tokens[0], file.FullName));
                photoNumber++;
            }
        }
        
        if (page != null && page.Photos.Count != 3)
            Album.Pages.Add(page);
        
        currentPageIndex = 1;
    }
    
    private async void MenuItemOpenAlbum_OnClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new OpenFolderDialog();
        var result = await dlg.ShowAsync(this);
        if (result != null)
        {
            LoadAlbum(result);
            ShowPhotosEvent();
        }
    }
    private void MenuItemClearAlbum_onClick(object? sender, RoutedEventArgs e)
    {
        Album.Pages.Clear();
        albumNameblock.Text = "";
        createPageIndex = 1;
        currentPageIndex = 1;
        Directory.Delete(Album.AlbumName, true);
        ShowPhotosEvent();
    }

    private async void MenuItemAddPhoto_OnClick(object? sender, RoutedEventArgs e)
    {
        var addDlg = new AddPhotoWindow();
        var result = await addDlg.ShowDialog<string[]>(this);
        var tokens = result[0].Split(".");
        string newPath = Album.AlbumName + "/" + result[1] + "." + tokens[^1];
        File.Copy(result[0], newPath);
        Photo photo = new Photo(result[1], newPath);
        Album.AddPhoto(photo, ref createPageIndex);
        currentPageIndex = createPageIndex;
        ShowPhotosEvent();
    }

    private void ButtonPageBack_OnClick(object? sender, RoutedEventArgs e)
    {
        if (currentPageIndex != 1)
        {
            currentPageIndex--;
            ShowPhotosEvent();
        }
    }
    private void ButtonPageForward_OnClick(object? sender, RoutedEventArgs e)
    {
        if (currentPageIndex != Album.Pages.Count )
        {
            currentPageIndex++;
            ShowPhotosEvent();
        }
    }

    private async void MenuItemDeletePhoto_OnClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog();
        dlg.Directory = Album.AlbumPath;
        dlg.Filters.Add(new FileDialogFilter() { Name = "Image Files", Extensions = { "png", "jpg", "jpeg" } });
        var result = await dlg.ShowAsync(this);
        if (result != null)
        {
            File.Delete(result[0]);
            LoadAlbum(Album.AlbumPath);
            photoCount.Text = "";
            ShowPhotosEvent();
        }    
    }

    private void MenuItemCountPhotos_OnClick(object? sender, RoutedEventArgs e)
    {
        photoCount.Text = "Количество фотографий в альбоме - " + Album.GetPhotoCount().ToString();
    }
}