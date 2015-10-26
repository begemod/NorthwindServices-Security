namespace WCFServices.CategoriesService
{
    using System.Collections.Generic;
    using System.IO;
    using System.ServiceModel;
    using DAL.DataServices;
    using DAL.Infrastructure;
    using WCFServices.DataContracts;

    public class CategoriesService : ICategoriesService
    {
        private readonly CategoriesDataService categoriesDataService;

        public CategoriesService()
        {
            var connectionFactory = new NortwindDbConnectionFactory();
            this.categoriesDataService = new CategoriesDataService(connectionFactory);
        }

        public IEnumerable<string> GetCategoryNames()
        {
            return this.categoriesDataService.GetCategoryNames();
        }

        public Stream GetCategoryImage(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return null;
            }

            try
            {
                var category = this.categoriesDataService.GetByCategoryName(categoryName);

                var categoryImage = category.Picture;

                var imageStream = new MemoryStream(categoryImage, 78, categoryImage.Length - 78);

                return imageStream;
            }
            catch (EntityNotFoundException exception)
            {
                throw new FaultException(new FaultReason(exception.Message), new FaultCode("Error"));
            }
        }

        public void SaveCategoryImage(SendingCategory sendingCategory)
        {
            const int BuffSize = 1000;

            if (sendingCategory == null)
            {
                return;
            }

            this.Validate(sendingCategory);

            var buffer = new byte[BuffSize];
            var memoryStream = new MemoryStream();

            var readed = sendingCategory.CategoryImage.Read(buffer, 0, BuffSize);

            while (readed != 0)
            {
                memoryStream.Write(buffer, 0, readed);
                readed = sendingCategory.CategoryImage.Read(buffer, 0, BuffSize);
            }

            var sourceCategory = this.categoriesDataService.GetByCategoryName(sendingCategory.CategoryName);
            sourceCategory.Picture = memoryStream.ToArray();

            this.categoriesDataService.UpdateCategoryPicture(sourceCategory);
        }

        private void Validate(SendingCategory category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new FaultException(new FaultReason("Category name is not defined."), new FaultCode("Error"));
            }

            try
            {
                 this.categoriesDataService.GetByCategoryName(category.CategoryName);
            }
            catch (EntityNotFoundException exception)
            {
                throw new FaultException(new FaultReason(exception.Message), new FaultCode("Error"));
            }
        }
    }
}
