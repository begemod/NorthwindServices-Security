namespace Tests.SelfHostingServiceTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;

    [TestClass]
    public class CategoriesServiceTests : BaseCategoriesServiceTests
    {
        private const string BasicHttpBindingICategoriesService = "BasicHttpBinding_ICategoriesService_SH";
        private const string NetTcpBindingICategoriesService = "NetTcpBinding_ICategoriesService_SH";
        private const string HttpMexEndpointAddress = "http://epruizhw0228:8733/Design_Time_Addresses/NorthwindWCFServices/CategoriesService/mex";
        private const string MetadataAddress = "http://epruizhw0228:8733/Design_Time_Addresses/NorthwindWCFServices/CategoriesService/?wsdl";
        private const string TcpMexEndpointAddress = "net.tcp://epruizhw0228:810/NorthwindWCFServices/CategoriesService/mex";


        [TestMethod]
        public void GetCategoryNamesTest()
        {
            this.GetCategoryNamesTest(BasicHttpBindingICategoriesService);
            this.GetCategoryNamesTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        public void GetCategoryImageFaultTest()
        {
            this.GetCategoryImageFaultTest(BasicHttpBindingICategoriesService);
            this.GetCategoryImageFaultTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        public void GetCategoryImageTest()
        {
            this.GetCategoryImageTest(BasicHttpBindingICategoriesService);
            this.GetCategoryImageTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        public void SaveCategoryImageNullCategoryNameFaultTest()
        {
            this.SaveCategoryImageNullCategoryNameFaultTest(BasicHttpBindingICategoriesService);
            this.SaveCategoryImageNullCategoryNameFaultTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        public void SaveCategoryImageWrongCategoryNameFaultTest()
        {
            this.SaveCategoryImageWithWrongCategoryNameFaultTest(BasicHttpBindingICategoriesService);
            this.SaveCategoryImageWithWrongCategoryNameFaultTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        public void SaveCategoryImageTest()
        {
            this.SaveCategoryImageTest(BasicHttpBindingICategoriesService);
            this.SaveCategoryImageTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        public void GetMetadataTest()
        {
            this.BaseGetMetadataOverMetadataExchangeTest(HttpMexEndpointAddress);
            this.BaseGetMetadataOverHttpGetTest(MetadataAddress);
            this.BaseGetMetadataOverMetadataExchangeTest(TcpMexEndpointAddress);
        }
    }
}