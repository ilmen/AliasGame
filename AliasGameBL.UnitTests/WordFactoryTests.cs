using AliasGameBL.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasGameBL.UnitTests
{
    [TestFixture]
    public class WordsTest
    {
        // TODO: Интеграционные тесты с файлами?

        [Test]
        public void GetAllWords_Always_ReturnsNotEmptyWordArray()
        {
            var factory = new WordFactory();

            var list = factory.GetAllWords();

            CollectionAssert.IsNotEmpty(list);
        }

        [Test]
        public void GetAllWords_Always_ReturnsStringArray()
        {
            var factory = new WordFactory();

            var list = factory.GetAllWords();

            Assert.IsInstanceOf<string[]>(list);
        }
    }
}
