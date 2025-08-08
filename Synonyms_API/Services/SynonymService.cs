namespace Synonyms_API.Services;

public class SynonymService : ISynonymService
{
    private readonly IUnionFindAlgorithm _unionFindAlgorithm;

    public SynonymService(IUnionFindAlgorithm unionFindService)
    {
        _unionFindAlgorithm = unionFindService;

        InitializeSynonyms();
    }

    public void AddSynonym(string wordA, string wordB)
    {
        _unionFindAlgorithm.Union(wordA.ToLower(), wordB.ToLower());
    }

    public List<string> GetSynonyms(string word)
    {
        return _unionFindAlgorithm.GetGroupMembers(word.ToLower());
    }

    public void ResetSynonyms()
    {
        _unionFindAlgorithm.ResetAddedSynonyms();
    }

    private void InitializeSynonyms()
    {
        var initialSynonyms = new List<(string, string)>
        {
            ("wash", "clean"),
            ("clean", "rinse"),
            ("quick", "fast"),
            ("rapid", "quick"),
            ("small", "tiny"),
            ("tiny", "mini")
        };

        foreach (var (a, b) in initialSynonyms)
        {
            AddSynonym(a, b);
        }
    }
}
