using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeAiIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaudeController : ControllerBase
    {
        private readonly ClaudeService _claudeService;

        public ClaudeController(ClaudeService claudeService)
        {
            _claudeService = claudeService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskClaude([FromBody] string prompt)
        {
            string apiKey = "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";  
            var response = await _claudeService.GetClaudeResponse(prompt, apiKey);
            return Ok(response);
        }
    }
}
