using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace OOPLab7
{
    public class FileReader
    {
        public FileReader() { }
        public List<List<int>> readMatrix(string filename) 
        {
            List<string> matrix = File.ReadAllLines(filename).ToList();
            List<List<int>> connectionMatrix = new List<List<int>>();
            if (matrix[0][0] != 'I')
            {
                MessageBox.Show("ERROR: Wrong Matrix File");
                return null;
            }
            for (int i = 1; i < matrix.Count; i++)
            {
                String[] row = matrix[i].Split(' ');
                if (row[0] != matrix[0].Split(' ')[i])
                {
                    MessageBox.Show("ERROR: Wrong Matrix File");
                    return null;
                }                    
                if (row.Count()!= matrix.Count)
                {
                    MessageBox.Show("ERROR: Wrong Matrix File");
                    return null;
                }
                connectionMatrix.Add(new List<int>());
                for (int j = 1; j < row.Count();j++)
                {
                    int tmp = Convert.ToInt32(row[j]);
                    if ((tmp != 0) && (tmp != 1))
                    {
                        MessageBox.Show("ERROR: Wrong Matrix File");
                        return null;
                    }
                    connectionMatrix[i-1].Add(tmp);
                }
            }
            MessageBox.Show("Matrix Read Successfully");
            return connectionMatrix;
        }
        public List<List<double>> readMatrixDouble(string filename)
        {
            List<string> matrix = File.ReadAllLines(filename).ToList();
            List<List<double>> connectionMatrix = new List<List<double>>();
            if (matrix[0][0] != 'I')
            {
                MessageBox.Show("ERROR: Wrong Matrix File");
                return null;
            }
            for (int i = 1; i < matrix.Count; i++)
            {
                String[] row = matrix[i].Split(' ');
                if (row[0] != matrix[0].Split(' ')[i])
                {
                    MessageBox.Show("ERROR: Wrong Matrix File");
                    return null;
                }
                if (row.Count() != matrix.Count)
                {
                    MessageBox.Show("ERROR: Wrong Matrix File");
                    return null;
                }
                connectionMatrix.Add(new List<double>());
                for (int j = 1; j < row.Count(); j++)
                {
                    double tmp = Convert.ToDouble(row[j]);
                    /*if ((tmp != 0) && (tmp != 1))
                    {
                        MessageBox.Show("ERROR: Wrong Matrix File");
                        return null;
                    }*/
                    connectionMatrix[i - 1].Add(tmp);
                }
            }
            MessageBox.Show("Matrix Read Successfully");
            return connectionMatrix;
        }
    }
}
