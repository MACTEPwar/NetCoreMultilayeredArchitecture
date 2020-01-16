using System;
using DAL.Models.Entitys;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class DbEntityContext : BaseDbContext, IDbContext
    {
        public DbEntityContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
