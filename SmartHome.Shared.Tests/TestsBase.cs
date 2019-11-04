using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SmartHome.Shared.Tests
{
    [TestFixture]
    [Timeout(100)]
    public abstract class TestsBase
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [TearDown]
        public void TearDown()
        {
        }
    }
}