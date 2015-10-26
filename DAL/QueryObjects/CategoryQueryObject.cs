namespace DAL.QueryObjects
{
    public class CategoryQueryObject
    {
        public QueryObject GetByCategoryName(string categoryName)
        {
            const string Sql = "select [CategoryID], [CategoryName], [Description], [Picture] from dbo.Categories where CategoryName = @CategoryName";
            return new QueryObject(Sql, new { CategoryName = categoryName });
        }

        public QueryObject GetCategoryNames()
        {
            const string Sql = "select CategoryName from dbo.Categories";
            return new QueryObject(Sql);
        }

        public QueryObject UpdateCategoryImage(int categoryId, byte[] categoryPicture)
        {
            const string Sql = @"update dbo.Categories
                                set
                                    Picture = @Picture
                                where
                                    CategoryID = @CategoryId";
            return new QueryObject(Sql, new { CategoryId = categoryId, Picture = categoryPicture });
        }
    }
}