using System;
using System.Collections.Generic;
using System.Text;
using QQmusic.Core.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace QQmusic.Infrastructure.Database
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new BoardItemViewConfiguration());
        }

        //public DbSet<BoardItemView> BoardItemViews { get; set; }
    }
}
