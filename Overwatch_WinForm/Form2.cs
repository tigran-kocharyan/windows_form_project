using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace Overwatch_WinForm
{
    public partial class Form2 : Form
    {
        private Form1 oldForm;

        public Form2(Form1 oldForm, Unit hero, Unit enemy)
        {
            this.oldForm = oldForm;
            InitializeComponent();
            label1.Text = $"Вы выбрали персонажа:\n\n{hero.Name}\n" +
                $"DPS: {hero.DPS}\n" +
                $"Headshot DPS: {hero.HDPS}\n" +
                $"Single Shot DPS: {hero.SingleDPS}\n" +
                $"Life: {hero.Life}\n" +
                $"Reload: {hero.Reload}";

            label2.Text = $"Вы выбрали персонажа:\n\n{enemy.Name}\n" +
                $"DPS: {enemy.DPS}\n" +
                $"Headshot DPS: {enemy.HDPS}\n" +
                $"Single Shot DPS: {enemy.SingleDPS}\n" +
                $"Life: {enemy.Life}\n" +
                $"Reload: {enemy.Reload}";
        }

        /// <summary>
        /// При закрытии формы с боем, будет открываться первоначальная форма с выбором персонажа.
        /// Реализация повтора решения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            oldForm.Show();
        }
    }
}
