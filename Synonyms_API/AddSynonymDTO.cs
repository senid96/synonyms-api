namespace Synonyms_API;

public record AddSynonymDTO
{
    public required string FirstWord { get; init; }
    public required string SecondWord { get; init; }  
}
