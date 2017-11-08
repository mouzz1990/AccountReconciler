using AccountReconcilerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DbConnection") { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Group1> Groups1 { get; set; }
        public DbSet<Group2> Groups2 { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<DetailsControlReturned> DetailsControlReturnedResults { get; set; }
        public DbSet<RuleAccount> RuleAccounts { get; set; }
    }
}
