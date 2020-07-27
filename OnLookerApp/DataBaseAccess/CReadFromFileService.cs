using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OnLooker
{
    public static class CReadFromFileService
    {
        public static Dictionary<String, String> ReadFromFileAsDictionary(string path)
        {
            Dictionary<String, String> data = new Dictionary<string, string>();
            if (File.Exists(path))
            {
                try
                {
                    String jsonString = File.ReadAllText(path);
                    data = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                    
                }
            }
            else
            {
                Console.WriteLine($"Can't find file {path}");
            }
            return data;
        }
    }
}
