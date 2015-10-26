namespace WCFServices.DataContracts
{
    using System.IO;
    using System.ServiceModel;

    [MessageContract(WrapperNamespace = "http://epam.com/NorthwindService")]
    public class SendingCategory
    {
        [MessageHeader]
        public string CategoryName { get; set; }

        [MessageBodyMember]
        public Stream CategoryImage { get; set; }
    }
}