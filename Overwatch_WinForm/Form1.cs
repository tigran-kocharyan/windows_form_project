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
        }

        private void Change()
        {
            button1.Text = "Обновить";
            //button1.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
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
                for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
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
            dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentCell.RowIndex);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Введите значение параметра в разумных границах в виде X-X, либо введите минимальное желаемое значение этого параметра.");
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty)
            {
                try
                {
                    string text = textBox1.Text;
                    if (text.Contains('-'))
                    {

                    }
                    else
                    {
                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                this.dataGridView1.Rows[i].Visible = true;
                                string cellValue = this.dataGridView1.Rows[i].Cells[dataGridView1.Columns["Damage per second "].Index].Value.ToString();
                                this.dataGridView1.Rows[i].Selected = false;
                                var temp = double.Parse(cellValue, Parser.changeLocale);
                                this.dataGridView1.Rows[i].Visible = Comparer.Compare(temp, text.Trim());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    textBox1.Text = "";
                    MessageBox.Show($"Вы ввели неправильные данные!\n{ex.Message}");
                }
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != String.Empty)
                {
                    string text = textBox2.Text;
                    if (text.Contains('-'))
                    {

                    }
                    else
                    {
                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                this.dataGridView1.Rows[i].Visible = true;
                                string cellValue = this.dataGridView1.Rows[i].Cells[dataGridView1.Columns["Life"].Index].Value.ToString();
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
                textBox2.Text = "";
                MessageBox.Show($"Вы ввели неправильные данные!\n{ex.Message}");
            }

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text != String.Empty)
                {
                    string text = textBox3.Text;
                    if (text.Contains('-'))
                    {

                    }
                    else
                    {
                        for (var i = 0; i < this.dataGridView1.Rows.Count; i++)
                        {
                            if (this.dataGridView1.Rows[i].Visible == true)
                            {
                                this.dataGridView1.Rows[i].Visible = true;
                                string cellValue = this.dataGridView1.Rows[i].Cells[dataGridView1.Columns["Headshot DPS  "].Index].Value.ToString();
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
                textBox3.Text = "";
                MessageBox.Show($"Вы ввели неправильные данные!\n{ex.Message}");
            }

        }
    }
}
