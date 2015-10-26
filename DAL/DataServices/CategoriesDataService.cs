namespace DAL.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using DAL.Entities;
    using DAL.Infrastructure;
    using DAL.QueryObjects;

    using Dapper;

    public class CategoriesDataService : BaseDataService
    {
        public CategoriesDataService(IConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public Category GetByCategoryName(string categoryName)
        {
            using (var connection = this.GetConnection())
            {
                var categoryQueryObject = new CategoryQueryObject();
                var categories = connection.Query<Category>(categoryQueryObject.GetByCategoryName(categoryName)).ToList();

                if (!categories.Any())
                {
                    throw new EntityNotFoundException(string.Format("Category with {0} name is not found in database.", categoryName));
                }

                return categories.First();
            }
        }

        public IEnumerable<string> GetCategoryNames()
        {
            using (var connection = this.GetConnection())
            {
                var categoryQueryObject = new CategoryQueryObject();
                return connection.Query<string>(categoryQueryObject.GetCategoryNames());
            }
        }

        public void UpdateCategoryPicture(Category category)
        {
            if (category == null)
            {
                return;
            }

            using (var connection = this.GetConnection())
            {
                var categoryQueryObject = new CategoryQueryObject();
                connection.Execute(categoryQueryObject.UpdateCategoryImage(category.CategoryID, category.Picture));
            }
        }
    }
}