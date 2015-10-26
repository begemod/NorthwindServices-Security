namespace DAL.DataServices
{
    using System.Data;

    public class BaseDataService
    {
        private readonly IConnectionFactory connectionFactory;

        protected BaseDataService(IConnectionFactory connectionFactory)
        {
          this.connectionFactory = connectionFactory;
        }

        protected IDbConnection GetConnection()
        {
           var connection = this.connectionFactory.Create();
            if (connection != null && connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }
    }
}