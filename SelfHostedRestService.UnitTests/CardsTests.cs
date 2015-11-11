using NSubstitute;
using NUnit.Framework;
using SelfHostedRestService.Infrastructure;
using SelfHostedRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedRestService.UnitTests
{
    [TestFixture]
    public class CardsTests
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
        public void GetActiveWords_GivenMultipleWords_ReturnsAllWords()
        {
            var words = GetWordsList(50);
            var provider = new Cards(10, new NoShuffler());
            
            var cards = provider.GetActiveWords(words);

            Assert.AreEqual(50, cards.Length);
        }

        [Test]
        public void GetActiveWords_GivenNoMultipleWords_ReturnsOnlyMultipleWords()
        {
            var words = GetWordsList(52);
            var provider = new Cards(10, new NoShuffler());

            var cards = provider.GetActiveWords(words);

            Assert.AreEqual(50, cards.Length);
        }

        [Test]
        public void GetActiveWords_OtherMaxCardCount_ReturnsMultipleWords()
        {
            var words = GetWordsList(12);
            var provider = new Cards(3, new NoShuffler());

            var cards = provider.GetActiveWords(words);

            Assert.AreEqual(12, cards.Length);
        }

        [Test]
        public void GetActiveWords_EmptyWordList_ThrownArgumentException()
        {
            var provider = new Cards(10, new NoShuffler());
            var shortList = new string[] { "word1", "word2", "word3" };

            var ex = Assert.Catch<ArgumentOutOfRangeException>(() => provider.GetActiveWords(shortList));
            StringAssert.Contains("Слов слишком мало даже для одной карточки", ex.Message);
        }
    }
}
