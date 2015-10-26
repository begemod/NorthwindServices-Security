namespace WCFServices.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract(Namespace = "http://epam.com/NorthwindServices")]
    public class OrderDetailDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public short Quantity { get; set; }

        [DataMember]
        public float Discount { get; set; }

        [DataMember]
        public OrderDTO Order { get; set; }

        [DataMember]
        public ProductDTO Product { get; set; }
    }
}