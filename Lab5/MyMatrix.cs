using MyNumbers;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyMatrix
{
    public class Matrix<T> where T : IMyNumbers<T>
    {
        public Matrix(int rows, int columns, List<List<T>>? matrixTemp = null)
        {
            setSize(rows, columns);
            setMatrixFromTemp(matrixTemp);
        }
        public void setSize(int x = 3, int y = 3)
        {
            rowCount = x;
            columnCount = y;
        }
        protected void setMatrixFromTemp(List<List<T>>? matrixTemp = null)
        {
            matrixBody = new T[rowCount][];
            rank = -1;
            for (int i = 0; i < rowCount; i++)
            {
                matrixBody[i] = new T[columnCount];
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        if (matrixTemp != null)
                            matrixBody[i][j] = matrixTemp[i][j];
                        else matrixBody[i][j] = default(T);
                    }
                }
            }
        }
        public void Transposition()
        {
            T[][] tmpMatrix = new T[rowCount][];
            for (int i = 0; i<rowCount; i++)
            {
                tmpMatrix[i] = new T[columnCount];
                for (int j = 0; j<columnCount;j++)
                {
                    tmpMatrix[i][j] = matrixBody[i][j];
                }
            }
            if (rowCount != columnCount)
            {
                setSize(columnCount, rowCount);
                setMatrixFromTemp();
            }
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = i; j < columnCount; j++)
                {
                    T tmp = matrixBody[i][j];
                    matrixBody[i][j] = tmpMatrix[j][i];
                    matrixBody[j][i] = tmp;
                }
            }
        }
        private T[][] rref()
        {
            //T[][] matrix = matrixBody; //for copying into matrixBody
            T[][] matrix = new T[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                matrix[i] = new T[columnCount];
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i][j] = matrixBody[i][j];
                    }
                }
            }
            int lead = 0;
            for (int r = 0; r < rowCount; r++)
            {
                if (columnCount <= lead) break;
                int i = r;
                while (matrix[i][lead].isZero())
                {
                    i++;
                    if (i == rowCount)
                    {
                        i = r;
                        lead++;
                        if (columnCount == lead)
                        {
                            lead--;
                            break;
                        }
                    }
                }
                for (int j = 0; j < columnCount; j++)
                {
                    T tmp = matrix[r][j];
                    matrix[r][j] = matrix[i][j];
                    matrix[i][j] = tmp;
                }
                T div = matrix[r][lead];
                if (!div.isZero())
                    for (int j = 0; j < columnCount; j++) matrix[r][j] = matrix[r][j]/div;
                for (int j = 0; j < rowCount; j++)
                {
                    if (j != r)
                    {
                        T sub = matrix[j][lead];
                        for (int k = 0; k < columnCount; k++) matrix[j][k] -= (sub * matrix[r][k]);
                    }
                }
                lead++;
            }
            return matrix;
        }
        public int getRank()
        {
            if (rank != -1)
                return rank;
            T[][] tmpMatrix = rref();
            rank = 0;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (!tmpMatrix[i][j].isZero())
                    {
                        rank++;
                        break;
                    }
                }
            }
            return rank;
        }
        public void printMatrix()
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Console.Write($"{matrixBody[i][j]} ");
                }
                Console.Write("\n");
            }
        }
        public (int, int) getSize() { return (rowCount, columnCount); }

        public T GetElem(int i, int j) { return matrixBody[i][j]; }

        public virtual void SetElem(int i, int j, T value)
        {
            matrixBody[i][j] = value;
            rank = -1;
        }

        public void Resize(int x, int y) 
        {
            rowCount = x;
            columnCount = y;
            rank = -1;
            setMatrixFromTemp();
        }

        protected int rowCount;
        protected int columnCount;
        protected int rank;
        protected T[][] matrixBody;

    }

    public class SquareMatrix<T> : Matrix<T> where T : IMyNumbers<T>
    {
        public SquareMatrix(int rows, List<List<T>>? matrixTemp = null) : base(rows, rows, matrixTemp) { determinant = default(T); }
        private T SubmatrixDeterminant(List<List<T>> subMatrix)
        {
            if (subMatrix.Count == 1)
            {
                return subMatrix[0][0];
                
            }
            T res = default(T);
            for (int i = 0; i < subMatrix.Count; i++)
            {
                List<List<T>> submatrix = new List<List<T>>();
                for (int j = 1; j < subMatrix.Count; j++)
                {
                    List<T> submatrixRow = new List<T>();
                    for (int k = 0; k < subMatrix.Count; k++)
                        if (k != i) submatrixRow.Add(subMatrix[j][k]);
                    submatrix.Add(submatrixRow);
                }
                if (res == default(T))
                {
                    res = SubmatrixDeterminant(submatrix) * (i % 2 == 0 ? subMatrix[0][i] : -subMatrix[0][i]);
                }
                else res += SubmatrixDeterminant(submatrix) * (i % 2 == 0 ? subMatrix[0][i] : -subMatrix[0][i]);
            }
            return res;
        }
        public T GetDeterminant()
        {
            if (determinant != default(T))
                return determinant;
            if (rowCount == 1)
            {
                return determinant = matrixBody[0][0];
            }
            determinant = matrixBody[0][0];
            determinant = determinant - matrixBody[0][0];
            for (int i = 0; i < rowCount; i++)
            {
                List<List<T>> submatrix = new List<List<T>>();
                for (int j = 1; j < rowCount; j++)
                {
                    List<T> submatrixRow = new List<T>();
                    for (int k = 0; k < rowCount; k++)
                        if (k != i) submatrixRow.Add(matrixBody[j][k]);
                    submatrix.Add(submatrixRow);
                }
                if (determinant == default(T))
                    determinant = SubmatrixDeterminant(submatrix) * (i % 2 == 0 ? matrixBody[0][i] : -matrixBody[0][i]);
                else determinant += SubmatrixDeterminant(submatrix) * (i % 2 == 0 ? matrixBody[0][i] : -matrixBody[0][i]);
            }
            return determinant;
        }
        public void SetSize(int x)
        {
            Resize(x, x);
            determinant = default(T);
        }
        public int GetSize() { return rowCount; }
        public override void SetElem(int i, int j, T value)
        {
            base.SetElem(i, j, value);
            determinant = default(T);
        }
        public void SetMatrix(int size, List<List<T>> matrix)
        {
            SetSize(size);
            setMatrixFromTemp(matrix);
        }
        private T determinant;
    }
}