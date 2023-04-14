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
    public partial class MatrixNumComp : UserControl
    {
        public MatrixNumComp(double? re, double? im)
        {
            InitializeComponent();
            if ((re== null) && (im== null))
            {
                textBox1.Text = "1";
                textBox2.Text = "0";
            }
            else
            {
                textBox1.Text = re.ToString();
                textBox2.Text = im.ToString();
            }
        }
    }
}
