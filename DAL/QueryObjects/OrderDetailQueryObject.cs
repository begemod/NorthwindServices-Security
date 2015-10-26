namespace DAL.QueryObjects
{
    public class OrderDetailQueryObject
    {
        public QueryObject GetByOrderId(int orderId)
        {
            const string Sql = @"select
                                    od.OrderID
                                   ,od.ProductID
                                   ,od.UnitPrice
                                   ,od.Quantity
                                   ,od.Discount
                                   ,p.ProductID
                                   ,p.CategoryID
                                   ,p.Discontinued
                                   ,p.ProductName
                                   ,p.QuantityPerUnit
                                   ,p.ReorderLevel
                                   ,p.SupplierID
                                   ,p.UnitPrice
                                   ,p.UnitsInStock
                                   ,p.UnitsOnOrder
                                from 
                                    dbo.[Order Details] od
                                    join dbo.Products p on p.ProductID = od.ProductID
                                where od.OrderID = @OrderID";

            return new QueryObject(Sql, new { OrderID = orderId });
        }
    }
}