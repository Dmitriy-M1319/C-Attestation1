using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task3_14;

public class PhotoAlbum
{
    public List<PhotoPage> Pages { get; set; }
    public string AlbumName { get; set; }
    
    public string AlbumPath { get; set; }

    public PhotoAlbum(string name)
    {
        Pages = new List<PhotoPage>();
        AlbumName = name;
        if (!Directory.Exists(name))
        {
            Directory.CreateDirectory(name);
        }
    }

    public void AddPhoto(Photo photo, ref int index)
    {
        if (Pages.Count != 0 && Pages.Last().Photos.Count < 3)
        {
            Pages.Last().Photos.Add(photo);
        }
        else
        {
            PhotoPage page = new PhotoPage(index);
            page.Photos.Add(photo);
            if (Pages.Count != 0)
                index++;
            Pages.Add(page);
        }
    }
    
    public int GetPhotoCount()
    {
        int sum = 0;
        foreach (var page in Pages)
        {
            sum += page.Photos.Count;
        }

        return sum;
    }
}