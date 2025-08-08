using Synonyms_API.Services;

namespace Synonyms_API.Tests
{
    public class UnionFindServiceTests
    {
        private readonly IUnionFindAlgorithm _service;

        public UnionFindServiceTests()
        {
            _service = new UnionFindAlgorithmImpl();
        }

        [Fact]
        public void Union_ShouldLinkWordsInBothDirections()
        {
            _service.Union("fast", "quick");
            _service.Union("quick", "rapid");

            var fastGroup = _service.GetGroupMembers("fast");

            Assert.Contains("quick", fastGroup);
            Assert.Contains("rapid", fastGroup);
        }

        [Fact]
        public void GetGroupMembers_ShouldReturnEmpty_WhenUnknown()
        {
            Assert.Empty(_service.GetGroupMembers("unknown"));
        }

        [Fact]
        public void GetGroupMembers_ShouldNotContainQueriedWord()
        {
            _service.Union("small", "tiny");
            var group = _service.GetGroupMembers("small");

            Assert.DoesNotContain("small", group);
            Assert.Contains("tiny", group);
        }
    }
}
