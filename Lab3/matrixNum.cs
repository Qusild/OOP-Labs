using number = MyNumber.Rational;

namespace OOPLab3
{
    public partial class matrixNum : UserControl
    {
        public matrixNum(number? input)
        {
            InitializeComponent();
            if (input == null)
            {
                textBox1.Text = "0";
                textBox2.Text = "1";
            }
            else
            {
                textBox1.Text = input.getNumerator().ToString();
                textBox2.Text = input.getDenumerator().ToString();
            }
        }
    }
}
