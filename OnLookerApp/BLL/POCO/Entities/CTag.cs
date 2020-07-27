
using System;
using System.Runtime.Serialization;

namespace OnLooker.Core
{
    [Serializable]
    [DataContract]
    public class CTag
    {
        public Int32 ID;
        [DataMember]
        public String Value { get; set; }
        [DataMember]
        public String Original { get;  set; }

        public CTag()
        {

        }
        public CTag(String userQuery)
        {
            
            Value = Create(userQuery);
            Original = userQuery;
        }
        private String Create(String originalString)
        {

            String tag = originalString;  //mock todo: implement an algorithm that create tag from user query string
            return tag;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
