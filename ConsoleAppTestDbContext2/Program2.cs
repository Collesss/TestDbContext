using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SwitchPortConfigurator.Api.Repository.Db;
using SwitchPortConfigurator.Api.Repository.Entities;

namespace ConsoleAppTestDbContext2
{
    internal class Program2
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<RepositoryDbContext> optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SwitchPortCofiguratorApi;Username=postgres;Password=1234");

            //int Id = 42;

            WaitPressAnyKey("Press any key to create db context 2...");

            using (RepositoryDbContext dbContext1 = new RepositoryDbContext(optionsBuilder.Options))
            {
                WaitPressAnyKey("Press any key to start transaction 2...");

                IDbContextTransaction transaction1 = dbContext1.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot);
                //AreaEntity areaEntity = new AreaEntity { Id = 100, Name = "TestAdd02" };

                PrintAreaWithSeparator(dbContext1);

                //dbContext.Areas.Add(areaEntity);

                AreaEntity areaEntityAdd = new AreaEntity { Name = "New 001" };

                //areaEntityDelete.Name = "Test -1";

                dbContext1.Areas.Add(areaEntityAdd);

                //dbContext1.Areas.Update(areaEntityDelete);

                WaitPressAnyKey("Press any key to save changes 2...");

                dbContext1.SaveChanges();

                WaitPressAnyKey("Press any key to commit 2...");

                PrintAreaWithSeparator(dbContext1);

                transaction1.Commit();

                PrintAreaWithSeparator(dbContext1);
            }
        }

        static void PrintAreaWithSeparator(RepositoryDbContext dbContext)
        {
            PrintAreas(dbContext);
            PrintSeparator();
        }

        static void WaitPressAnyKey(string message = null)
        {
            Console.WriteLine(string.IsNullOrWhiteSpace(message) ? "Press any key to continue..." : message);
            Console.ReadKey(true);
        }

        static void PrintSeparator() =>
            Console.WriteLine(new string('-', Console.BufferWidth));

        static void PrintAreas(RepositoryDbContext dbContext) =>
            Console.WriteLine(string.Join(",\n", dbContext.Areas.ToList().Select(area => $"{{\n\tId: {area.Id},\n\tName: \"{area.Name}\"\n}}")));
    }
}