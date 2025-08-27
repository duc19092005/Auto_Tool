using OpenQA.Selenium.DevTools.V100.Overlay;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newestTool.helper
{
    internal class readfileHelper
    {
        public static async Task<(bool , Dictionary<string, string>?)> readFile(string filePath)
        {
            Dictionary<string , string> keyValuePairs = new Dictionary<string , string>();
            var fileData = await File.ReadAllLinesAsync(filePath);
          
            foreach (var line in fileData)
            {
                try
                {
                    if (String.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var parts = line.Replace(" ", "").Split('-', StringSplitOptions.RemoveEmptyEntries);

                    // Có 2 cách

                    if (parts[1].Contains("{") && parts[1].Contains("}"))
                    {
                        var ReplaceAndTrim = parts[1].Replace("{", "").Replace("}", "").Replace(" ", "").Trim();
                        keyValuePairs.Add(parts[0], ReplaceAndTrim);

                    }
                    else
                    {
                        keyValuePairs.Add(parts[0], parts[1]);
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Lỗi : " + ex.ToString());
                    return (false, null);
                }
            }
            return (true , keyValuePairs);
        }
    }
}
