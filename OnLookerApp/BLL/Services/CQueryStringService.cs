
using System.Runtime.Serialization;

namespace OnLooker.Core
{
    public class CQueryStringService
    {
        private STag tag { get; set; }
        public CQueryStringService(string originalString)
        {
            string _tag = TagCreate(originalString);

            this.tag = new STag(originalString, _tag);
        }

        private string TagCreate(string originalString)
        {

            string tag = originalString;  //mock todo: implement an algorithm that create tag from user query string
            return tag;
        }
        public STag GetTag()
        {
           return tag;
        }
    }
}
