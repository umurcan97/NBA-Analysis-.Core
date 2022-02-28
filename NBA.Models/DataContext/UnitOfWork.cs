namespace NBA.Models.DataContext
{
    using Microsoft.EntityFrameworkCore;
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
