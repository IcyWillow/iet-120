using System;
using System.Data.Entity;
using M120Projekt.Model;

namespace M120Projekt.Data
{
    public class Context : DbContext
    {
        public Context() : base("name=M120ConnectionstringRemote")
        {
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Data.Context, Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            modelBuilder.Entity<User>().ToTable("User"); // Damit kein "s" angehängt wird an Tabelle
            modelBuilder.Entity<Word>().ToTable("Word");
            modelBuilder.Entity<Highscore>().ToTable("Highscore");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Highscore> Highscores { get; set; }
    }
}
