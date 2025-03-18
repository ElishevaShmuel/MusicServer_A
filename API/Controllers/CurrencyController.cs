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
        [HttpGet]
        public async Task<IActionResult> GetSum(string userId)
        {
            var sum= await _context.getSumAsync(userId);
            if(sum == -1)
                return BadRequest("User not found.");
            return Ok(sum);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]int cost,string userId)
        {
            var sum = await _context.AddAsync(cost,userId);
            if (sum == -1)
                return BadRequest("User not found.");
            return Ok(sum);
        }
    }
}
