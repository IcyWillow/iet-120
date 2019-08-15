using iet_120.Model;

namespace iet_120.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, Email = "vinilodeon@hotmail.com", Salutation = "Herr", Firstname = "Vinicius",
                            Lastname = "Pontes", Password = "test123456", CreateAt = DateTime.Now}
            );

            context.Words.AddOrUpdate(x => x.Id,
                new Word() { Id = 1, Name = "zeit", CreatedAt = DateTime.Now, UserId = 1},
                new Word() { Id = 1, Name = "kaefer", CreatedAt = DateTime.Now, UserId = 1 },
                new Word() { Id = 1, Name = "baum", CreatedAt = DateTime.Now, UserId = 1 },
                new Word() { Id = 1, Name = "honig", CreatedAt = DateTime.Now, UserId = 1 },
                new Word() { Id = 1, Name = "blatt", CreatedAt = DateTime.Now, UserId = 1 },
                new Word() { Id = 1, Name = "meteorit", CreatedAt = DateTime.Now, UserId = 1 },
                new Word() { Id = 1, Name = "salamander", CreatedAt = DateTime.Now, UserId = 1 },
                new Word() { Id = 1, Name = "stein", CreatedAt = DateTime.Now, UserId = 1 },
                new Word() { Id = 1, Name = "insekt", CreatedAt = DateTime.Now, UserId = 1 }
            );
        }
    }
}
