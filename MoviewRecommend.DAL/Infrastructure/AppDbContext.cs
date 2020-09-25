using Microsoft.EntityFrameworkCore;
using MoviewRecommend.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviewRecommend.DAL.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Movie> Movie { get; set; }

        public virtual DbSet<Note> Note { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
