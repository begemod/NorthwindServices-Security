namespace Tests.IISHostingServicesTests
{
    using System.ServiceModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.BaseTests;

    [TestClass]
    public class CategoriesServiceTests : BaseCategoriesServiceTests
    {
        private const string BasicHttpBindingICategoriesService = "BasicHttpBinding_ICategoriesService_IIS";
        private const string NetTcpBindingICategoriesService = "BasicHttpBinding_ICategoriesService_IIS"; //"NetTcpBinding_ICategoriesService_IIS";

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
    }
}
