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
using MyMatrix;
using static System.Windows.Forms.DataFormats;
using MyNumbers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MyMath;
using System.Runtime.InteropServices.Marshalling;

namespace OOPLab5
{
    public partial class ServerApp : Form
    {
        public delegate void AddText(TcpClient tcp);
        public AddText myDelegate;
        //private object squareMatrix;
        Thread myThread;
        private string command;
        private string NumberType;
        private string matrixStr;
        private string matrixSize;
        public ServerApp()
        {
            InitializeComponent();
            //tcpListener = new TcpListener(localAddress, 1005);
            //tcpListener.Start();
            myThread = new Thread(begin);
            myThread.Start(this);
        }
        private string matrixManager()
        {
            string[] strTmp = matrixStr.Split(' ');
            int realSize = (int)Math.Sqrt((strTmp.Count() - 1) / 2);
            StringBuilder ans = new StringBuilder();
            switch (NumberType)
            {
                case "Rational":
                    SquareMatrix<Rational> squareMatrixRat = new SquareMatrix<Rational>(Convert.ToInt32(matrixSize));
                    //List<List<Rational>> tmpRat = new List<List<Rational>>();
                    for (int i = 0; i < squareMatrixRat.GetSize(); i++)
                    {
                        //tmpRat.Add(new List<Rational>());
                        for (int j = 0; j < squareMatrixRat.GetSize(); j++)
                        {
                            Rational newElemRat;
                            if (j < realSize)
                            {
                                if (i< realSize)
                                    newElemRat = new Rational(Convert.ToInt32(strTmp[i * realSize * 2 + j * 2]),
                                                          Convert.ToInt32(strTmp[i * realSize * 2 + j * 2 + 1]));
                                else newElemRat = new Rational(0, 1);
                            }
                                
                            else newElemRat = new Rational(0, 1);
                            squareMatrixRat.SetElem(i,j, newElemRat);
                        }
                    }
                    matrixCalc<Rational>(squareMatrixRat);
                    ans.Append(squareMatrixRat.GetSize());
                    ans.Append('\n');
                    for (int i = 0; i < squareMatrixRat.GetSize(); i++)
                    {
                        for (int j = 0; j < squareMatrixRat.GetSize(); j++)
                        {
                            ans.Append((squareMatrixRat.GetElem(i, j)).getNumerator().ToString());
                            ans.Append(' ');
                            ans.Append((squareMatrixRat.GetElem(i, j)).getDenumerator().ToString());
                            ans.Append(' ');
                        }
                    }
                    ans.Append('\n');
                    ans.Append(squareMatrixRat.getRank());
                    ans.Append('\n');
                    ans.Append((squareMatrixRat.GetDeterminant()).getNumerator().ToString());
                    ans.Append(' ');
                    ans.Append((squareMatrixRat.GetDeterminant()).getDenumerator().ToString());
                    ans.Append(' ');
                    break;
                case "Complex":
                    SquareMatrix<Complex> squareMatrixCom = new SquareMatrix<Complex>(Convert.ToInt32(matrixSize));
                    //List<List<Complex>> tmpCom = new List<List<Complex>>();
                    for (int i = 0; i < squareMatrixCom.GetSize(); i++)
                    {
                        //tmpCom.Add(new List<Complex>());
                        for (int j = 0; j < squareMatrixCom.GetSize(); j++)
                        {
                            Complex newElemCom;
                            if (j < realSize)
                            {
                                if (i < realSize)
                                    newElemCom = new Complex(Convert.ToDouble(strTmp[i * realSize * 2 + j * 2]),
                                                       Convert.ToDouble(strTmp[i * realSize * 2 + j * 2 + 1]));
                                else newElemCom = new Complex(0, 0);
                            }
                            else newElemCom = new Complex(0, 0);
                            squareMatrixCom.SetElem(i,j,newElemCom);
                        }
                    }
                    matrixCalc<Complex>(squareMatrixCom);
                    ans.Append(squareMatrixCom.GetSize());
                    ans.Append('\n');
                    for (int i = 0; i < squareMatrixCom.GetSize(); i++)
                    {
                        for (int j = 0; j < squareMatrixCom.GetSize(); j++)
                        {
                            ans.Append((squareMatrixCom.GetElem(i, j)).getRe().ToString());
                            ans.Append(' ');
                            ans.Append((squareMatrixCom.GetElem(i, j)).getIm().ToString());
                            ans.Append(' ');
                        }
                    }
                    ans.Append('\n');
                    ans.Append(squareMatrixCom.getRank());
                    ans.Append('\n');
                    ans.Append((squareMatrixCom.GetDeterminant()).getRe().ToString());
                    ans.Append(' ');
                    ans.Append((squareMatrixCom.GetDeterminant()).getIm().ToString());
                    ans.Append(' ');
                    break;
                case "Double":
                    SquareMatrix<MyNumbers.Double> squareMatrixDb = new SquareMatrix<MyNumbers.Double>(Convert.ToInt32(matrixSize));
                    for (int i = 0; i < squareMatrixDb.GetSize(); i++)
                    {
                        //tmpCom.Add(new List<Complex>());
                        for (int j = 0; j < squareMatrixDb.GetSize(); j++)
                        {
                            MyNumbers.Double newElemDb;
                            if (j< realSize)
                            {
                                if (i < realSize)
                                    newElemDb = new MyNumbers.Double(Convert.ToDouble(strTmp[i * realSize* 2 + j * 2]));
                                else newElemDb = new MyNumbers.Double(0);
                            }
                            else newElemDb = new MyNumbers.Double(0);
                            squareMatrixDb.SetElem(i, j, newElemDb);
                        }
                    }
                    matrixCalc<MyNumbers.Double>(squareMatrixDb);
                    ans.Append(squareMatrixDb.GetSize());
                    ans.Append('\n');
                    for (int i = 0; i < squareMatrixDb.GetSize(); i++)
                    {
                        for (int j = 0; j < squareMatrixDb.GetSize(); j++)
                        {
                            ans.Append((squareMatrixDb.GetElem(i, j)).getDouble().ToString());
                            ans.Append(" 0 ");
                        }
                    }
                    ans.Append('\n');
                    ans.Append(squareMatrixDb.getRank());
                    ans.Append('\n');
                    ans.Append((squareMatrixDb.GetDeterminant()).getDouble().ToString());
                    ans.Append(" 0 ");
                    break;
            }
            return ans.ToString();
        }
        private void matrixCalc<T>(SquareMatrix<T> squareMatrix) where T : IMyNumbers<T>
        {
            switch (command)
            {
                case "Transposition":
                    squareMatrix.Transposition();
                    break;
                case "Determinant":
                    squareMatrix.GetDeterminant();
                    break;
                case "Rank":
                    squareMatrix.getRank();
                    break;
                /*case "changeSize":
                    squareMatrix.SetMatrix(squareMatrix.GetSize(), setListFromString(values[3], values[0]));
                    break;*/
                default:
                    break;
            }
        }
        async void begin(object obj)
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

                            command = values[1];
                            NumberType = values[0];
                            matrixStr = values[3];
                            matrixSize = values[2];
                            string ansString = matrixManager();
                            await writer.WriteAsync(ansString);
                            //Console.WriteLine(/*"Message Recieved: {0}", */ message);
                            break;
                        }
                    }
                }
            }
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