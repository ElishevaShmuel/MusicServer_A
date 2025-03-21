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


        [HttpGet("get")]
        public async Task<IActionResult> getAllFile()
        {
            return await _context.getAllFiles();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] UploadViewModel userAndFile)
        {
            if (userAndFile.File == null || userAndFile.File.Length == 0)
                return BadRequest("Please upload a valid audio file.");

            var id = await _context.WriteAsync(userAndFile);
            return Ok(new { id });
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromBody] UploadViewModel userAndFileCost, string fileName)
        {

            var filePath = Path.Combine(_audioDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("קובץ השמע לא נמצא.");
            }

            return await _context.ReadAsync(userAndFileCost,filePath);
        }

        [HttpDelete("delete/{fileName}")]
        public IActionResult DeleteAudio([FromBody] User user, string fileName)
        {
            var filePath = Path.Combine(_audioDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("קובץ השמע לא נמצא.");
            }
            user.Files.Remove(user.Files.FirstOrDefault(f => f.FileName == fileName));
            System.IO.File.Delete(filePath);
            return Ok("קובץ השמע נמחק בהצלחה.");
        }
    }
}
