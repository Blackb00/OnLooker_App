using System;

namespace RiaCrawlerWCF
{
    class CParsingKeys
    {
        public String FindQeryUrls = @"<div class=""list-item""><div class=""list-item__content""><a href=""(?<val>.*?)""";
         public String GetContent = @"<div class=""article__text"">(?<val>.*?)</div>";
        public String GetAllTags = @"<.*>";
        public String GetTitle = @"<h1 class=""article__title"">(?<val>.*?)</h1>";


    }
}
