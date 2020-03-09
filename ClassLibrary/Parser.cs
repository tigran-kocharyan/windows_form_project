using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace ClassLibrary
{
    /// <summary>
    /// Класс, содержащий всю информацию для парсинга CSV.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Так как в файле используется double по стандартам en-US, нужно это учитывать.
        /// </summary>
        static public CultureInfo changeLocale = new CultureInfo("en-US");

        /// <summary>
        /// Основной метод парсинга.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public string[][] ReadCSV(string path)
        {
            try
            {
                string[] csvText = File.ReadAllLines(path).Where(e => e.Trim() != "").ToArray();
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
                        if(answer[i][j] == "infinity")
                        {
                            answer[i][j] = double.PositiveInfinity.ToString();
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
