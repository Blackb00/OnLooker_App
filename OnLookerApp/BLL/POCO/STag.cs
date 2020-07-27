
namespace OnLooker.Core
{
    public struct STag
    {
        public string Value { get; set; }
        public string Original { get; private set; }
        public STag(string userQuery, string tag)
        {
            this.Value = tag;
            this.Original = userQuery;
        }
    }
}
