using MyMath;
using OOPLab3;
using System.Net.Sockets;
using System.Text;


namespace OOPLab4
{
    public partial class ClientApp : Form
    {
        public ClientApp()
        {
            InitializeComponent();
            this.size = 3;
            for (int i = 0; i < 3; i++)
            {
                this.matrix.Add(new List<(int, int)>());
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
        async Task sendMsg(string commandLine, int size, List<List<(int, int)>> matrix)
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
        }
        public void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public async void SizeScroller_ValueChanged(object sender, EventArgs e)
        {
            if (SizeScroller.Value > 0)
            {
                await communicator.sendMsg("changeSize", (int)SizeScroller.Value, matrix);
                this.matrix = communicator.GetMatrix();
                this.size = communicator.GetSize();
                setMatrixView();
            }
            else SizeScroller.Value = 0;
        }
        private void setMatrixView()
        {
            LayoutBox.SetBounds(12, 12, matrix.Count() * 50, matrix.Count() * 100);
            LayoutBox.Controls.Clear();
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix.Count(); j++)
                {
                    matrixNum tmp = new matrixNum(matrix[i][j].Item1, matrix[i][j].Item2);
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
                    if (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[0].Text) != 0)
                    {
                        matrix[i][j] = (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[2].Text),
                                        Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[0].Text));
                    }
                    else matrix[i][j] = (Convert.ToInt32(LayoutBox.Controls[i * matrix.Count() + j].Controls[2].Text),1);
                }
            }
        }

        public async void TranspositionButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            await communicator.sendMsg("Transposition", size, matrix);
            this.matrix = communicator.GetMatrix();
            setMatrixView();
        }

        public async void DeterminantButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            await communicator.sendMsg("Determinant", size, matrix);
            this.determinant = communicator.GetDeterminant();
            if ((determinant.Item1 < 0) || (determinant.Item2 < 0))
                MessageBox.Show($"  {Math.Abs(determinant.Item1)}" +
                $"\n- _____\n" +
                $"  {Math.Abs(determinant.Item2)}");
            else MessageBox.Show($"{determinant.Item1}" +
                $"\n_____\n" +
                $"{determinant.Item2}");
        }

        public async void RankButton_Click(object sender, EventArgs e)
        {
            setMatrixFromView();
            await communicator.sendMsg("Rank", size, matrix);
            this.rank = communicator.GetRank(); 
            MessageBox.Show(rank.ToString());
        }

        private TcpClient tcpClient = new TcpClient();
        private List<List<(int, int)>> matrix = new List<List<(int, int)>>();
        private int size;
        private int rank;
        private (int, int) determinant;
        private Communicator communicator;
        private void ClientApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
