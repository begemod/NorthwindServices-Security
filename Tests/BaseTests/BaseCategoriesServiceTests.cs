namespace Tests.BaseTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;

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
            var client = TestsEnviroment.GetChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName).CreateChannel();

            var names = client.GetCategoryNames();

            Assert.IsTrue(names != null && names.Any());
        }

        protected void GetCategoryImageFaultTest(string endpointConfigurationName)
        {
            var invalidCategoryName = Guid.NewGuid().ToString();

            var client = TestsEnviroment.GetChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName).CreateChannel();

            try
            {
                client.GetCategoryImage(invalidCategoryName);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void GetCategoryImageTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName).CreateChannel();

            var names = client.GetCategoryNames();

            var categoryName = names.First();

            var readerStream = client.GetCategoryImage(categoryName);

            var memoryStream = this.ReadDataToMemoryStream(readerStream);

            Assert.IsTrue(memoryStream.Length > 0);
        }

        protected void SaveCategoryImageNullCategoryNameFaultTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName).CreateChannel();

            var sendingCategory = new SendingCategory
                                       {
                                           CategoryName = string.Empty,
                                           CategoryImage = new MemoryStream()
                                       };

            try
            {
                client.SaveCategoryImage(sendingCategory);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void SaveCategoryImageWithWrongCategoryNameFaultTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName).CreateChannel();

            var sendingCategory = new SendingCategory
            {
                CategoryName = Guid.NewGuid().ToString(),
                CategoryImage = new MemoryStream()
            };

            try
            {
                client.SaveCategoryImage(sendingCategory);

                Assert.Fail();
            }
            catch (FaultException)
            {
            }
        }

        protected void SaveCategoryImageTest(string endpointConfigurationName)
        {
            var client = TestsEnviroment.GetChannelFactory<ICategoriesServiceChannel>(endpointConfigurationName).CreateChannel();

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

        protected void BaseGetMetadataOverHttpGetTest(string address)
        {
            this.BaseGetMetadataTest(address, MetadataExchangeClientMode.HttpGet);
        }

        protected void BaseGetMetadataOverMetadataExchangeTest(string address)
        {
            this.BaseGetMetadataTest(address, MetadataExchangeClientMode.MetadataExchange);
        }

        private void BaseGetMetadataTest(string endpointAddress, MetadataExchangeClientMode metadataExchangeClientMode)
        {
            var client = new MetadataExchangeClient(new Uri(endpointAddress), metadataExchangeClientMode);

            var metadata = client.GetMetadata();

            var wsdlImporter = new WsdlImporter(metadata);

            var contracts = wsdlImporter.ImportAllContracts();

            Assert.IsTrue(contracts.Any());
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