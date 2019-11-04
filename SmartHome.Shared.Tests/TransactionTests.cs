namespace SmartHome.Shared.Tests
{
    namespace SmartHome.Shared.Tests
    {
        using System.Linq;
        using System.Threading.Tasks;
        using System.Transactions;
        using Database;
        using Database.Entities;
        using Microsoft.EntityFrameworkCore;
        using Microsoft.Extensions.Configuration;
        using NUnit.Framework;
    
        [TestFixture]
        public abstract class TransactionTests : DatabaseTests
        {
            [SetUp]
            public void SetUp()
            {
                DbContext.Database.BeginTransaction();
            }

            [TearDown]
            public new void TearDown()
            {
                DbContext.Database.CurrentTransaction.Rollback();
            }

            public TType GetAny<TType>() where TType : Entity
            {
                return DbContext.Set<TType>().AsQueryable().First();
            }
        }
    }
}