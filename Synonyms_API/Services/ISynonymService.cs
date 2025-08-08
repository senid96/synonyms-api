namespace Synonyms_API.Services;

public interface ISynonymService
{
    void AddSynonym(string wordA, string wordB);
    List<string> GetSynonyms(string word);
    void ResetSynonyms();
}