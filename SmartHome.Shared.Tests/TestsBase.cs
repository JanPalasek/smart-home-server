using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SmartHome.Common;
using SmartHome.Common.DateTimeProviders;
using SmartHome.Infrastructure;

namespace SmartHome.Shared.Tests
{
    [TestFixture]
    public abstract class TestsBase
    {
        protected IMapper Mapper { get; private set; }
        protected IDateTimeProvider DateTimeProvider { get; set; }
        

        [OneTimeSetUp]
        public void SetUpOnce()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // get all non-tests assemblies
                var infrastructureAssembly = Assembly.Load("SmartHome.Infrastructure");
                var webAssembly = Assembly.Load("SmartHome.Web");
                
                cfg.AddMaps(infrastructureAssembly, webAssembly);
            });

            Mapper = config.CreateMapper();
        }
        
        [SetUp]
        public void Setup()
        {
            DateTimeProvider = new StaticDateTimeProvider(new DateTime(2019, 6, 15));
        }
        
        [TearDown]
        public void TearDown()
        {
        }
    }
}