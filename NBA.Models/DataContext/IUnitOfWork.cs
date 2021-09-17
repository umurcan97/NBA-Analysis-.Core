namespace NBA.Models.DataContext
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
