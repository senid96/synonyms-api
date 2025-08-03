namespace Synonyms_API
{
    public record AddSynonymDTO
    {
        public required string FirstWord { get; set; }
        public required string SecondWord { get; set; }  
    }
}
