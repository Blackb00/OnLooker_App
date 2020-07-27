using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RiaCrawlerWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRiaCrawlerService
    {
        [OperationContract]
        CArticle[] GetWorldNews(String tag);

        [OperationContract]
        CArticle[] GetLocalNews(String tag, String country);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "RiaCrawlerWCF.ContractType".

    [DataContract]
    public class CArticle
    {
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public String Content { get; set; }
        [DataMember]
        public byte[] Html { get; set; }
        [DataMember]
        public String Url { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public CCountry Country { get; set; }
        [DataMember]
        public CTag[] Tags { get; set; }
    }

    public class CCountry
    {
        public String Name { get; set; }
        public String Code { get; set; }
    }

    public class CTag
    {
        public String Original { get; set; }
        public String Value { get; set; }
    }
}
