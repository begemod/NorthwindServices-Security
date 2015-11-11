namespace Tests.SelfHostingServiceTests
{
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;

    [TestClass]
    public class CategoriesServiceTests : BaseCategoriesServiceTests
    {
        private const string BasicHttpBindingICategoriesService = "BasicHttpBinding_ICategoriesService_SH";
        private const string NetTcpBindingICategoriesService = "NetTcpBinding_ICategoriesService_SH";

        [TestMethod]
        public void GetCategoryNamesTest()
        {
            this.GetCategoryNamesTest(BasicHttpBindingICategoriesService);
            this.GetCategoryNamesTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
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
        [ExpectedException(typeof(FaultException))]
        public void SaveCategoryImageNullCategoryNameFaultTest()
        {
            this.SaveCategoryImageNullCategoryNameFaultTest(BasicHttpBindingICategoriesService);
            this.SaveCategoryImageNullCategoryNameFaultTest(NetTcpBindingICategoriesService);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
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
    }
}