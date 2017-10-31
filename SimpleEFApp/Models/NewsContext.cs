using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SimpleEFApp.Models
{
    public class NewsContext : DbContext
    {
        public NewsContext() : base("name=MyDBRemote")
        {

        }
      
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        //    modelBuilder.Entity<Article>().HasOptional<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey<int?>(a => a.CategoryId);
        //}
    }
}