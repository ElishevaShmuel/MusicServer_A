using Core.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Iservice;
using Amazon.S3;
using Core.Entities;
using Amazon.S3.Model;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioFileController : ControllerBase
    {
        private readonly IserAudioFile _context;
        private readonly IConfiguration _configuration;
        private readonly string _audioDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "audio");
        private readonly IAmazonS3 _s3Client;

        public AudioFileController(IserAudioFile context, IConfiguration configuration, IAmazonS3 s3Client)
        {
            _context = context;
            _configuration = configuration;
            _s3Client = s3Client;

        }


        [HttpGet("get")]
        public async Task<IActionResult> getAllFile()
        {
            var files = await _context.getAllFiles();
            return Ok(files);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> getById(int id)
        {
            var files = await _context.getById(id);
            return Ok(files);
        }


        [HttpGet("Upload")]
        public async Task<IActionResult> GetPresignedUrl([FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("File name is required.");
            }

            var request = new GetPreSignedUrlRequest
            {
                BucketName = "musicommunity",
                Key = fileName,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(50),
                ContentType = "audio/mpeg"
            };

            string url = _s3Client.GetPreSignedURL(request);

            return Ok(new { url });
        }

        [HttpPost("save")]
        public async Task<IActionResult> save([FromBody] MusicFile file)
        {
            if (file == null)
            {
                return BadRequest("נתוני הקובץ אינם תקינים.");
            }
            Console.WriteLine(JsonConvert.SerializeObject(file));
            await _context.WriteAsync(file);

            return Ok("קובץ השמע נשמר בהצלחה.");
        }


        [HttpGet("Download")]
        public IActionResult GetPresignedDownloadUrl(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("יש לספק שם קובץ.");
            }

            var request = new GetPreSignedUrlRequest
            {
                BucketName = "musicommunity",
                Key = fileName,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(60) // תוקף של שעה
            };

            string url = _s3Client.GetPreSignedURL(request);
            return Ok(new { url });
        }


        [HttpDelete("Delete")]
        public IActionResult DeleteAudio([FromBody] MusicFile file)
        {
            var res = _context.removeAsync(file);
            //מחזיר אם הקובץ נמחק בהצלחה
            if (res.Result == -1)
            {
                return NotFound("קובץ השמע לא נמצא.");
            }
            else
            {
                return Ok("קובץ השמע נמחק בהצלחה.");
            }
        }

    }
}
