using System.Collections.Generic;

namespace Task3_14;

public class PhotoPage
{
    /**
     * Пусть на странице располагается по 3 фотографии
     */
    public List<Photo> Photos { get; set; }
    public int PageNumber { get; }

    public PhotoPage(int number)
    {
        Photos = new List<Photo>();
        PageNumber = number;
    }

}