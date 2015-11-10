using NUnit.Framework;
using SelfHostedRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedRestService.UnitTests
{
    [TestFixture]
    public class CardTests
    {
        string[] GetNotEmptyWordsArray()
        {
            return new string[] { "word1", "word2", "word3", "word4", "word5" };
        }

        [Test]
        public void Ctor_EmptyWordList_ThrownException()
        {
            var emptyWordArray = new string[] {};

            var ex = Assert.Catch<ArgumentOutOfRangeException>(() => new Card(0, emptyWordArray));

            StringAssert.Contains("Коллекция слов для карточки не может быть пустой", ex.Message);
        }

        [Test]
        public void Ctor_NotEmptyWordList_CardInstanceForWordListSuccessfullyCreated()
        {
            var words = GetNotEmptyWordsArray();

            var card = new Card(0, words);

            CollectionAssert.AreEqual(card.Words, words);
        }
    }
}
