using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overwatch_WinForm
{
    public partial class Form2 : Form
    {
        private Form1 oldForm;

        public Form2(Form1 f)
        {
            oldForm = f;
            InitializeComponent();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            oldForm.Show();
        }
    }
}
