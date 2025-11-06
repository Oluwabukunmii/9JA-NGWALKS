using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGWALKSAPI.Models.Domain;
using NGWALKSAPI.Models.DTO;
using NGWALKSAPI.API.Repositories;

namespace NGWALKSAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ImageController : ControllerBase
    {
        private readonly IimageRespository imageRespository;

        public ImageController(IimageRespository imageRespository)
        {
            this.imageRespository = imageRespository;
        }

        //  VERSION 1 
        [MapToApiVersion("1.0")]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadV1([FromForm] ImageUploadRequestDtoV1 request)
        {
            ValidateFileUpload(request.File);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageDomainModel = new Image
            {
                file = request.File,
                fileExtension = Path.GetExtension(request.File.FileName),
                fileSizeInByte = request.File.Length,
                fileName = request.Filename,  // DTO’s Filename property
                fileDescription = request.fileDescription
            };

            await imageRespository.Upload(imageDomainModel);

            return Ok(new
            {
                Version = "1.0",
                imageDomainModel.fileName,
                imageDomainModel.fileDescription
            });
        }

        //  VERSION 2 
        [MapToApiVersion("2.0")]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadV2([FromForm] ImageUploadRequestDtoV2 request)
        {
            ValidateFileUpload(request.File);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageDomainModel = new Image
            {
                file = request.File,
                fileExtension = Path.GetExtension(request.File.FileName),
                fileSizeInByte = request.File.Length,
                fileName = request.MyFilename, //  V2's MyFilename
                fileDescription = request.fileDescription
            };

            await imageRespository.Upload(imageDomainModel);

            return Ok(new
            {
                Version = "2.0",
                imageDomainModel.fileName,
                imageDomainModel.fileDescription
            });
        }

        // ======= Common Validation =======
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("file", "Unsupported file extension.");
            }

            if (file.Length > 10 * 1024 * 1024) // 10MB
            {
                ModelState.AddModelError("file", "File size exceeds 10MB limit.");
            }
        }
    }
}
