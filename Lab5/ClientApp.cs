using MyMath;
using MyNumbers;
using System.Net.Sockets;
using System.Text;


namespace OOPLab5
{
    public partial class ClientApp : Form
    {
        public ClientApp()
        {
            InitializeComponent();
            this.size = 3;
            for (int i = 0; i < 3; i++)
            {
                this.matrix.Add(new List<(double, double)>());
                for (int j = 0; j < 3; j++)
                {
                    if (j == i)
                        this.matrix[i].Add((1, 1));
                    else this.matrix[i].Add((0, 1));

                }
            }
            communicator = new Communicator();
            //sendMsg("Init", size, matrix);
            setMatrixView();
        }
        public void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public async void SizeScroller_ValueChanged(object sender, EventArgs e)
        {
            if (SizeScroller.Value > 0)
            {
                await communicator.sendMsg(NumberType, "changeSize", (int)SizeScroller.Value, matrix);
                this.matrix = communicator.GetMatrix();
                this.size = communicator.GetSize();
                setMatrixView();
            }
            else SizeScroller.Value = 0;
        }
        private void setMatrixView()
        {
            int sizeX = 0,sizeY = 0;
            switch (NumberType)
            {
                case "Rational":
                    sizeX = 50; sizeY = 100;
                    break;
                case "Complex":
                    sizeX = 140; sizeY = 40;
                    break;
                case "Double":
                    sizeX = 50; sizeY = 40;
                    break;
            }
            LayoutBox.SetBounds(12, 12, matrix.Count() * sizeX, matrix.Count() * sizeY);
            LayoutBox.Controls.Clear();
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix.Count(); j++)
                {
                    Control tmp=null;
                    switch (NumberType)
                    {
                        case "Rational":
                            tmp = new matrixNum((int)matrix[i][j].Item1, (int)matrix[i][j].Item2);
                            break;
                        case "Complex":
                            tmp = new MatrixNumComp(matrix[i][j].Item1, matrix[i][j].Item2);
                            break;
                        case "Double":
                            tmp = new MatrixNumDouble(matrix[i][j].Item1);
                            break;
                    }
                    
                    LayoutBox.Controls.Add(tmp);
                }
            }
            return;
        }

        private void setMatrixFromView()
        {
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix.Count(); j++)
                {
                    switch (NumberType)
                    {
                        case "Rational":
                            if (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[0].Text) != 0)
                            {
                                matrix[i][j] = (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[2].Text),
                                                Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[0].Text));
                            }
                            else matrix[i][j] = (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[2].Text), 1);
                            break;
                        case "Complex": //Controls[3] - real Controls[1] - imaginary
                            matrix[i][j] = (Convert.ToDouble(LayoutBox.Controls[i * matrix.Count() + j].Controls[3].Text),
                                            Convert.ToDouble(LayoutBox.Controls[i * matrix.Count() + j].Controls[1].Text));
                            break;
                        case "Double": //Controls[1] - Double
                            matrix[i][j] = (Convert.ToDouble(LayoutBox.Controls[i * matrix.Count() + j].Controls[1].Text),0);
                            break;

                    }
                    
                }
            }
        }

        public async void TranspositionButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            await communicator.sendMsg(NumberType, "Transposition", size, matrix);
            this.matrix = communicator.GetMatrix();
            setMatrixView();
        }

        public async void DeterminantButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            await communicator.sendMsg(NumberType,"Determinant", size, matrix);
            determinant = communicator.GetDeterminant();
            string[] tmpAns = determinant.ToString().Split(", ");
            switch(NumberType)
            {
                case "Rational":
                    MessageBox.Show(new Rational(Convert.ToInt32(tmpAns[0].Substring(1)), Convert.ToInt32(tmpAns[1].Substring(0,tmpAns.Length-1))).ToString());
                    break;
                case "Complex": //Controls[3] - real Controls[1] - imaginary
                    MessageBox.Show(new Complex(Convert.ToDouble(tmpAns[0].Substring(1)), Convert.ToDouble(tmpAns[1].Substring(0, tmpAns.Length - 1))).ToString());
                    break;
                case "Double": //Controls[1] - Double
                    MessageBox.Show(new MyNumbers.Double(Convert.ToDouble(tmpAns[0].Substring(1))).ToString());
                    break;
            }
            //MessageBox.Show(tmpAns);
        }

        public async void RankButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            await communicator.sendMsg(NumberType, "Rank", size, matrix);
            int rank = communicator.GetRank(); 
            MessageBox.Show(rank.ToString());
        }

        private TcpClient tcpClient = new TcpClient();
        private List<List<(double, double)>> matrix = new List<List<(double, double)>>();
        private int size;
        private object determinant;
        private int rank;
        private Communicator communicator;
        private void ClientApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void NumberTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumberType = NumberTypePicker.Text;
            setMatrixView();
        }
        private String NumberType = "Rational";
    }
}
