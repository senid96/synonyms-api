using Microsoft.AspNetCore.Mvc;
using Synonyms_API.Services;

namespace Synonyms_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SynonymsController : ControllerBase
{
    private readonly ISynonymService _synonymService;

    public SynonymsController(ISynonymService synonymService)
    {
        _synonymService = synonymService;
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] AddSynonymDTO synonyms)
    {
        try
        {
            _synonymService.AddSynonym(synonyms.FirstWord, synonyms.SecondWord);
            return Ok("New synonyms added.");
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while adding the synonym.");
        }
    }

    [HttpGet("{word}")]
    public IActionResult Get(string word = "wash")
    {
        try
        {
            var synonyms = _synonymService.GetSynonyms(word);
            return Ok(synonyms);
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while retrieving synonyms.");
        }
    }

    [HttpGet("reset")]
    public IActionResult Reset()
    {
        _synonymService.ResetSynonyms();
        return Ok("Synonyms reset to initial state.");
    }
}
