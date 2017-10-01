using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using ReminderApi.Model;
using System.Linq;

namespace ReminderApi
{

    public class ReminderContext : DbContext
    {
        public ReminderContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=ReminderApp;User=SA;Password='Qwerty123!!';Connect Timeout=60");
            }
        }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.Now;
                }
                ((BaseEntity)entity.Entity).DateModified = DateTime.Now;
            }
            return base.SaveChanges();
        }
    }
}