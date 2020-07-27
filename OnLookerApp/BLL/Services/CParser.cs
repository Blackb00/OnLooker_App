using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnLooker
{
    public static class CParser
    {
        public static List<string> ParseWebPage(String url, String regExp)
        {
            List<string> arr = new List<string>();
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                String queryResult = client.DownloadString(url);

                String pattern = regExp;
                RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
                Regex regex = new Regex(pattern, options);
                Match match = regex.Match(queryResult);



                while (match.Success)
                {
                    arr.Add(match.Groups["val"].Value);
                    match = match.NextMatch();
                }
            }

            return arr;
        }


    }
}
