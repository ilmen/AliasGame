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
    public class StringShufflerTests
    {
        private string[] GetWordsArray()
        {
            return new string[] { "word1", "word2", "word3", "word4", "word5", "word6" };
        }

        private void CheckEquipotentOfCollections(IEnumerable<string> collection1, IEnumerable<string> collection2)
        {
            CollectionAssert.AreEquivalent(
                collection1.OrderBy(x => x),
                collection2.OrderBy(x => x),
                "Меняться должен только порядок элементов! Метод Shuffle должен возвращать равномощное множество (биекцию), т.е. не должен менять состав множества.");
        }

        [Test]
        public void Shuffle_Always_ReturnsShuffledCollection()
        {
            SystemTime.Set(DateTime.MinValue);
            var words = GetWordsArray();
            var shlr = new StringShuffler();

            var shuffled = shlr.Shuffle(words).ToArray();

            CheckEquipotentOfCollections(words, shuffled);
            
            // в теории, существует вероятность, что на какой-либо машине с данными time1 и time2 тест
            // вернет одинаковые коллекции, так как существует вероятность что Random зависит
            // не только от начального значения Seed, а еще и от окружения и может вернуть 
            // эквивалентные ряды псевдослучайных чисел при различных начальных значениях Seed
            CollectionAssert.AreNotEqual(words, shuffled);    
        }

        [Test]
        public void Shuffle_RunedAtDifferentTime_RetunsOtherShuffle()
        {
            var words = GetWordsArray();
            var shlr = new StringShuffler();
            var time1 = DateTime.MinValue;
            var time2 = DateTime.MinValue.AddTicks(1);
            
            SystemTime.Set(time1);
            var shuffled1 = shlr.Shuffle(words).ToArray();
            
            SystemTime.Set(time2);
            var shuffled2 = shlr.Shuffle(words).ToArray();

            CheckEquipotentOfCollections(shuffled1, shuffled2);

            // в теории, существует вероятность, что на какой-либо машине с данными time1 и time2 тест
            // вернет одинаковые коллекции, так как существует вероятность что Random зависит
            // не только от начального значения Seed, а еще и от окружения и может вернуть 
            // эквивалентные ряды псевдослучайных чисел при различных начальных значениях Seed
            CollectionAssert.AreNotEqual(shuffled1, shuffled2);
        }

        [Test]
        public void Shuffle_RunedAtTheSameTime_RetunsEqualsShuffle()
        {
            var words = GetWordsArray();
            var shlr = new StringShuffler();
            SystemTime.Set(DateTime.MinValue);

            var shuffled1 = shlr.Shuffle(words).ToArray();
            var shuffled2 = shlr.Shuffle(words).ToArray();

            CheckEquipotentOfCollections(shuffled1, shuffled2);

            // в теории, существует вероятность, что на какой-либо машине с данными time1 и time2 тест
            // вернет одинаковые коллекции, так как существует вероятность что Random зависит
            // не только от начального значения Seed, а еще и от окружения и может вернуть 
            // эквивалентные ряды псевдослучайных чисел при различных начальных значениях Seed
            CollectionAssert.AreEquivalent(shuffled1, shuffled2);
        }

        [Test]
        public void Shuffle_NullCollection_ThrownArgumentNullException()
        {
            var words = GetWordsArray();
            var shlr = new StringShuffler();
            SystemTime.Set(DateTime.MinValue);

            var ex = Assert.Catch<ArgumentNullException>(() => shlr.Shuffle(null));
            StringAssert.Contains("Ссылка", ex.Message);    
        }

        [TearDown]
        public void ResetSystemTime()
        {
            SystemTime.Reset();
        }
    }
}
