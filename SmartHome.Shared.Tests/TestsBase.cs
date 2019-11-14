using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SmartHome.Shared.Tests
{
    [TestFixture]
    public abstract class TestsBase
    {
        protected IMapper Mapper { get; private set; }
        

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // get all non-tests assemblies
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName!.StartsWith("SmartHome"));
                
                cfg.AddMaps(assemblies);
            });

            Mapper = config.CreateMapper();
        }
        
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