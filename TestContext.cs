// @mteinum
using System.Data.Entity;

namespace EfExt
{
    public class TestContext : DbContext
    {
        static TestContext()
        {
            Database.SetInitializer<TestContext>(null);            
        }

        public IDbSet<PhoneNumber> Numbers { get; set; }
    }
}