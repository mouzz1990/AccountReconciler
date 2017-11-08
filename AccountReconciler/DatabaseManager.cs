using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountReconcilerLibrary
{
    public static class DatabaseManager
    {
        //Database Context
        public static DatabaseContext DatabaseContext;

        //Discard changes from database
        public static void DiscardChanges()
        {
            foreach (DbEntityEntry entry in DatabaseContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }

        //SaveChanges
        public static void SaveChanges()
        {
            DatabaseContext.SaveChanges();
        }
    }
}
