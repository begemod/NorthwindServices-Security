namespace DAL.QueryObjects
{
    public class ShipperQueryObject
    {
        public QueryObject GetAll()
        {
            const string Sql = @"select [ShipperID], [CompanyName], [Phone] from [dbo].[Shippers]";
            return new QueryObject(Sql);
        }
    }
}