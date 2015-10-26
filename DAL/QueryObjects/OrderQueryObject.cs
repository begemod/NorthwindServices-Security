namespace DAL.QueryObjects
{
    using DAL.Entities;

    public class OrderQueryObject
    {
        public QueryObject GetAll()
        {
            const string Sql =
                "select OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry from dbo.Orders";
            return new QueryObject(Sql);
        }

        public QueryObject GetById(int orderId)
        {
            const string Sql =
                "select OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry from dbo.Orders where OrderID = @OrderID";
            return new QueryObject(Sql, new { OrderID = orderId });
        }

        public QueryObject DeleteOrder(int orderId)
        {
            const string Sql = @"delete dbo.[Order Details] where OrderID = @OrderID;
                                 delete dbo.Orders where OrderID = @OrderID;
                                 select @@ROWCOUNT";

            return new QueryObject(Sql, new { OrderID = orderId });
        }

        public QueryObject InsertOrder(Order order)
        {
            const string Sql = @"insert into [dbo].[Orders]
                                       ([CustomerID]
                                       ,[EmployeeID]
                                       ,[OrderDate]
                                       ,[RequiredDate]
                                       ,[ShippedDate]
                                       ,[ShipVia]
                                       ,[Freight]
                                       ,[ShipName]
                                       ,[ShipAddress]
                                       ,[ShipCity]
                                       ,[ShipRegion]
                                       ,[ShipPostalCode]
                                       ,[ShipCountry])
                                 values
                                      (@CustomerID,
                                       @EmployeeID,
                                       @OrderDate,
                                       @RequiredDate,
                                       @ShippedDate,
                                       @ShipVia,
                                       @Freight,
                                       @ShipName,
                                       @ShipAddress,
                                       @ShipCity,
                                       @ShipRegion,
                                       @ShipPostalCode,
                                       @ShipCountry);
                                select scope_identity()";

            var parameters = new
            {
                order.CustomerID,
                order.EmployeeID,
                order.OrderDate,
                order.RequiredDate,
                order.ShippedDate,
                order.ShipVia,
                order.Freight,
                order.ShipName,
                order.ShipAddress,
                order.ShipCity,
                order.ShipRegion,
                order.ShipPostalCode,
                order.ShipCountry
            };

            return new QueryObject(Sql, parameters);
        }

        public QueryObject UpdateOrder(Order order)
        {
            const string Sql = @"update [dbo].[Orders]
                                   set [CustomerID] = @CustomerID
                                      ,[EmployeeID] = @EmployeeID
                                      ,[OrderDate] = @OrderDate
                                      ,[RequiredDate] = @RequiredDate
                                      ,[ShippedDate] = @ShippedDate
                                      ,[ShipVia] = @ShipVia
                                      ,[Freight] = @Freight
                                      ,[ShipName] = @ShipName
                                      ,[ShipAddress] = @ShipAddress
                                      ,[ShipCity] = @ShipCity
                                      ,[ShipRegion] = @ShipRegion
                                      ,[ShipPostalCode] = @ShipPostalCode
                                      ,[ShipCountry] = @ShipCountry
                                 where
                                    OrderID = @OrderID

                                select @@ROWCOUNT";

            var parameters = new
            {
                order.CustomerID,
                order.EmployeeID,
                order.OrderDate,
                order.RequiredDate,
                order.ShippedDate,
                order.ShipVia,
                order.Freight,
                order.ShipName,
                order.ShipAddress,
                order.ShipCity,
                order.ShipRegion,
                order.ShipPostalCode,
                order.ShipCountry,
                order.OrderID
            };

            return new QueryObject(Sql, parameters);
        }
    }
}