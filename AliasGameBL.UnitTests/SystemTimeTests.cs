using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasGameBL.UnitTests
{
    [TestFixture]
    public class SystemTimeTests
    {
        [Test]
        public void Set_Always_SetNewValueForNowProperty()
        {
            var trueTime = SystemTime.Now;
            var customTime = new DateTime(2001, 1, 1);
            
            SystemTime.Set(customTime);

            Assert.AreEqual(customTime, SystemTime.Now);
            Assert.AreNotEqual(customTime, trueTime);
        }

        [Test]
        public void Reset_Always_ResetNowPropertyBehaviorToStandart()
        {
            var customTime = new DateTime(2001, 1, 1);
            SystemTime.Set(customTime);

            Assert.AreEqual(customTime, SystemTime.Now);

            SystemTime.Reset();

            Assert.AreNotEqual(customTime, SystemTime.Now);
        }

        [TearDown]
        public void ResetSystemTime()
        {
            SystemTime.Reset();
        }
    }
}
