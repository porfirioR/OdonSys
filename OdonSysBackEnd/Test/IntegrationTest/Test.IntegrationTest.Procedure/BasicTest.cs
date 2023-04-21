using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcceptanceTest.Host.Api
{
    internal class BasicTest : TestBase
    {
        [Test]
        public void BaseReturnOk()
        {
            // Just for run test base and insert script sql
            Assert.IsTrue(true);
        }
    }
}
