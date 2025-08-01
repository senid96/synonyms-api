namespace Synonyms_API.Services
{
    public interface ISynonym
    {
        void AddSynonym(string word1, string word2);
        List<string> GetSynonyms(string word);
    }
}
