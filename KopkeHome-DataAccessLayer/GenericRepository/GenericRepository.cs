using Dapper;
using KopkeHome_DataAccessLayer.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Dapper.SqlMapper;


namespace KopkeHome_DataAccessLayer.GenericRepository
{
    public class GenericRepository : IGenericRepository, IDisposable
    {
        public IDbConnection Connection { get; set; }
        private readonly string connectionString;

        int timeOut = 0;
        public GenericRepository(IConfigFactory configFactory, IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("KopkeHome_WebContext");
            Connection = configFactory.GetConnection;
            timeOut = configFactory.GetTimeOut;
        }

        public bool Add<T>(string query, T parameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                return Connection.Execute(query, parameters, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public int AddMultipleResponse<T>(string query, DynamicParameters parameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                return Connection.QueryFirstOrDefault<int>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void AddMultiple<T>(string query, List<T> parameters)
        {
            using (IDbConnection dbConnection = Connection)
            {
                foreach (T parameter in parameters)
                {
                    dbConnection.Execute(query, parameter, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public bool Delete(string query, object parameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.Execute(query, parameters, commandType: CommandType.StoredProcedure) > 0;
            }
        }

        public IEnumerable<T> GetEntities<T>(string query, object parameters)
        {
            IEnumerable<T> collection;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                collection = con.Query<T>(query, parameters, commandType: CommandType.StoredProcedure);
                return collection;
            }
        }

        public T Get<T>(string query, object parameters)
        {
            T collection;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                collection = Connection.QuerySingleOrDefault<T>(query, parameters, commandType: CommandType.StoredProcedure);
                return collection;
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> GetMultipleResultSets<T1, T2>(string sQuery, object parameters)
        {
            GridReader report;
            Tuple<IEnumerable<T1>, IEnumerable<T2>> dataToRtn;

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                report = dbConnection.QueryMultiple(sQuery, parameters, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);

                dataToRtn = new Tuple<IEnumerable<T1>, IEnumerable<T2>>(report.Read<T1>(), report.Read<T2>());

            }
            return dataToRtn;
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> GetMultipleResultSets<T1, T2, T3>(string sQuery, object parameters)
        {
            GridReader report;
            Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> dataToRtn;

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                report = dbConnection.QueryMultiple(sQuery, parameters, commandTimeout: timeOut, commandType: CommandType.StoredProcedure);
                dataToRtn = new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(report.Read<T1>(), report.Read<T2>(), report.Read<T3>());
            }
            return dataToRtn;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }
        }



    }
}
