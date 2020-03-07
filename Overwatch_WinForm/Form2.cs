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
        /// <summary>
        /// Приватное поле с формой для ее запуска после завершения Form2.
        /// </summary>
        private Form1 oldForm;

        /// <summary>
        /// Реализация рандомных значений с помощью объекта Random.
        /// </summary>
        private static readonly Random random = new Random();

        /// <summary>
        /// Поля для хранения игрока и противника.
        /// </summary>
        private Unit hero;
        private Unit enemy;

        /// <summary>
        /// Конструктор Form2, для получения персонажей и формы.
        /// </summary>
        /// <param name="oldForm"></param>
        /// <param name="hero"></param>
        /// <param name="enemy"></param>
        public Form2(Form1 oldForm, Unit hero, Unit enemy)
        {
            this.oldForm = oldForm;
            InitializeComponent();
            this.hero = hero;
            this.enemy = enemy;
            label1.Text = $"Ваш персонаж:\n\n{hero.Name}\n" +
                $"DPS: {hero.DPS}\n" +
                $"Headshot DPS: {hero.HDPS}\n" +
                $"Single Shot DPS: {hero.SingleDPS}\n" +
                $"Life: {hero.Life}\n" +
                $"Reload: {hero.Reload}";

            label2.Text = $"Ваш противник:\n\n{enemy.Name}\n" +
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

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;

            int bullets = 5;
            double damage = 0;

            while (hero.Life > 0 && bullets!=0)
            {
                double probability = random.NextDouble();
                if (probability <= 0.7)
                {
                    enemy.Life -= hero.DPS * 0.1;
                    damage += hero.DPS * 0.1;
                }
                bullets -= 1;
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Прицельная атака");
        }
    }
}
