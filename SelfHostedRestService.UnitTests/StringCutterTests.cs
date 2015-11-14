using NSubstitute;
using NUnit.Framework;
using SelfHostedRestService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedRestService.UnitTests
{
    [TestFixture]
    public class StringCutterTests
    {
        #region Helpers
        private string[] GetWordsList()
        {
            return new string[] { "word1", "word2", "word3", "word4", "word5" };
        }

        private string[] GetWordsList(int count)
        {
            return Enumerable.Range(0, count)
                .Select(x => "word" + x)
                .ToArray();
        }

        private class NoShuffler : IShuffler<string>
        {
            public IEnumerable<string> Shuffle(IEnumerable<string> collection)
            {
                return collection;
            }
        }
        #endregion

        [Test]
        public void CutMultipleOfBasis_GivenMultipleWords_ReturnsAllWords()
        {
            var words = GetWordsList(50);
            var provider = new StringCutter();

            var cards = provider.CutMultipleOfBasis(words, 10);

            Assert.AreEqual(50, cards.Length);
        }

        [Test]
        public void CutMultipleOfBasis_GivenNoMultipleWords_ReturnsOnlyMultipleWords()
        {
            var words = GetWordsList(52);
            var provider = new StringCutter();

            var cards = provider.CutMultipleOfBasis(words, 10);

            Assert.AreEqual(50, cards.Length);
        }

        [Test]
        public void CutMultipleOfBasis_OtherMaxCardCount_ReturnsOtherMultipleWords()
        {
            var words = GetWordsList(12);
            var provider = new StringCutter();

            var cards = provider.CutMultipleOfBasis(words, 3);

            Assert.AreEqual(12, cards.Length);
        }

        [Test]
        public void CutMultipleOfBasis_EmptyWordList_ThrownArgumentOutOfRangeException()
        {
            var provider = new StringCutter();
            var shortList = new string[] { "word1", "word2", "word3" };

            var ex = Assert.Catch<ArgumentOutOfRangeException>(() => provider.CutMultipleOfBasis(shortList, 10));
            StringAssert.Contains("Слов слишком мало", ex.Message);
        }

        [Test]
        public void CutMultipleOfBasis_NullEntities_ThrownArgumentNullException()
        {
            var provider = new StringCutter();

            var ex = Assert.Catch<ArgumentNullException>(() => provider.CutMultipleOfBasis(null, -1));
            StringAssert.Contains("Ссылка", ex.Message);
        }

        [Test]
        public void CutMultipleOfBasis_ZeroBasis_ThrownArgumentException()
        {
            var provider = new StringCutter();
            var words = GetWordsList();

            var ex = Assert.Catch<ArgumentException>(() => provider.CutMultipleOfBasis(words, 0));
            StringAssert.Contains("Базис", ex.Message);
        }

        [Test]
        public void CutMultipleOfBasis_ZeroLessBasis_ThrownArgumentException()
        {
            var provider = new StringCutter();
            var words = GetWordsList();

            var ex = Assert.Catch<ArgumentException>(() => provider.CutMultipleOfBasis(words, -1));
            StringAssert.Contains("Базис", ex.Message);
        }
    }
}
