using MyMath;
using number = MyNumber.Rational;
namespace OOPLab3
{
    public partial class Form1 : Form
    {
        private SquareMatrix matrix;

        public Form1()
        {
            InitializeComponent();
            matrix = new SquareMatrix();
            setMatrixView();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SizeScroller_ValueChanged(object sender, EventArgs e)
        {
            if (SizeScroller.Value > 0) 
            {
                matrix.SetSize(Convert.ToInt32(SizeScroller.Value));
                setMatrixView();
            }
            else SizeScroller.Value = 0;
        }

        private void setMatrixView()
        {
            LayoutBox.SetBounds(12, 12, matrix.GetSize() * 50, matrix.GetSize() * 100);
            LayoutBox.Controls.Clear();
            for (int i = 0;i<matrix.GetSize();i++)
            {
                for (int j = 0;j<matrix.GetSize();j++)
                {
                    matrixNum tmp = new matrixNum(matrix.GetElem(i,j));
                    LayoutBox.Controls.Add(tmp);
                }
            }
        }

        private void setMatrixFromView()
        {
            for (int i = 0;i<matrix.GetSize();i++)
            {
                for (int j = 0; j < matrix.GetSize(); j++)
                {
                    if (Convert.ToInt32(LayoutBox.Controls[i * matrix.GetSize() + j].Controls[0].Text)!=0)
                        matrix.SetElem(i, j, new number(
                            Convert.ToInt32(LayoutBox.Controls[i * matrix.GetSize() + j].Controls[2].Text), 
                            Convert.ToInt32(LayoutBox.Controls[i * matrix.GetSize() + j].Controls[0].Text)));
                    else matrix.SetElem(i, j, new number(
                            Convert.ToInt32(LayoutBox.Controls[i * matrix.GetSize() + j].Controls[2].Text),
                            1));
                }
            }
        }

        private void TranspositionButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            matrix.Transposition();
            setMatrixView();
        }

        private void DeterminantButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            if ((matrix.GetDeterminant().getNumerator() < 0)|| (matrix.GetDeterminant().getDenumerator() < 0))
                MessageBox.Show($"  {Math.Abs(matrix.GetDeterminant().getNumerator())}" +
                $"\n- _____\n" +
                $"  {Math.Abs(matrix.GetDeterminant().getDenumerator())}");
            else MessageBox.Show($"{matrix.GetDeterminant().getNumerator()}" +
                $"\n_____\n" +
                $"{matrix.GetDeterminant().getDenumerator()}");
        }

        private void RankButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            MessageBox.Show(matrix.GetRank().ToString());
        }
    }
}