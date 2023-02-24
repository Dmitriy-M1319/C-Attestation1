namespace Task3_14;

public class Photo
{
    public string Description { get; set; }
    public string RealPhotoPath { get; set; }

    public Photo(string description = "", string path = "")
    {
        Description = description;
        RealPhotoPath = path;
    }

}