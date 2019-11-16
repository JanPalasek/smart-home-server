namespace SmartHome.Web.Configurations
{
    public class FileManagerConfiguration
    {
        public double MaximumUploadSize { get; set; }
        public string StoragePath { get; set; } = null!;
    }
}