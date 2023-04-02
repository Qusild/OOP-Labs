using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab4
{
    public class Communicator
    {
        public Communicator()
        {
            Client = new TcpClient();
        }
        public async Task sendMsg(string commandLine, int size, List<List<(int, int)>> matrix)
        {
            StringBuilder tmp = new StringBuilder();
            tmp.Append(commandLine);
            tmp.Append("\n");
            tmp.Append(size.ToString());
            tmp.Append("\n");
            foreach (var row in matrix)
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
        public int GetSize()
        {
            return this.size;
        }
        public int GetRank()
        {
            return this.rank;
        }
        public (int, int) GetDeterminant()
        {
            return this.determinant;
        }
        public List<List<(int, int)>> GetMatrix()
        {
            return this.matrix;
        }
        private TcpClient Client;
        private List<List<(int, int)>> matrix = new List<List<(int, int)>>();
        private int size;
        private int rank;
        private (int, int) determinant;
    }
}
