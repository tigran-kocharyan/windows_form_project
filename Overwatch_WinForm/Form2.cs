using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
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
            this.hero = hero;
            this.enemy = enemy;

            InitializeComponent();

            // Проверяется существование файла, чтобы избежать Exception.
            if (File.Exists("../../../img/battle.ico"))
                this.Icon = new Icon("../../../img/battle.ico");

            // Кнопка атаки противника скрыта.
            button3.Visible = false;
            // Обновляем значение жизней после сражения или загрузки в форму.
            RefreshHeroInfo();
            RefreshEnemyInfo();

            // Проверяем значение полей Life.
            CheckDeath(hero.Life, enemy.Life);
        }

        /// <summary>
        /// Так как у нас три кнопки, то каждый раз прописывать состояние каждой из них такое себе удовольствие
        /// Поэтому украсим немного код и упростим себе жизнь.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        public void ChangeVisibility(bool first, bool second, bool third)
        {
            button1.Visible = first;
            button2.Visible = second;
            button3.Visible = third;
        }

        /// <summary>
        /// Метод для проверки на смерть персонажа 
        /// пользователя или его противника.
        /// </summary>
        /// <param name="heroLife"></param>
        /// <param name="enemyLife"></param>
        public void CheckDeath(double heroLife, double enemyLife)
        {
            if (heroLife <= 0)
            {
                MessageBox.Show($"К сожалению, {enemy.Name} нанес Вашему персонажу {hero.Name} " +
                    $"сокрушительный удар. Вы проиграли :(\n" +
                    $"Повезет в следующий раз!");

                SaveXML.WriteXML(hero, enemy);
                this.Close();
            }
            else if (enemyLife <= 0)
            {
                MessageBox.Show($"Поздравляем! Ваш персонаж {hero.Name} победил противника " +
                    $"{enemy.Name}.\n" +
                    $"Восстанию машин, которые побеждают наш интеллект и тактику, не бывать!");

                SaveXML.WriteXML(hero, enemy);
                this.Close();
            }
        }

        /// <summary>
        /// Обновляет информацию о персонаже.
        /// </summary>
        public void RefreshHeroInfo()
        {
            string life = hero.Life.ToString();
            if (hero.Life <= 0)
            {
                life = "*умер*";
            }

            label1.Text = $"Ваш персонаж:\n\n{hero.Name}\n" +
                $"DPS: {hero.DPS}\n" +
                $"Headshot DPS: {hero.HDPS}\n" +
                $"Single Shot DPS: {hero.SingleDPS}\n" +
                $"Life: {life}\n" +
                $"Reload: {hero.Reload}";
        }

        /// <summary>
        /// Обновляет информацию о противнике.
        /// </summary>
        public void RefreshEnemyInfo()
        {
            string life = enemy.Life.ToString();
            if (enemy.Life <= 0)
            {
                life = "*умер*";
            }

            label2.Text = $"Ваш противник:\n\n{enemy.Name}\n" +
                $"DPS: {enemy.DPS}\n" +
                $"Headshot DPS: {enemy.HDPS}\n" +
                $"Single Shot DPS: {enemy.SingleDPS}\n" +
                $"Life: {life}\n" +
                $"Reload: {enemy.Reload}";
        }

        /// <summary>
        /// При закрытии формы с боем, будет открываться первоначальная форма с выбором персонажа.
        /// Реализация повтора решения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>oldForm
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            oldForm.Show();
        }

        /// <summary>
        /// Метод, для обработки нажатия на кнопку Обычной атаки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            ChangeVisibility(false, false, true);

            int bullets = 5;
            int onTarget = 0;
            double damage = 0;

            while (hero.Life > 0 && bullets != 0)
            {
                double probability = random.NextDouble();
                if (probability <= 0.7)
                {
                    enemy.Life -= hero.DPS * 0.1;
                    damage += hero.DPS * 0.1;
                    onTarget += 1;
                }
                bullets -= 1;
            }

            label3.Text = $"Персонаж {hero.Name} нанес {damage} противнику {enemy.Name} урона," +
                $" попав {onTarget} раз(a).";

            // Проверяем значение полей и обновляем их после нанесения урона.
            RefreshEnemyInfo();
            CheckDeath(hero.Life, enemy.Life);

            // Сохраняем значение полей в XML и фокусируем кнопку атаки противника.
            SaveXML.WriteXML(hero, enemy);
            button3.Focus();
        }

        /// <summary>
        /// Метод, обрабатывающи прицельную атаку персонажа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            ChangeVisibility(false, false, true);

            int onTarget = 0, onHead = 0, bullets = 3;
            double damage = 0;

            while (enemy.Life > 0 && bullets != 0)
            {
                double probability = random.NextDouble();
                if (probability <= 0.3)
                {
                    double headshotProb = random.NextDouble();
                    if (headshotProb <= 0.2)
                    {
                        enemy.Life -= hero.HDPS;
                        onTarget += 1;
                        onHead += 1;
                    }
                    else
                    {
                        enemy.Life -= hero.DPS * 0.4;
                        damage += hero.DPS * 0.4;
                        onTarget += 1;
                    }
                }
                bullets -= 1;
            }

            label3.Text = $"Персонаж {hero.Name} использовал прицельную атаку и нанес " +
                $"{damage} урона противнику " +
                $"{enemy.Name}, попав {onTarget} раз(a) и из них {onHead} в голову.";

            // Проверяем значение полей и обновляем их после нанесения урона.
            RefreshEnemyInfo();
            CheckDeath(hero.Life, enemy.Life);

            // Сохраняем значение полей в XML и фокусируем кнопку атаки противника.
            SaveXML.WriteXML(hero, enemy);
            button3.Focus();
        }

        /// <summary>
        /// Метод, обрабатывающий атаку противника-компьютера, с вероятностью 50/50 выстреливает
        /// либо прицельной атакой, либо обычной.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            ChangeVisibility(true, true, false);

            double chance = random.NextDouble();
            int bullets, onTarget = 0, onHead = 0;
            double damage = 0;

            // Вероятность атаки с прицеливание для бота равна 0,5.
            if (chance <= 0.5)
            {
                bullets = 3;
                while (hero.Life > 0 && bullets != 0)
                {
                    double probability = random.NextDouble();
                    if (probability <= 0.3)
                    {
                        double headshotProb = random.NextDouble();
                        if (headshotProb <= 0.2)
                        {
                            hero.Life -= enemy.HDPS;
                            onTarget += 1;
                            onHead += 1;
                        }
                        else
                        {
                            hero.Life -= enemy.DPS * 0.4;
                            damage += enemy.DPS * 0.4;
                            onTarget += 1;
                        }
                    }
                    bullets -= 1;
                }

                label3.Text = $"Противник {enemy.Name} использовал прицельную атаку и нанес {damage} урона " +
                    $"Вашему персонажу " +
                $"{hero.Name}, попав {onTarget} раз(a) и из них {onHead} в голову.";
            }
            else
            {
                bullets = 5;

                while (hero.Life > 0 && bullets != 0)
                {
                    double probability = random.NextDouble();
                    if (probability <= 0.7)
                    {
                        hero.Life -= enemy.DPS * 0.1;
                        damage += enemy.DPS * 0.1;
                        onTarget += 1;
                    }
                    bullets -= 1;
                }

                label3.Text = $"Противник {enemy.Name} нанес {damage} урона Вашему персонажу {hero.Name}, " +
                    $"попав {onTarget} раз(a).";
            }

            // Проверяем значение полей и обновляем их после нанесения урона.
            RefreshHeroInfo();
            CheckDeath(hero.Life, enemy.Life);

            // Сохраняем значение полей в XML и фокусируем кнопку атаки персонажа пользователя.
            SaveXML.WriteXML(hero, enemy);
            button1.Focus();
        }
    }
}
