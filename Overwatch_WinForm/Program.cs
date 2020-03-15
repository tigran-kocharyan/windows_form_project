using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace Overwatch_WinForm
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Во избежание непредвиденных ошибок, ловим их так же в Main().
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception)
            {

                MessageBox.Show("Вы что-то натворили :(\nПросим Вас оценивать работу без попыток через " +
                    "изменение файлов сломать программу.");
            }
        }
    }
}
