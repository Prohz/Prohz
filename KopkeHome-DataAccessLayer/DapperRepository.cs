using Dapper;
using System.Data;

namespace KopkeHome_DataAccessLayer
{
    public class DapperRepository<T> where T : class
    {
        private readonly DapperContext _context;

        public DapperRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetSPList(string Procedure, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())
            {

                return await connection.QueryAsync<T>
                    (Procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<int> InsertSp(string Procedure, DynamicParameters parameters)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync
                    (Procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
