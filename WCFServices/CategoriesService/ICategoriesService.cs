namespace WCFServices.CategoriesService
{
    using System.Collections.Generic;
    using System.IO;
    using System.ServiceModel;
    using WCFServices.DataContracts;

    [ServiceContract(Namespace = "http://epam.com/NorthwindService")]
    public interface ICategoriesService
    {
        [OperationContract]
        IEnumerable<string> GetCategoryNames();

        [OperationContract]
        Stream GetCategoryImage(string categoryName);

        [OperationContract]
        void SaveCategoryImage(SendingCategory sendingCategory);
    }
}