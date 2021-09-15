using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
  public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Help> Help { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<SubscribeRequest> SubscribeRequests { get; set; }
    }
}
