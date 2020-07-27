using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingNews.WebService.Models
{
    public class CArticle
    {
        public String Title { get; set; }
        public String Content { get; set; }
        public byte[] Html { get; set; }
        public String Url { get; set; }
        public DateTime Date { get; set; }
        public CCountry Country { get; set; }
        public CTag[] Tags { get; set; }
    }
}
