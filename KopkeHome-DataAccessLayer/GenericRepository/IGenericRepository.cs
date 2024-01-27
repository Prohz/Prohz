using Dapper;
using System.Data;
namespace KopkeHome_DataAccessLayer.GenericRepository
{
    public interface IGenericRepository
    {
        IDbConnection Connection { get; }
        bool Add<T>(string query, T parameters);
        int AddMultipleResponse<T>(string query, DynamicParameters parameters);
        void AddMultiple<T>(string query, List<T> parameters);
        bool Delete(string query, object parameters);
        T Get<T>(string query, object parameter);
        IEnumerable<T> GetEntities<T>(string query, object parameters);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultipleResultSets<T1, T2>(string sQuery, object parameters);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultipleResultSets<T1, T2, T3>(string sQuery, object parameters);
    }

}
