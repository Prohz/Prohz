using System.Data;
namespace KopkeHome_DataAccessLayer.Repository
{
    public interface IConfigFactory
    {
        IDbConnection GetConnection { get; }
        int GetTimeOut { get; }

    }
}
