namespace Synonyms_API.Services;

public interface IUnionFindAlgorithm
{
    void Union(string wordA, string wordB);
    List<string> GetGroupMembers(string word);
    void ResetAddedSynonyms();
}
