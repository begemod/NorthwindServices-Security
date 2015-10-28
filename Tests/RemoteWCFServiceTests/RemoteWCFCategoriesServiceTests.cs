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
        public void HttpBinding_GetCategoryNamesTest()
        {
            this.GetCategoryNamesTest(BasicHttpBindingIOrdersService);
        }

        [TestMethod]
        public void NetTcpBinding_GetCategoryNamesTest()
        {
            this.GetCategoryNamesTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void HttpBinding_GetCategoryImageFaultTest()
        {
            this.GetCategoryImageFaultTest(BasicHttpBindingIOrdersService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void NetTcpBinding_GetCategoryImageFaultTest()
        {
            this.GetCategoryImageFaultTest(NetTcpBindingIOrdersService);
        }

        [TestMethod]
        public void HttpBinding_GetCategoryImageTest()
        {
            this.GetCategoryImageTest(BasicHttpBindingIOrdersService);
        }

        [TestMethod]
        public void NetTcpBinding_GetCategoryImageTest()
        {
            this.GetCategoryImageTest(NetTcpBindingIOrdersService);
        }


        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void SaveCategoryImageNullCategoryNameFaultTest()
        {
            using (var client = new CategoriesServiceClient())
            {
                client.SaveCategoryImage(string.Empty, new MemoryStream());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void SaveCategoryImageWrongCategoryNameFaultTest()
        {
            using (var client = new CategoriesServiceClient())
            {
                var wrongCategoryName = Guid.NewGuid().ToString();
                var stream = new MemoryStream();

                client.SaveCategoryImage(wrongCategoryName, stream);
            }
        }

        [TestMethod]
        public void SaveCategoryImageTest()
        {
            using (var client = new CategoriesServiceClient())
            {
                var names = client.GetCategoryNames();
                var categoryName = names.First();

                var readerStream = client.GetCategoryImage(categoryName);

                var memoryStream = this.ReadDataToMemoryStream(readerStream);
                memoryStream.Position = 0;

                client.SaveCategoryImage(categoryName, memoryStream);
            }
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
