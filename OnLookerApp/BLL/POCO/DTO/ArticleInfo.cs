using System;
using System.Runtime.Serialization;

namespace OnLooker.Core
{
    [Serializable]
    [DataContract]
    public class ArticleInfo
    { 
        public Int32 ID { get; set; }
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
        public CountryInfo Country { get; set; }
        [DataMember]
        public CTag[] Tags { get; set; }
     }
}
