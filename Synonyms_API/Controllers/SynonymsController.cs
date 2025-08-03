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
        public IActionResult Add([FromBody] AddSynonymDTO synonyms)
        {
            _synonymService.AddSynonym(synonyms.FirstWord, synonyms.SecondWord);
            return Ok();
        }

        [HttpGet("{word}")]
        public IActionResult Get(string word = "wash")
        {
            var synonyms = _synonymService.GetSynonyms(word);
            return Ok(synonyms);
        }
    }
}
