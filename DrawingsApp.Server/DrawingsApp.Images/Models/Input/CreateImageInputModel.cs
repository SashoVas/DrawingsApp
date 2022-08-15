namespace DrawingsApp.Images.Models.Input
{
    public class CreateImageInputModel
    {
        public IFormFile Image { get; set; }
        public string Name { get; set; }
    }
}
