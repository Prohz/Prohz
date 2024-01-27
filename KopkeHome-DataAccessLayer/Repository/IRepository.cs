using System.Linq.Expressions;
namespace KopkeHome_DataAccessLayer.Repository
{
    public interface IRepository<T>
    {
        Task<List<T>> GetMultipleRecord();
        Task<T> GetSingle(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<List<T>> FindAllByCondition(Expression<Func<T, bool>> match);

        Task<T> FindSingleByCondition(Expression<Func<T, bool>> match);
    }


}
