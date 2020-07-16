namespace StoredProcedure.Migrations
{
    using StoredProcedure.Controllers;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoredProcedure.Controllers.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StoredProcedure.Controllers.DatabaseContext";
        }

        protected override void Seed(StoredProcedure.Controllers.DatabaseContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            for (int i = 0; i < 10; i++)
            {
                context.Books.AddOrUpdate(new Book()
                {
                    ID=i+1,
                    name = FakeData.NameData.GetCompanyName(),
                    Description = FakeData.TextData.GetSentence(),
                    Description2 = "test"+(i+1),
                    Description3 = -1,
                    Description4 = "seedTest",
                    PublishedDate = FakeData.DateTimeData.GetDatetime()
                });
                     
            }

            context.SaveChanges();
            for (int i = 0; i < 10; i++)
            {
                context.Authors.AddOrUpdate(new Author()
                {
                    ID = i + 1,
                    name = FakeData.NameData.GetFemaleFirstName(),
                    surname = FakeData.NameData.GetSurname(),
                    BirthDate = FakeData.DateTimeData.GetDatetime(),
                    Salary =FakeData.NumberData.GetNumber(1000,5000)
                });

            }
            context.SaveChanges();
        }
    }
}
