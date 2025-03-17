using Core.Entities;
using Core.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioFileController : ControllerBase
    {
        private readonly IserAudioFile _context;
        private readonly IConfiguration _configuration;
        private readonly string _audioDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "audio");

        public AudioFileController(IserAudioFile context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid audio file.");

            var id = await _context.WriteAsync(file);

            return Ok(new { id });
        }
        public async Task<IActionResult> Download(string fileName)
        {

            var filePath = Path.Combine(_audioDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("קובץ השמע לא נמצא.");
            }

            return await _context.ReadAsync(filePath, fileName);
        }

        [HttpDelete("delete/{fileName}")]
        public IActionResult DeleteAudio(string fileName)
        {
            var filePath = Path.Combine(_audioDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("קובץ השמע לא נמצא.");
            }

            System.IO.File.Delete(filePath);
            return Ok("קובץ השמע נמחק בהצלחה.");
        }
    }
}
