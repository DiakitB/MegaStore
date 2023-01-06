namespace CandySoap.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICovertypesRepository Covers { get; }
        IProductRepository Product { get; }
        void Save();
    }
}
