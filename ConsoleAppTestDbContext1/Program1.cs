using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SwitchPortConfigurator.Api.Repository.Db;
using SwitchPortConfigurator.Api.Repository.Entities;

namespace ConsoleAppTestDbContext1
{
    internal class Program1
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<RepositoryDbContext> optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SwitchPortCofiguratorApi;Username=postgres;Password=1234");

            int Id = 14;

            WaitPressAnyKey("Press any key to create db context 1...");

            using (RepositoryDbContext dbContext1 = new RepositoryDbContext(optionsBuilder.Options))
            {
                WaitPressAnyKey("Press any key to start transaction 1...");

                IDbContextTransaction transaction1 = dbContext1.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot);
                //AreaEntity areaEntity = new AreaEntity { Id = 100, Name = "TestAdd02" };

                PrintAreaWithSeparator(dbContext1);

                //dbContext.Areas.Add(areaEntity);

                AreaEntity areaEntityDelete = dbContext1.Areas.Find(Id);

                //areaEntityDelete.Name = "Test -1";

                dbContext1.Areas.Remove(areaEntityDelete);

                //dbContext1.Areas.Update(areaEntityDelete);

                WaitPressAnyKey("Press any key to save changes 1...");

                dbContext1.SaveChanges();

                PrintAreaWithSeparator(dbContext1);

                WaitPressAnyKey("Press any key to commit 1...");

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