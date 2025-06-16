using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace RestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UploadImageFile(IFormFile ImageFile)
        {
            if(ImageFile == null || ImageFile.Length == 0)
            {
                return BadRequest();
            }
            string upDir = @"C:\Uploads";
            if (!Directory.Exists(upDir))
            {
                Directory.CreateDirectory(upDir);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(upDir, fileName);
            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }
            return Ok(filePath);
        }
        [HttpGet("{fileName}", Name = "GetImage")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult GetImageFile(string fileName)
        {
            string upDir = @"C:\Uploads";
            var filePath = Path.Combine(upDir, fileName);

            if (!System.IO.File.Exists(filePath))
                return BadRequest("Image Not Found");
            var Image = System.IO.File.OpenRead(filePath);
            var mime = GetMime(filePath);
            return File(Image, mime);
        }
        private string GetMime(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }
    }
}
