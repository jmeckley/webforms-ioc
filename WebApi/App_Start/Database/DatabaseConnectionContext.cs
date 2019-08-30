using System.Data;

namespace WebApi
{
    /*
     * This is just a help class for working directly with IDbConnection and Dapper.
     * Normally you would not need this class because the data access framework will track this for you
     */
    public class DatabaseConnectionContext
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; internal set; }

        public DatabaseConnectionContext(IDbConnection connection)
        {
            Connection = connection;
        }
    }
}