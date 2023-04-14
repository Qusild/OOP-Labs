using number = MyNumbers.Rational;

namespace OOPLab5
{
    public partial class matrixNum : UserControl
    {
        public matrixNum(int? numerator, int? denumerator)
        {
            InitializeComponent();
            if ((numerator == null)&&(denumerator == null))
            {
                textBox1.Text = "0";
                textBox2.Text = "1";
            }
            else
            {
                textBox1.Text = numerator.ToString();
                textBox2.Text = denumerator.ToString();
            }
        }
    }
}
