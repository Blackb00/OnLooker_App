using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnLooker.Core;

namespace OnLooker
{
    public class RegularExpressions: AParsingExpressions              //todo: move all String implementations to json 
    {
        public RegularExpressions()
        {
            base.FindQeryUrls =
                @"<div class=""list-item""><div class=""list-item__content""><a href=""(?<val>.*?)""";
            base.GetContent = @"<div class=""article__text"">(?<val>.*?)</div>";
            base.GetAllTags = @"<.*>";
        }
    }
}
