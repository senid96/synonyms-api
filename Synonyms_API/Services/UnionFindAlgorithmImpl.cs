namespace Synonyms_API.Services;

public class UnionFindAlgorithmImpl : IUnionFindAlgorithm
{
    private readonly Dictionary<string, string> childToParentMap = [];

    private string GetRootWord(string word)
    {
        if (!childToParentMap.TryGetValue(word, out var p))
            return childToParentMap[word] = word; // Initialize if not present

        if (p != word)
            childToParentMap[word] = GetRootWord(p);

        return childToParentMap[word];
    }

    public void Union(string wordA, string wordB)
    {
        var rootA = GetRootWord(wordA);
        var rootB = GetRootWord(wordB);

        if (rootA != rootB)
            childToParentMap[rootA] = rootB;
    }

    public List<string> GetGroupMembers(string word)
    {
        var root = GetRootWord(word.ToLower());

        return childToParentMap.Keys
            .Where(w => w != word.ToLower() && GetRootWord(w) == root)
            .ToList();
    }

    public void ResetAddedSynonyms()
    {
        childToParentMap.Clear();
    }
}
