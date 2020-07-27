using OnLooker.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OnLooker.DataBaseAccess
{
    class CV_0001 : AVersion
    {
        
        static String _fileWithCountries = "./../../../DataBaseAccess/Source/Countries.txt";
        internal override String Major
        {
            get { return "01"; }
        }
        internal override String Minor
        {
            get { return "01"; }
        }
        internal override String FileNumber
        {
            get { return "0001"; }
        }
        internal override String Comment
        {
            get { return "Countries insert"; }
        }

        internal override void GetVersion()
        {
            /*this version is for putting default values in db (the names of all existing countries)*/
            /*-- get countries from text file --*/
            if (File.Exists(_fileWithCountries))
            {
                List<CountryInfo> countries = new List<CountryInfo>();
                using (StreamReader sr = new StreamReader(_fileWithCountries))
                {
                    String ln;
                    while ((ln = sr.ReadLine()) != null)
                    {
                        String[] arr = ln.Split(' ');
                        List<String> arr2 = new List<string>();
                        foreach (var s in arr)
                        {
                            if (s != "")
                                arr2.Add(s);
                        }
                        StringBuilder str = new StringBuilder();
                        for (var i = 0; i < arr2.Count - 3; i++)
                        {
                            str.AppendFormat(arr2[i]);
                            str.AppendFormat(" ");
                        }
                        countries.Add(new CountryInfo
                        {
                            Code = arr2[arr2.Count - 2],
                            Name = str.ToString()
                        });
                    }
                }
                CCountryGateway countryGateway = new CCountryGateway();
                foreach (var country in countries)
                {
                    countryGateway.Create(country);
                }

                Upgrade();
            }
            else
            {
                Console.WriteLine($"Can't find file {_fileWithCountries}");
            }
        }
    }
}
