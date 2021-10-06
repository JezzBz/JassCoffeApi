using JassCoffeApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JassCoffeApi
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            
            builder.SetBasePath(Directory.GetCurrentDirectory());
            
            builder.AddJsonFile("appsettings.json");
            
            var config = builder.Build();
            
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
       
    }
}
