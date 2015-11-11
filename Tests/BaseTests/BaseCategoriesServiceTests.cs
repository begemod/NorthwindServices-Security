namespace Tests.BaseTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WCFServices.Cotracts;
    using WCFServices.DataContracts;

    internal interface ICategoriesServiceChannel : IClientChannel, ICategoriesService
    {
    }

    public class BaseCategoriesServiceTests
    {
        protected void GetCategoryNamesTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName))
            {
                var client = channel.CreateChannel();

                var names = client.GetCategoryNames();

                Assert.IsTrue(names != null && names.Any());
            }
        }

        protected void GetCategoryImageFaultTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName))
            {
                var invalidCategoryName = Guid.NewGuid().ToString();

                var client = channel.CreateChannel();

                var stream = client.GetCategoryImage(invalidCategoryName);
            }
        }

        protected void GetCategoryImageTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName))
            {
                var client = channel.CreateChannel();

                var names = client.GetCategoryNames();

                var categoryName = names.First();

                var readerStream = client.GetCategoryImage(categoryName);

                var memoryStream = this.ReadDataToMemoryStream(readerStream);

                Assert.IsTrue(memoryStream.Length > 0);
            }
        }

        protected void SaveCategoryImageNullCategoryNameFaultTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName))
            {
                var client = channel.CreateChannel();

                var sendingCategory = new SendingCategory
                                           {
                                               CategoryName = string.Empty,
                                               CategoryImage = new MemoryStream()
                                           };

                client.SaveCategoryImage(sendingCategory);
            }
        }

        protected void SaveCategoryImageWithWrongCategoryNameFaultTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName))
            {
                var client = channel.CreateChannel();

                var sendingCategory = new SendingCategory
                {
                    CategoryName = Guid.NewGuid().ToString(),
                    CategoryImage = new MemoryStream()
                };

                client.SaveCategoryImage(sendingCategory);
            }
        }

        protected void SaveCategoryImageTest(string endpointConfigurationName)
        {
            using (var channel = new ChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName))
            {
                var client = channel.CreateChannel();

                var names = client.GetCategoryNames();
                var categoryName = names.First();

                var readerStream = client.GetCategoryImage(categoryName);

                var memoryStream = this.ReadDataToMemoryStream(readerStream);
                memoryStream.Position = 0;

                var sendingCategory = new SendingCategory
                {
                    CategoryName = categoryName,
                    CategoryImage = memoryStream
                };

                client.SaveCategoryImage(sendingCategory);
            }
        }

        private MemoryStream ReadDataToMemoryStream(Stream inputStream)
        {
            const int BuffSize = 1000;

            var buffer = new byte[BuffSize];

            var memoryStream = new MemoryStream();

            var readed = inputStream.Read(buffer, 0, BuffSize);

            while (readed != 0)
            {
                memoryStream.Write(buffer, 0, readed);
                readed = inputStream.Read(buffer, 0, BuffSize);
            }

            return memoryStream;
        }
    }
}