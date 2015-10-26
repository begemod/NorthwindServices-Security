namespace DAL.DataServices
{
    using System.Collections.Generic;
    using DAL.Entities;
    using DAL.QueryObjects;

    public class ShippersDataService : BaseDataService
    {
        public ShippersDataService(IConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<Shipper> GetAll()
        {
            using (var connection = this.GetConnection())
            {
                var shipperQueryObject = new ShipperQueryObject();
                return connection.Query<Shipper>(shipperQueryObject.GetAll());
            }
        }
    }
}