// @mteinum

using System.Data.Entity;

namespace EfExt.Tests
{
    public class TestContext : DbContext
    {
        static TestContext()
        {
            Database.SetInitializer<TestContext>(null);            
        }

        public IDbSet<PhoneNumber> Numbers { get; set; }
        public IDbSet<NumberPlan> NumberPlans { get; set; } 
    }
}