namespace Synonyms_API.Services;

public class ThreadSafeUnionFindAlgorithmDecorator : IUnionFindAlgorithm
{
    private readonly IUnionFindAlgorithm _unionFindAlgorithm;
    private readonly ReaderWriterLockSlim _lock = new();

    public ThreadSafeUnionFindAlgorithmDecorator(IUnionFindAlgorithm unionFindAlgorithm)
    {
        _unionFindAlgorithm = unionFindAlgorithm;
    }

    public void Union(string wordA, string wordB)
    {
        _lock.EnterWriteLock();
        try
        {
            _unionFindAlgorithm.Union(wordA, wordB);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public List<string> GetGroupMembers(string word)
    {
        _lock.EnterReadLock();
        try
        {
            return _unionFindAlgorithm.GetGroupMembers(word);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void ResetAddedSynonyms()
    {
        _lock.EnterWriteLock();
        try
        {
            _unionFindAlgorithm.ResetAddedSynonyms();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
}
