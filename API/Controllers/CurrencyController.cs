using Core.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Iservice;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IserCurrency _context;
        private readonly IConfiguration _configuration;

        public CurrencyController(IserCurrency context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet("getSum")]
        public async Task<IActionResult> GetSum(int userId)
        {
            var sum= await _context.getSumAsync(userId);
            if(sum == -1)
                return BadRequest("User not found.");
            return Ok(sum);
        }

        [HttpPost("addSum")]
        public async Task<IActionResult> Add([FromBody]int cost,int userId)
        {
            var sum = await _context.AddAsync(cost,userId);
            if (sum == -1)
                return BadRequest("User not found.");
            return Ok(sum);
        }
        [HttpPost("subSum")]
        public async Task<IActionResult> sub([FromBody] int cost, int userId)
        {
            var sum = await _context.SubAsync(cost, userId);
            if (sum == -1)
                return BadRequest("User not found.");
            return Ok(sum);
        }
    }
}
