using Moq;
using Synonyms_API.Services;

namespace Synonyms_API.Tests
{
    public class SynonymServiceTests
    {
        private readonly Mock<IUnionFindAlgorithm> _unionFind;
        private readonly SynonymService _service;

        public SynonymServiceTests()
        {
            _unionFind = new Mock<IUnionFindAlgorithm>();
            _service = new SynonymService(_unionFind.Object);
        }

        [Fact]
        public void AddSynonym_ShouldLowercaseWords()
        {
            _service.AddSynonym("FAST", "Quick");

            _unionFind.Verify(s => s.Union("fast", "quick"), Times.Once);
        }

        [Fact]
        public void GetSynonyms_ShouldLowercaseWord_AndReturnList()
        {
            var expected = new List<string> { "quick", "rapid" };
            _unionFind.Setup(s => s.GetGroupMembers("fast")).Returns(expected);

            var result = _service.GetSynonyms("FAST");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ResetSynonyms_ShouldClearAndReinitialize()
        {
            _service.ResetSynonyms();

            _unionFind.Verify(s => s.ResetAddedSynonyms(), Times.Once);
        }
    }
}
