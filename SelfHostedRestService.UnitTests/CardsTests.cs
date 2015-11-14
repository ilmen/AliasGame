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

        private class StubShuffler : IShuffler<string>
        {
            public IEnumerable<string> Shuffle(IEnumerable<string> collection)
            {
                return collection;
            }
        }

        private class StubCutter : ICutter<string>
        {
            public string[] CutMultipleOfBasis(string[] entities, int basis)
            {
                return entities;
            }
        }
        #endregion

        [Test]
        public void Ctor_ZeroWordsCountInOneCard_ThrownsArgumentException()
        {
            var shuffler = Substitute.For<IShuffler<string>>();
            var cutter = Substitute.For<ICutter<string>>();

            var ex = Assert.Catch<ArgumentException>(() => new Cards(0, shuffler, cutter));
            StringAssert.Contains("wordsCountInOneCard", ex.Message);
        }

        [Test]
        public void Ctor_ZeroLessWordsCountInOneCard_ThrownsArgumentException()
        {
            var shuffler = Substitute.For<IShuffler<string>>();
            var cutter = Substitute.For<ICutter<string>>();

            var ex = Assert.Catch<ArgumentException>(() => new Cards(-1, shuffler, cutter));
            StringAssert.Contains("wordsCountInOneCard", ex.Message);
        }

        [Test]
        public void GetCards_Always_UseShuffler()
        {
            var shuffler = Substitute.For<IShuffler<string>>();
            var cutter = Substitute.For<ICutter<string>>();
            var cards = new Cards(1, shuffler, cutter);
            var words = GetWordsList();

            cards.GetCards(words);

            shuffler.Received().Shuffle(Arg.Any<string[]>());
        }

        [Test]
        public void GetCards_Always_UseCutter()
        {
            var shuffler = Substitute.For<IShuffler<string>>();
            var cutter = Substitute.For<ICutter<string>>();
            var cards = new Cards(1, shuffler, cutter);
            var words = GetWordsList();

            cards.GetCards(words);

            cutter.Received().CutMultipleOfBasis(Arg.Any<string[]>(), Arg.Any<int>());
        }

        [Test]
        public void GetCards_Always_ReturnsInstanceOfCardClass()
        {
            var words = GetWordsList(10);
            var provider = new Cards(10, new StubShuffler(), new StubCutter());

            var cards = provider.GetCards(words);

            CollectionAssert.AllItemsAreInstancesOfType(cards, typeof(Card));
        }

        [Test]
        public void GetCards_Given10WordsWith10Basis_Returns1CardOf10Words()
        {
            var words = GetWordsList(10);
            var provider = new Cards(10, new StubShuffler(), new StubCutter());

            var cards = provider.GetCards(words);

            Assert.AreEqual(1, cards.Length);
            Assert.AreEqual(10, cards.First().Words.Length);
            Assert.AreEqual(0, cards.First().Index);
        }

        [Test]
        public void GetCards_Given1WordWith10Basis_Returns1CardOf1Word()
        {
            var collectionWithOneWOrd = new string[] { "OneWord" };
            var provider = new Cards(10, new StubShuffler(), new StubCutter());

            var cards = provider.GetCards(collectionWithOneWOrd);

            Assert.AreEqual(1, cards.Length);
            Assert.AreEqual(1, cards.First().Words.Length);
            Assert.AreEqual(0, cards.First().Index);
        }

        [Test]
        public void GetCards_Given50WordsWith10Basis_Returns5CardOf10Words()
        {
            var words = GetWordsList(50);
            var provider = new Cards(10, new StubShuffler(), new StubCutter());

            var cards = provider.GetCards(words);

            Assert.AreEqual(5, cards.Length);
            Assert.IsTrue(cards.All(x => x.Words.Length == 10));

            var cardWithIndex = cards
                .Select((x, index) => new
                {
                    CollIndex = index, 
                    TestCard = x
                });
            Assert.IsTrue(cardWithIndex.All(x => x.CollIndex == x.TestCard.Index));
        }

        [Test]
        public void GetCards_Given15WordsWith10Basis_Returns1FullCardAnd1HalfCard()
        {
            var words = GetWordsList(15);
            var provider = new Cards(10, new StubShuffler(), new StubCutter());

            var cards = provider.GetCards(words);

            Assert.AreEqual(2, cards.Length);
            Assert.AreEqual(0, cards[0].Index);
            Assert.AreEqual(1, cards[1].Index);
            Assert.AreEqual(10, cards[0].Words.Length);
            Assert.AreEqual(5, cards[1].Words.Length);        
        }
    }
}
