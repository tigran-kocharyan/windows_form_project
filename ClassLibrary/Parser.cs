using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace ClassLibrary
{
    public class Parser
    {
        static public CultureInfo changeLocale = new CultureInfo("en-US");
        static public string[][] ReadCSV(string path)
        {
            try
            {
                string[] csvText = File.ReadAllLines(path);
                string[][] answer = new string[csvText.Length][];
                for (int i = 0; i < csvText.Length; i++)
                {
                    answer[i] = csvText[i].Split(';');
                    for (int j = 0; j < answer[i].Length; j++)
                    {
                        if(answer[i][j] == String.Empty)
                        {
                            answer[i][j] = "0";
                        }
                    }
                }
                return answer;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
