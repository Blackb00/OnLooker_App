
using System;
using System.Runtime.Serialization;

namespace OnLooker.Core
{
    [Serializable]
    [DataContract]
    public class CountryInfo
    {
      
        public Int32 ID { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String Code { get; set; }

        public String FullName
        {
            get => $"({Code}) {Name}";
        }
    }
}
