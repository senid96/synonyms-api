using Microsoft.AspNetCore.Mvc;
using Synonyms_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Synonyms_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynonymsController : ControllerBase
    {
        private readonly ISynonym _synonymService;

        public SynonymsController(ISynonym synonymService)
        {
            _synonymService = synonymService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromQuery] string word1, [FromQuery] string word2)
        {
            _synonymService.AddSynonym(word1, word2);
            return Ok();
        }

        [HttpGet("{word}")]
        public IActionResult Get(string word)
        {
            var synonyms = _synonymService.GetSynonyms(word);
            return Ok(synonyms);
        }
    }
}
