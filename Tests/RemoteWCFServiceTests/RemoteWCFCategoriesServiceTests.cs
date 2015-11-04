namespace Tests.RemoteWCFServiceTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.CategoriesService;

    [TestClass]
    public class RemoteWCFCategoriesServiceTests
    {
        private const string BasicHttpBindingIOrdersService = "BasicHttpBinding_ICategoriesService";
        private const string NetTcpBindingIOrdersService = "NetTcpBinding_ICategoriesService";

        [TestMethod]
        public void GetCategoryNamesTest()
        {
            this.GetCategoryNamesTest(BasicHttpBindingIOrdersService);
            this.GetCategoryNamesTest(NetTcpBindingIOrdersService);
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void GetCategoryImageFaultTest()
        {
            this.GetCategoryImageFaultTest(BasicHttpBindingIOrdersService);
            this.GetCategoryImageFaultTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void GetCategoryImageTest()
        {
            this.GetCategoryImageTest(BasicHttpBindingIOrdersService);
            this.GetCategoryImageTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void SaveCategoryImageNullCategoryNameFaultTest()
        {
           this.SaveCategoryImageNullCategoryNameFaultTest(BasicHttpBindingIOrdersService);
           this.SaveCategoryImageNullCategoryNameFaultTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void SaveCategoryImageWrongCategoryNameFaultTest()
        {
            this.SaveCategoryImageWithWrongCategoryNameFaultTest(BasicHttpBindingIOrdersService);
            this.SaveCategoryImageWithWrongCategoryNameFaultTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void SaveCategoryImageTest()
        {
            this.SaveCategoryImageTest(BasicHttpBindingIOrdersService);
            this.SaveCategoryImageTest(NetTcpBindingIOrdersService);
        }

        private void GetCategoryNamesTest(string endpointConfigurationName)
        {
            using (var client = new CategoriesServiceClient(endpointConfigurationName))
            {
                var names = client.GetCategoryNames();

                Assert.IsTrue(names != null && names.Any());
            }
        }

        private void GetCategoryImageFaultTest(string endpointConfigurationName)
        {
            using (var client = new CategoriesServiceClient(endpointConfigurationName))
            {
                var invalidCategoryName = Guid.NewGuid().ToString();
                var stream = client.GetCategoryImage(invalidCategoryName);
            }
        }

        private void GetCategoryImageTest(string endpointConfigurationName)
        {
            using (var client = new CategoriesServiceClient(endpointConfigurationName))
            {
                var names = client.GetCategoryNames();

                var categoryName = names.First();

                var readerStream = client.GetCategoryImage(categoryName);

                var memoryStream = this.ReadDataToMemoryStream(readerStream);

                Assert.IsTrue(memoryStream.Length > 0);
            }
        }

        private void SaveCategoryImageNullCategoryNameFaultTest(string endpointConfigurationName)
        {
            using (var client = new CategoriesServiceClient(endpointConfigurationName))
            {
                client.SaveCategoryImage(string.Empty, new MemoryStream());
            }
        }

        private void SaveCategoryImageWithWrongCategoryNameFaultTest(string endpointConfigurationName)
        {
            using (var client = new CategoriesServiceClient(endpointConfigurationName))
            {
                var wrongCategoryName = Guid.NewGuid().ToString();
                var stream = new MemoryStream();

                client.SaveCategoryImage(wrongCategoryName, stream);
            }
        }

        private void SaveCategoryImageTest(string endpointConfigurationName)
        {
            using (var client = new CategoriesServiceClient(endpointConfigurationName))
            {
                var names = client.GetCategoryNames();
                var categoryName = names.First();

                var readerStream = client.GetCategoryImage(categoryName);

                var memoryStream = this.ReadDataToMemoryStream(readerStream);
                memoryStream.Position = 0;

                client.SaveCategoryImage(categoryName, memoryStream);
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
