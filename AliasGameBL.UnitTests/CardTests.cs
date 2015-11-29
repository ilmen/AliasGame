using NUnit.Framework;
using AliasGameBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasGameBL.UnitTests
{
    [TestFixture]
    public class CardTests
    {
        string[] GetNotEmptyWordsArray()
        {
            return new string[] { "word1", "word2", "word3", "word4", "word5" };
        }

        [Test]
        public void Ctor_NullWordList_ThrowException()
        {
            var ex = Assert.Catch < ArgumentNullException > (() => new Card(0, null));

            StringAssert.Contains("Пустая коллекция слов", ex.Message);
        }

        [Test]
        public void Ctor_EmptyWordList_ThrownException()
        {
            var emptyWordArray = new string[] {};

            var ex = Assert.Catch<ArgumentException>(() => new Card(0, emptyWordArray));

            StringAssert.Contains("Коллекция слов для карточки не может быть пустой", ex.Message);
        }

        [Test]
        public void Ctor_NotEmptyWordList_CardInstanceForWordListSuccessfullyCreated()
        {
            var words = GetNotEmptyWordsArray();

            var card = new Card(0, words);

            CollectionAssert.AreEqual(words, card.Words);
        }

        [Test]
        public void Ctor_Always_SetCorrectProperties()
        {
            var index = 7;
            var words = new string[] { "word1", "word2", "word3" };
            var card = new Card(index, words);

            Assert.AreEqual(index, card.Index);
            CollectionAssert.AreEquivalent(words, card.Words);
        }
    }
}
