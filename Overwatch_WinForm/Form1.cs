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
    public partial class Form1 : Form
    {
        /// <summary>
        /// Констркутор формы.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // Проверяется существование файла, чтобы избежать Exception.
            if (File.Exists("../../../img/logo.ico"))
                this.Icon = new Icon("../../../img/logo.ico");
            // Скрываем от пользователя все кнопки и тексты.
            MakeInvisible();
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        }

        /// <summary>
        /// Объект Random для генерации случайного противника.
        /// </summary>
        private static readonly Random random = new Random();

        /// <summary>
        /// Поле для хранения выбранной ячейки.
        /// </summary>
        private static int rowIndex;

        /// <summary>
        ///  Делает кнопки и тексты невидимыми пользователю.
        /// </summary>
        private void MakeInvisible()
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
        }

        /// <summary>
        /// Делает кнопки и тексты видимыми пользователю.
        /// </summary>
        private void MakeVisible()
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            button3.Visible = true;
        }

        /// <summary>
        /// Делает все ячейки видимыми после фильтрации.
        /// </summary>
        private void Renew()
        {
            label5.Text = String.Empty;
            label6.Text = String.Empty;
            label7.Text = String.Empty;
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i].Visible = true;
            }
        }

        /// <summary>
        /// Метод обновляет значение кнопки считывания для последующего обновления таблицы.
        /// </summary>
        private void Change()
        {
            button1.Text = "Сбросить Фильтр";
            MessageBox.Show($"Здравствуй, Игрок! Вот, что тебе нужно сделать:\n\n" +
                $"В появившейся таблице кликни по номеру строки слева, чтобы получить информацию о выбранном." +
                $"\n\n{new String('-', 92)}\nПамятка по работе фильтра:\n\n" +
                "Ты можешь фильтровать по нескольким параметрам одновременно, но не забывай тогда обновлять таблицу кнопкой " +
                "'Сбросить Фильтр'.\n\nНе советую шутить с этой игрушкой дьявола...");
            Button2_Click(null, null);
        }

        /// <summary>
        /// Метод для обработки события изменения ячейки. Избавляет пользователя от проблем 
        /// с некорректными данными.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 0)
                {
                    if (this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()
                        == "infinity")
                    {
                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                            double.PositiveInfinity.ToString();
                    }
                    else
                    {
                        if (double.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) < 0) throw new ArgumentException();
                        else
                            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = double.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()).ToString();
                    }
                }
            }
            catch (Exception)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                MessageBox.Show("Вы ввели недопустимое значение в ячейку с числом " +
                    "или использовали недопустимую локаль! Советуем использовать ',' вместо '.' в double." +
                    "Теперь там будет стоять 0! Вводите, пожалуйста, либо int, либо double.\n" +
                    "Не думаю, что персонаж умеет стрелять строками :)", caption : "Ошибка ввода значений в ячейку.");
            }
        }

        /// <summary>
        /// Кнопка для считывания CSV-файла и обновления таблицы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Считать CSV")
                {
                    Change();
                    MakeVisible();
                    string[][] stats = Parser.ReadCSV("../../Overwatch.csv");
                    for (int i = 0; i < stats[0].Length; i++)
                    {
                        this.dataGridView1.Columns.Add(stats[0][i], stats[0][i]);
                    }
                    for (int i = 1; i < stats.Length; i++)
                    {
                        this.dataGridView1.Rows.Add(stats[i]);
                    }
                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        this.dataGridView1.Rows[i].HeaderCell.Value = $"{i + 1}";
                    }
                }
                else
                {
                    textBox1.Text = String.Empty;
                    textBox2.Text = String.Empty;
                    textBox3.Text = String.Empty;
                    Renew();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка при чтении CSV-файла. Возможно, Вы совершили критичную ошибку" +
                    ", изменив что-то там, либо просто удалили всю информацию из файла. Так как теперь будет невозможно" +
                    "выбрать персонажей, советуем Вам вернуть все на место и запустить игру снова :)");
                this.Close();
            }
            
        }

        /// <summary>
        /// Кнопка-памятка по фильтрации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для фильтрации:\n\nВведите в TextBox значение параметра" +
                " в разумных границах в виде X-Y, " +
                "либо введите минимальное желаемое значение этого параметра.\n\n" +
                "Для выбора персонажа:\n\n Двойной клик по номеру " +
                "в заголовке строки.\nЕсли Вы вдруг изменили значение в какой-то ячейке персонажа, но перед этим выбрали этого персонажа, " +
                "то не забудьте перевыбрать этого персонажа, чтобы обновить его параметры.");
        }

        /// <summary>
        /// Кнопка для реализации фильтрации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button3_Click(object sender, EventArgs e)
        {
            Renew();
            try
            {
                if (textBox1.Text != String.Empty)
                {
                    string text = textBox1.Text.Trim();
                    label5.Text = text;
                    if (text.Contains("-"))
                    {
                        string[] splitted = text.Split('-');
                        double minNumber = double.Parse(splitted[0].ToString(),
                            Parser.changeLocale);
                        double maxNumber = double.Parse(splitted[1].ToString(),
                            Parser.changeLocale);

                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                string cellValue = this.dataGridView1.Rows[i].
                                    Cells[dataGridView1.Columns["Damage per second "].Index].Value.ToString();
                                this.dataGridView1.Rows[i].Selected = false;
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(minNumber, maxNumber, cellValue);
                            }
                        }
                    }
                    else
                    {
                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                this.dataGridView1.Rows[i].Visible = true;
                                string cellValue = this.dataGridView1.Rows[i].
                                    Cells[dataGridView1.Columns["Damage per second "].Index].Value.ToString();
                                this.dataGridView1.Rows[i].Selected = false;
                                var temp = double.Parse(cellValue, Parser.changeLocale);
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(temp, text);
                            }
                        }
                    }
                }
                if (textBox2.Text != String.Empty)
                {
                    string text = textBox2.Text.Trim();
                    label6.Text = text;
                    if (text.Contains('-'))
                    {
                        string[] splitted = text.Split('-');
                        double minNumber = double.Parse(splitted[0].ToString(),
                            Parser.changeLocale);
                        double maxNumber = double.Parse(splitted[1].ToString(),
                            Parser.changeLocale);

                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                string cellValue = this.dataGridView1.Rows[i].
                                    Cells[dataGridView1.Columns["Life"].Index].Value.ToString();
                                this.dataGridView1.Rows[i].Selected = false;
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(minNumber, maxNumber, cellValue);
                            }
                        }
                    }
                    else
                    {
                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                this.dataGridView1.Rows[i].Visible = true;
                                string cellValue = this.dataGridView1.Rows[i].
                                    Cells[dataGridView1.Columns["Life"].Index].Value.ToString();
                                this.dataGridView1.Rows[i].Selected = false;
                                var temp = double.Parse(cellValue, Parser.changeLocale);
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(temp, text);
                            }
                        }
                    }
                }
                if (textBox3.Text != String.Empty)
                {
                    string text = textBox3.Text.Trim();
                    label7.Text = text;
                    if (text.Contains('-'))
                    {
                        string[] splitted = text.Split('-');
                        double minNumber = double.Parse(splitted[0].ToString(),
                            Parser.changeLocale);
                        double maxNumber = double.Parse(splitted[1].ToString(),
                            Parser.changeLocale);

                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                string cellValue = this.dataGridView1.Rows[i].
                                    Cells[dataGridView1.Columns["Headshot DPS  "].Index].Value.ToString();
                                this.dataGridView1.Rows[i].Selected = false;
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(minNumber, maxNumber, cellValue);
                            }
                        }
                    }
                    else
                    {
                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                this.dataGridView1.Rows[i].Visible = true;
                                string cellValue = this.dataGridView1.Rows[i].
                                    Cells[dataGridView1.Columns["Headshot DPS  "].Index].Value.ToString();
                                this.dataGridView1.Rows[i].Selected = false;
                                var temp = double.Parse(cellValue, Parser.changeLocale);
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(temp, text);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Button1_Click(null, null);
                MessageBox.Show($"Вы ввели неправильные данные!\n{ex.Message}");
            }
            finally
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        /// <summary>
        /// Выводит информацию о персонаже в Label_4.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var row = this.dataGridView1.Rows[e.RowIndex];
            rowIndex = e.RowIndex;

            // Проверка, чтобы избежать выбора персонажа с неправильным полем.
            for (int i = 1; i < 5; i++)
            {
                try
                {
                    double.Parse(row.Cells[i].Value.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("Возможно, Вам удалось изменить значение в ячейку на невалидное и Вы решили" +
                        "выбрать этого персонажа. Все поля, кроме валидных, будут иметь теперь значение 0.");
                    row.Cells[i].Value = "0";
                }
            }
            label4.Text = $"Вы выбрали персонажа:\n\n{row.Cells[0].Value.ToString()}\n" +
                $"DPS: {row.Cells[1].Value.ToString()}\n" +
                $"Headshot DPS: {row.Cells[2].Value.ToString()}\n" +
                $"Single Shot DPS: {row.Cells[3].Value.ToString()}\n" +
                $"Life: {row.Cells[4].Value.ToString()}\n" +
                $"Reload: {row.Cells[5].Value.ToString()}";
        }

        /// <summary>
        /// Событие, которое показывает кнопку начала игры, как только игрок выбрал персонажа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label4_TextChanged(object sender, EventArgs e)
        {
            button4.Visible = true;
        }

        /// <summary>
        /// Метод, необходимый для перехода к процессу игры с выбранным персонажем.
        /// Так как иногда пользователь может загрузить сохранение, в котором умер, а потом начать игру, 
        /// то сразу же в этом методе сделаем кнопку загрузки сохранения видимой.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(label4.Text.ToString());
                int randomIndex;
                var rowHero = this.dataGridView1.Rows[rowIndex];
                Unit hero = new Unit(rowHero.Cells[0].Value.ToString(), double.Parse(rowHero.Cells[1].Value.ToString()),
                    double.Parse(rowHero.Cells[2].Value.ToString()), double.Parse(rowHero.Cells[3].Value.ToString()),
                    double.Parse(rowHero.Cells[4].Value.ToString()), double.Parse(rowHero.Cells[5].Value.ToString()));

                do
                {
                    randomIndex = random.Next(0, 58);
                } while (randomIndex == rowIndex);

                var rowEnemy = this.dataGridView1.Rows[randomIndex];
                Unit enemy = new Unit(rowEnemy.Cells[0].Value.ToString(), double.Parse(rowEnemy.Cells[1].Value.ToString()),
                    double.Parse(rowEnemy.Cells[2].Value.ToString()), double.Parse(rowEnemy.Cells[3].Value.ToString()),
                    double.Parse(rowEnemy.Cells[4].Value.ToString()), double.Parse(rowEnemy.Cells[5].Value.ToString()));

                button5.Visible = true;

                // Проверка, чтобы избежать выбора персонажа с неправильным полем.
                for (int i = 1; i < 5; i++)
                {
                    try
                    {
                        double.Parse(rowEnemy.Cells[i].Value.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Возможно, Вам удалось изменить значение в ячейку на невалидное и Вы решили" +
                            "выбрать этого персонажа. Все поля, кроме валидных, будут иметь теперь значение 0.");
                        rowEnemy.Cells[i].Value = "0";
                    }
                }

                if (double.Parse(rowEnemy.Cells[4].Value.ToString()) == 0)
                {
                    MessageBox.Show($"У Вашего рандомно выбранного противника  {rowEnemy.Cells[0].Value.ToString()} " +
                        $"уже 0 жизней, навряд ли Вы хотите бить труп :(\nДадим Богу Рандома выбрать Вам соперника" +
                        $"подстать.");;
                }
                else if (double.Parse(rowHero.Cells[4].Value.ToString()) == 0)
                {
                    MessageBox.Show($"У Вашего выбранного персонажа  {rowHero.Cells[0].Value.ToString()} " +
                        $"уже 0 жизней, навряд ли Вы сможете сражаться этим трупом :(\n" +
                        $"Предлагаю Вам перевыбрать чуть более живучего персонажа ;)");
                }
                else if (double.Parse(rowHero.Cells[4].Value.ToString()) == double.PositiveInfinity
                    && double.Parse(rowEnemy.Cells[4].Value.ToString()) == double.PositiveInfinity)
                {
                    MessageBox.Show($"Вау! Вы изменили значения в CSV-файле или в XML и теперь Ваш персонаж" +
                        $" {rowHero.Cells[0].Value.ToString()} и персонаж Вашего противника " +
                        $"{rowEnemy.Cells[0].Value.ToString()} имеют бесконечное количество жизней.\n" +
                        $"Очевидно, что игра так же будет бесконечной, поэтому присуждаю Вам честную ничью, " +
                        $"чтобы не мучить, в ожидании завершения сражения :)");
                }
                else
                {
                    new Form2(this, hero, enemy).Show();
                    this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так!");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (File.Exists("../../../saves/autosave.xml"))
            {
                try
                {
                    Unit[] units = SaveXML.ReadXML();

                    if (units[0].Life <= 0)
                    {
                        MessageBox.Show($"К сожалению, {units[1].Name} нанес Вашему персонажу {units[0].Name} " +
                            $"сокрушительное поражение в этом сохранении и оно не может быть использовано.\n" +
                            $"Поэтому, лучше начните Новую Игру, чтобы соперник не пинал Вашего мертвого" +
                            $"персонажа :)");
                        button5.Visible = false;
                    }
                    else if (units[1].Life <= 0)
                    {
                        MessageBox.Show($"Поздравляем! Ваш персонаж {units[0].Name} победил противника " +
                            $"{units[1].Name} в этом сохранении  и оно не может быть использовано.\n" +
                            $"Поэтому, лучше начните Новую Игру, чтобы не добивать мертвого соперника :)");
                        button5.Visible = false;
                    }
                    else if(units[1].Life == double.PositiveInfinity && units[1].Life == double.PositiveInfinity)
                    {
                        MessageBox.Show($"Вау! Вы изменили значения в CSV-файле или в XML и теперь Ваш персонаж" +
                            $" {units[0].Name} и персонаж Вашего противника " +
                            $"{units[1].Name} имеют бесконечное количество жизней.\n" +
                            $"Очевидно, что игра так же будет бесконечной, поэтому присуждаю Вам честную ничью, " +
                            $"чтобы не мучить, в ожидании завершения сражения :)");
                        button5.Visible = false;
                    }
                    else
                    {
                        new Form2(this, units[0], units[1]).Show();
                        this.Hide();
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так при чтении Вашего сохранения! " +
                        "Скорее всего, Вы его повредили. Советуем начать Новую Игру и не исправлять файл с " +
                        "сохранениями, чтобы потом прочитать его :)");
                    button5.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Извините, но у Вас нет последнего сохранения :(\n\n" +
                    "Скорее всего Вы еще не начинали игру или удалили файл XML");
            }
        }
    }
}