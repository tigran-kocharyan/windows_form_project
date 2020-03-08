﻿using System;
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
            this.hero = hero;
            this.enemy = enemy;

            InitializeComponent();

            button3.Visible = false;

            RefreshHeroInfo();

            RefreshEnemyInfo();
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
        /// <param name="e"></param>
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            oldForm.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;

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

            label3.Text = $"Персонаж {hero.Name} нанес {damage} противнику {enemy.Name}," +
                $" попав {onTarget} раз(a).";

            RefreshEnemyInfo();

            if (enemy.Life <= 0)
            {
                MessageBox.Show($"Игрок 1 в роли персонажа {hero.Name} победил противника" +
                    $" {enemy.Name}.\nПоздравляю! " +
                    $"Восстанию машин, которые побеждают наш интеллект и тактику, не бывать!");

                this.Close();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;

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
                $"{damage} противнику " +
                $"{enemy.Name}, попав {onTarget} раз(a) и из них {onHead} в голову.";

            RefreshEnemyInfo();

            if (enemy.Life <= 0)
            {
                MessageBox.Show($"Игрок 1 в роли персонажа {hero.Name} победил противника " +
                    $"{enemy.Name}.\nПоздравляю! " +
                    $"Восстанию машин, которые побеждают наш интеллект и тактику, не бывать!");

                this.Close();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = false;

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

                label3.Text = $"Противник {enemy.Name} использовал прицельную атаку и нанес {damage} " +
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

                label3.Text = $"Противник {enemy.Name} нанес {damage} Вашему персонажу {hero.Name}, " +
                    $"попав {onTarget} раз(a).";
            }

            RefreshHeroInfo();

            if (hero.Life <= 0)
            {
                MessageBox.Show($"К сожалению, {enemy.Name} нанес Вашему персонажу {hero.Name} " +
                    $"сокрушительное поражение.\n" +
                    $"Повезет в следующий раз!");

                this.Close();
            }
        }
    }
}
