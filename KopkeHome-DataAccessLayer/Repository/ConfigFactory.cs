using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace KopkeHome_DataAccessLayer.Repository
{
    public class ConfigFactory : IConfigFactory
    {
        private readonly string connectionString;
        private readonly int connectionTimeOut;
        public ConfigFactory(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("KopkeHome_WebContext");
            int TimeOut;

            this.connectionTimeOut = int.TryParse(configuration.GetSection("ConnectionStrings:ConnectionTimeOut").Value, out TimeOut)
            ? TimeOut : 20000;
        }
        public IDbConnection GetConnection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
        public int GetTimeOut
        {
            get
            {
                return this.connectionTimeOut;
            }
        }

    }
}
