using System;

namespace OnLooker.Core
{
    public class ArticleInfo
    { 
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Html { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public CCountry Country { get; set; }
        public STag Tag { get; set; }
     }
}
