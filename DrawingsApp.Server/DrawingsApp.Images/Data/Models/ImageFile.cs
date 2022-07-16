namespace DrawingsApp.Images.Data.Models
{
    public class ImageFile
    {
        public ImageFile()
        {
            Id =Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public string ImageFolder { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Expleanatoin { get; set; }
        public string UserId { get; set; }
    }
}
