// ChatController.cs
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _openAiApiKey;

    public ChatController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _openAiApiKey = configuration["OpenAI:ApiKey"];
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
    {
        if (string.IsNullOrEmpty(_openAiApiKey))
        {
            return BadRequest("OpenAI API key not configured");
        }

        try
        {
            var openAiRequest = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "אתה עוזר וירטואלי של אתר מיוזיק - פלטפורמה לשיתוף מוזיקה. תמיד תענה בעברית ובצורה ידידותית." }
                }.Concat(request.Messages.Select(m => new { role = m.Role, content = m.Content })),
                max_tokens = 500,
                temperature = 0.7
            };

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _openAiApiKey);

            var response = await _httpClient.PostAsJsonAsync(
                "https://api.openai.com/v1/chat/completions",
                openAiRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAiResponse>();
                return Ok(new { message = result.Choices[0].Message.Content });
            }

            return StatusCode(500, "Failed to get response from OpenAI");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}

