using Microsoft.EntityFrameworkCore;

namespace NBA.Models.DataContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly INBAContext _db;
        public UnitOfWork(INBAContext db)
        {
            _db = db;
        }
        public void Commit()
        {
            ((DbContext)this._db).SaveChanges();
        }
    }
}
