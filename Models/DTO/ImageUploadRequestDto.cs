namespace NGWALKSAPI.Models.DTO
{
    public class ImageUploadRequestDtoV1
    {

        public IFormFile File { get; set; }

        public string Filename { get; set; }

        public string? fileDescription { get; set; }
    }

    public class ImageUploadRequestDtoV2
    {

        public IFormFile File { get; set; }

        public string MyFilename { get; set; }

        public string? fileDescription { get; set; }
    }
}
