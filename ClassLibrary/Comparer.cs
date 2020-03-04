using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Comparer
    {
        /// <summary>
        /// Метод для безопасного приведения строки в число.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        static int ReadInt(string message, int left, int right)
        {
            int number;
            do
            {
                Console.WriteLine(message);
            } while (!int.TryParse(Console.ReadLine(), out number) || number < left || number > right);
            return number;
        }

        /// <summary>
        /// Метод для сравнения значения из таблицы с тем, что пользователь вводит в BoxText.
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <param name="boxText"></param>
        static public bool Compare(double rowNumber, string boxText)
        {
            double boxNumber = double.Parse(boxText);

            if (boxNumber <= rowNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Перегрузка метода Compare для сравнения чисел с диапозоне min-max.
        /// </summary>
        /// <param name="rowNumberMin"></param>
        /// <param name="rowNumberMax"></param>
        /// <param name="boxText"></param>
        /// <returns></returns>
        static public bool Compare(double rowNumberMin, double rowNumberMax, string boxText)
        {
            double boxNumber = double.Parse(boxText);

            if (boxNumber <= rowNumberMax && boxNumber >= rowNumberMin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
