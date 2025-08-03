using Synonyms_API.Services;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Synonyms_API.Tests
{
    public class SynonymServiceTests
    {
        [Fact]
        public void AddSynonym_ShouldGroupWords_AsSynonyms()
        {
            var service = new SynonymService();
            service.AddSynonym("happy", "joyful");

            var synonyms = service.GetSynonyms("happy");
            Assert.Contains("joyful", synonyms);
            Assert.DoesNotContain("happy", synonyms);
        }

        [Fact]
        public void GetSynonyms_ShouldReturnAllConnectedSynonyms()
        {
            var service = new SynonymService();
            service.AddSynonym("happy", "joyful");
            service.AddSynonym("joyful", "cheerful");

            var synonyms = service.GetSynonyms("happy");
            Assert.Contains("joyful", synonyms);
            Assert.Contains("cheerful", synonyms);
            Assert.Equal(2, synonyms.Count);
        }

        [Fact]
        public void GetSynonyms_ShouldReturnEmptyList_IfNoSynonyms()
        {
            var service = new SynonymService();
            var synonyms = service.GetSynonyms("nonexisting");
            Assert.Empty(synonyms);
        }

        [Fact]
        public void AddSynonym_ShouldBeCaseInsensitive()
        {
            var service = new SynonymService();
            service.AddSynonym("Happy", "Joyful");
            var synonyms = service.GetSynonyms("HAPPY");
            Assert.Contains("joyful", synonyms);
        }

        [Fact]
        public void InitialSynonyms_AreLoadedCorrectly()
        {
            var service = new SynonymService();
            var washSynonyms = service.GetSynonyms("wash");
            Assert.Contains("clean", washSynonyms);
            Assert.Contains("rinse", washSynonyms);
            Assert.Equal(2, washSynonyms.Count);

            var smallSynonyms = service.GetSynonyms("small");
            Assert.Contains("tiny", smallSynonyms);
            Assert.Contains("mini", smallSynonyms);
        }
    }
}