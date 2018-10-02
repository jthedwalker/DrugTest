using System.Data.Entity;

namespace DrugTest.Models
{
    public class DriverContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DriverContext() : base("name=DriverContext")
        {
            // Database.SetInitializer<DriverContext>(new CreateDatabaseIfModelChanges<DriverContext>());
            // Database.SetInitializer<DriverContext>(new CreateDatabaseIfNotExists<DriverContext>());                                                            
            // Database.SetInitializer<DriverContext>(new DropCreateDatabaseAlways<DriverContext>());

            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<TestBatch> TestBatches { get; set; }

        public DbSet<Error> Errors { get; set; }
    }
}
