namespace Synonyms_API.Services
{
    public class SynonymService : ISynonym
    {
        private readonly Dictionary<string, string> parent = [];
        private readonly Dictionary<string, HashSet<string>> groups = [];

        private string Find(string word)
        {
            if (!parent.ContainsKey(word))
            {
                parent[word] = word;
                groups[word] = new HashSet<string> { word };
            }

            if (parent[word] != word)
                parent[word] = Find(parent[word]);

            return parent[word];
        }

        private void Union(string a, string b)
        {
            var rootA = Find(a);
            var rootB = Find(b);

            if (rootA == rootB) return;

            // Merge smaller group into larger group
            if (groups[rootA].Count < groups[rootB].Count)
                (rootA, rootB) = (rootB, rootA);

            foreach (var word in groups[rootB])
            {
                parent[word] = rootA;
                groups[rootA].Add(word);
            }

            groups.Remove(rootB);
        }

        public void AddSynonym(string word1, string word2)
        {
            Union(word1.ToLower(), word2.ToLower());
        }

        public List<string> GetSynonyms(string word)
        {
            var root = Find(word.ToLower());
            return new List<string>(groups[root].Where(w => w != word.ToLower()));
        }
    }
}
