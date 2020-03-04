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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
        }

        private void Change()
        {
            button1.Text = "Обновить";
            /*MessageBox.Show($"Здравствуй, Игрок! Вот, что тебе нужно сделать:\n\n" +
                $"В появившейся таблице кликни по номеру строки слева, чтобы получить информацию о выбранном." +
                $"\n\n{new String('-', 92)}\nПамятка по работе фильтра:\n\n" +
                "Ты можешь фильтровать по нескольким параметрам одновременно, но не забывай тогда обновлять таблицу кнопкой " +
                "'Обновить'.\n\nНе советую шутить с этой игрушкой дьявола...");*/
            //button1.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            if (button1.Text == "Считать CSV Ваших Персонажей")
            {
                Change();
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
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    this.dataGridView1.Rows[i].Visible = true;
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            label4.Text = $"Hero";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Введите значение параметра в разумных границах в виде X-Y, " +
                "либо введите минимальное желаемое значение этого параметра.");
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty)
            {

                string text = textBox1.Text.Trim();
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
                                Cells[dataGridView1.Columns["Life"].Index].Value.ToString();
                            this.dataGridView1.Rows[i].Selected = false;
                            this.dataGridView1.Rows[i].Visible = Comparer.Compare(minNumber, maxNumber, text.Trim());
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
                            this.dataGridView1.Rows[i].Visible = Comparer.Compare(temp, text.Trim());
                        }
                    }
                }

                //catch (Exception ex)
                //{
                //    Button1_Click(null, null);
                //    MessageBox.Show($"Вы ввели неправильные данные!\n{ex.Message}");
                //}
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != String.Empty)
                {
                    string text = textBox2.Text.Trim();
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
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(minNumber, maxNumber, text.Trim());
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
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(temp, text.Trim());
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

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text != String.Empty)
                {
                    string text = textBox3.Text.Trim();
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
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(minNumber, maxNumber, text.Trim());
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
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(temp, text.Trim());
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

        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 0)
                {
                    if (this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "infinity")
                    {
                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                            double.PositiveInfinity.ToString();
                    }
                    else
                    {
                        double.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                            Parser.changeLocale);
                    }

                }
            }
            catch (Exception)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                MessageBox.Show("Вы ввели недопустимое значение в ячейку с числом! " +
                    "Теперь там будет стоять 0! Вводите, пожалуйста, либо int, либо double.\n" +
                    "Не думаю, что персонаж умеет стрелять строками :)");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}