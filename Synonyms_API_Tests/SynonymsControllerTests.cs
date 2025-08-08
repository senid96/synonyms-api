using Microsoft.AspNetCore.Mvc;
using Moq;
using Synonyms_API.Controllers;
using Synonyms_API.Services;

namespace Synonyms_API.Tests
{
    public class SynonymsControllerTests
    {
        private readonly Mock<ISynonymService> _synonymsService;
        private readonly SynonymsController _controller;

        public SynonymsControllerTests()
        {
            _synonymsService = new Mock<ISynonymService>();
            _controller = new SynonymsController(_synonymsService.Object);
        }

        private static OkObjectResult AssertOkWithValue(IActionResult result, object? expectedValue = null)
        {
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ok.StatusCode);
            if (expectedValue != null)
                Assert.Equal(expectedValue, ok.Value);
            return ok;
        }

        [Fact]
        public void Add_ShouldCallService_AndReturnMessage()
        {
            var dto = new AddSynonymDTO { FirstWord = "car", SecondWord = "vehicle" };

            var result = _controller.Add(dto);

            _synonymsService.Verify(s => s.AddSynonym("car", "vehicle"), Times.Once);
            AssertOkWithValue(result, "New synonyms added.");
        }

        [Fact]
        public void Get_ShouldReturnSynonyms()
        {
            const string word = "wash";
            var expectedSynonyms = new List<string> { "clean", "rinse" };
            _synonymsService.Setup(s => s.GetSynonyms(word)).Returns(expectedSynonyms);

            var result = _controller.Get(word);

            AssertOkWithValue(result, expectedSynonyms);
            _synonymsService.Verify(s => s.GetSynonyms(word), Times.Once);
        }

        [Fact]
        public void Reset_ShouldCallService_AndReturnMessage()
        {
            var result = _controller.Reset();

            _synonymsService.Verify(s => s.ResetSynonyms(), Times.Once);
            AssertOkWithValue(result, "Synonyms reset to initial state.");
        }
    }
}
