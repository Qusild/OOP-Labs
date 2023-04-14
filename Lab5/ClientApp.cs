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
        /*async Task sendMsg(string commandLine, int size, List<List<(int, int)>> matrix)
        {
            StringBuilder tmp = new StringBuilder();
            tmp.Append(commandLine);
            tmp.Append("\n");
            tmp.Append(size.ToString());
            tmp.Append("\n");
            foreach(var row in matrix)
            {
                foreach ((int, int) elem in row)
                {
                    tmp.Append(elem.Item1.ToString());
                    tmp.Append(' ');
                    tmp.Append(elem.Item2.ToString());
                    tmp.Append(' ');
                }
                    
            }
            string message = tmp.ToString();
            using (var tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync("127.0.0.1", 1005);
                using (var networkStream = tcpClient.GetStream())
                using (var writer = new StreamWriter(networkStream))
                using (var reader = new StreamReader(networkStream))
                {
                    //while (true)
                    //{
                    writer.AutoFlush = true;
                    networkStream.ReadTimeout = 5000;
                    char[] buffer = new char[512];
                    //Console.WriteLine("Write Message");;
                    //bufferedStream.WriteAsync(byteArray, 0, byteArray.Length);
                    await writer.WriteAsync(message);
                    //bufferedStream.Flush();
                    //}
                    await reader.ReadAsync(buffer, 0, buffer.Length);
                    string ansBuffered = new string(buffer);
                    //bufferedStream.ReadAsync(buffer, 0, buffer.Length);
                    string[] values = ansBuffered.Split('\n');
                    this.size = Convert.ToInt32(values[0]);
                    this.rank = Convert.ToInt32(values[2]);
                    this.determinant = (Convert.ToInt32(values[3].Split(' ')[0]), Convert.ToInt32(values[3].Split(' ')[1]));
                    string[] strTmp = values[1].Split(' ');
                    List<List<(int, int)>> tmpList = new List<List<(int, int)>>();
                    for (int i = 0; i < size; i++)
                    {
                        tmpList.Add(new List<(int, int)>());
                        for (int j = 0; j < size; j++)
                        {
                            tmpList[i].Add((Convert.ToInt32(strTmp[i * size * 2 + j * 2]),
                                         Convert.ToInt32(strTmp[i * size * 2 + j * 2 + 1])));
                        }

                    }
                    this.matrix = tmpList;
                }
                tcpClient.Close();
            }
        }*/
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
                case "Рациональные":
                    sizeX = 50; sizeY = 100;
                    break;
                case "Комплексные":
                    sizeX = 140; sizeY = 40;
                    break;
                case "Вещественные":
                    sizeX = 60; sizeY = 40;
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
                        case "Рациональные":
                            tmp = new matrixNum((int)matrix[i][j].Item1, (int)matrix[i][j].Item2);
                            break;
                        case "Комплексные":
                            tmp = new MatrixNumComp(matrix[i][j].Item1, matrix[i][j].Item2);
                            break;
                        case "Вещественные":
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
                        case "Рациональные":
                            if (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[0].Text) != 0)
                            {
                                matrix[i][j] = (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[2].Text),
                                                Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[0].Text));
                            }
                            else matrix[i][j] = (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[2].Text), 1);
                            break;
                        case "Комплексные": //Controls[3] - real Controls[1] - imaginary
                            matrix[i][j] = (Convert.ToDouble(LayoutBox.Controls[i * matrix.Count() + j].Controls[3].Text),
                                            Convert.ToDouble(LayoutBox.Controls[i * matrix.Count() + j].Controls[1].Text));
                            break;
                        case "Вещественные": //Controls[1] - Double
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
                case "Рациональные":
                    MessageBox.Show(new Rational(Convert.ToInt32(tmpAns[0].Substring(1)), Convert.ToInt32(tmpAns[1].Substring(0,tmpAns.Length-1))).ToString());
                    break;
                case "Комплексные": //Controls[3] - real Controls[1] - imaginary
                    MessageBox.Show(new Complex(Convert.ToDouble(tmpAns[0].Substring(1)), Convert.ToDouble(tmpAns[1].Substring(0, tmpAns.Length - 1))).ToString());
                    break;
                case "Вещественные": //Controls[1] - Double
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
        private String NumberType = "Рациональные";
    }
}
