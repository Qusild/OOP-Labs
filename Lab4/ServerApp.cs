using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using MyMath;
using Number = MyNumber.Rational;
using static System.Windows.Forms.DataFormats;

namespace OOPLab4
{
    public partial class ServerApp : Form
    {
        public delegate void AddText(TcpClient tcp);
        public AddText myDelegate;
        private SquareMatrix squareMatrix;
        Thread myThread;

        public ServerApp()
        {
            InitializeComponent();
            //tcpListener = new TcpListener(localAddress, 1005);
            //tcpListener.Start();
            squareMatrix = new SquareMatrix();
            myThread = new Thread(begin);
            myThread.Start(this);
        }

        async void begin(Object obj)
        {
            var loaclAddress = IPAddress.Parse("127.0.0.1");
            var tcpListener = new TcpListener(loaclAddress, 1005);
            tcpListener.Start();
            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClient();
                while (tcpClient.Connected)
                {
                    char[] buffer = new char[512];
                    string command = "";
                    // UC ucObj = new UC();
                    using (var networkStream = tcpClient.GetStream())
                    using (var writer = new StreamWriter(networkStream))
                    using (var reader = new StreamReader(networkStream))
                    {
                        writer.AutoFlush = true;
                        //networkStream.ReadTimeout = 0;
                        while (true)
                        {
                            await reader.ReadAsync(buffer, 0, buffer.Length);
                            var message = new string(buffer);
                            string[] values = message.Split('\n');
                            command = values[0];
                            switch (command)
                            {
                                case "Transposition":
                                    squareMatrix.SetMatrix(Convert.ToInt32(values[1]), setListFromString(values[2]));
                                    squareMatrix.Transposition();
                                    break;
                                case "Determinant":
                                    squareMatrix.SetMatrix(Convert.ToInt32(values[1]), setListFromString(values[2]));
                                    squareMatrix.GetDeterminant();
                                    break;
                                case "Rank":
                                    squareMatrix.SetMatrix(Convert.ToInt32(values[1]), setListFromString(values[2]));
                                    squareMatrix.GetRank();
                                    break;
                                case "changeSize":
                                    squareMatrix.SetMatrix(setListFromString(values[2]).Count(), setListFromString(values[2]));
                                    squareMatrix.SetSize(Convert.ToInt32(values[1]));
                                    break;
                                default:
                                    break;
                            }
                            StringBuilder ansString = new StringBuilder();
                            ansString.Append(squareMatrix.GetSize());
                            ansString.Append('\n');
                            for (int i = 0; i < squareMatrix.GetSize(); i++)
                            {
                                for (int j = 0; j < squareMatrix.GetSize(); j++)
                                {
                                    ansString.Append(squareMatrix.GetElem(i, j).getNumerator().ToString());
                                    ansString.Append(' ');
                                    ansString.Append(squareMatrix.GetElem(i, j).getDenumerator().ToString());
                                    ansString.Append(' ');
                                }
                            }
                            ansString.Append('\n');
                            ansString.Append(squareMatrix.GetRank());
                            ansString.Append('\n');
                            ansString.Append(squareMatrix.GetDeterminant().getNumerator());
                            ansString.Append(' ');
                            ansString.Append(squareMatrix.GetDeterminant().getDenumerator());
                            await writer.WriteAsync(ansString.ToString());
                            //Console.WriteLine(/*"Message Recieved: {0}", */ message);
                            break;
                        }
                    }
                }
            }
        }
        private List<List<Number>> setListFromString(string str)
        {
            List<List<Number>> tmp = new List<List<Number>>();
            string[] strTmp = str.Split(' ');
            for (int i = 0; i < squareMatrix.GetSize(); i++)
            {
                tmp.Add(new List<Number>());
                for (int j = 0; j < squareMatrix.GetSize(); j++)
                {
                    tmp[i].Add(new Number(Convert.ToInt32(strTmp[i * squareMatrix.GetSize()*2 + j*2]),
                                 Convert.ToInt32(strTmp[i * squareMatrix.GetSize()*2 + j*2 + 1])));
                }

            }
            return tmp;
        }

        private void ServerApp_Load(object sender, EventArgs e)
        {
            ClientApp client = new ClientApp();
            client.Show();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}