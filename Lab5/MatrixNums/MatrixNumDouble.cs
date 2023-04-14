using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOPLab5
{
    public partial class MatrixNumDouble : UserControl
    {
        public MatrixNumDouble(double? n)
        {
            InitializeComponent();
            textBox2.Hide();
            if (n != null)
                textBox1.Text = n.ToString();
            else textBox1.Text = "0";
        }
    }
}
