using System;
using System.Collections.Generic;
using number = MyNumber.Rational;


namespace MyMath
{
    public class SquareMatrix
    {
        public SquareMatrix(int size = 3, number[][]? Matrix = null)
        {
            if (Matrix != null)
            {
                if ((size == Matrix.GetLength(0)) && (size == Matrix.GetLength(1)))
                    MatrixConstructor(size, Matrix);
                else
                {
                    Console.WriteLine("ERROR: Array size != Array dimensions");
                    Console.WriteLine("Default Matrix was created instead");
                    MatrixConstructor();
                }
            }
            else MatrixConstructor(size);
        }

        private void MatrixConstructor(int size = 3, number[][]? Matrix = null)
        {
            if (Matrix == null)
            {
                this.size = size;
                this.rank = -1;
                this.determinant = new number(0,1);
                //this.determinant = 0;
                this.Matrix = new number[size][];
                for (int i = 0; i < size; i++)
                {
                    this.Matrix[i] = new number[size];
                    for (int j = 0; j < size; j++)
                        this.Matrix[i][j] = new number(0, 1);
                        //this.Matrix[i][j] = 0;
                    this.Matrix[i][i] = new number(1,1);
                    //this.Matrix[i][i] = 1;

                }
            }
            else
            {
                this.size = size;
                this.Matrix = Matrix;
                this.rank = -1;
                this.determinant = new number(0,1);
                //this.determinant = 0;
            }
        }

        public void SetMatrix(int size, List<List<number>> Matrix)
        {
            if ((size == Matrix.Count) && (size != 0) && (size == Matrix[0].Count))
            {
                number[][] tmpMatrix = Matrix.Select(x => x.ToArray()).ToArray();
                MatrixConstructor(size, tmpMatrix);
            }
            else
            {
                Console.WriteLine("ERROR: Array size != Array dimensions");
                Console.WriteLine("Default Matrix was created instead");
                MatrixConstructor();
            }
        }

        public void Transposition()
        {
            number[][] Result = new number[size][];
            for (int i = 0; i < size; i++)
                Result[i] = new number[size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Result[j][i] = Matrix[i][j];
                }
            }
            Matrix = Result;
        }

        private number SubmatrixDeterminant(List<List<number>> subMatrix)
        {
            if (subMatrix.Count == 1)
            {
                if ((subMatrix[0][0] != 0) && (1 > rank))
                    rank = 1;
                return subMatrix[0][0];
            }
            number res = new number(0,1);
            //number res = 0;
            for (int i = 0; i < subMatrix.Count; i++)
            {
                List<List<number>> submatrix = new List<List<number>>();
                for (int j = 1; j < subMatrix.Count; j++)
                {
                    List<number> submatrixRow = new List<number>();
                    for (int k = 0; k < subMatrix.Count; k++)
                        if (k != i) submatrixRow.Add(subMatrix[j][k]);
                    submatrix.Add(submatrixRow);
                }
                res += SubmatrixDeterminant(submatrix) * (i % 2 == 0 ? subMatrix[0][i] : -subMatrix[0][i]);
            }
            if ((res != 0) && (subMatrix.Count > rank))
                rank = subMatrix.Count;
            return res;
        }

        public number GetDeterminant()
        {
            if (determinant != 0)
                return determinant;
            //determinant = new number(1,0);
            if (size == 1)
            {
                if (Matrix[0][0] != 0)
                    rank = 1;
                else rank = 0;
                return Matrix[0][0];
            }
            for (int i = 0; i < size; i++)
            {
                List<List<number>> submatrix = new List<List<number>>();
                for (int j = 1; j < size; j++)
                {
                    List<number> submatrixRow = new List<number>();
                    for (int k = 0; k < size; k++)
                        if (k != i) submatrixRow.Add(Matrix[j][k]);
                    submatrix.Add(submatrixRow);
                }
                determinant += SubmatrixDeterminant(submatrix) * (i % 2 == 0 ? Matrix[0][i] : -Matrix[0][i]);

            }
            //determinant = determinant.round();
            //determinant = Math.Round(determinant, 12, MidpointRounding.AwayFromZero);
            if (determinant != 0)
                rank = size;
            else if (rank == -1)
                rank = 0;
            return determinant;
        }
        public void PrintMatrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(Matrix[i][j].ToString()+" ");
                Console.WriteLine("");
            }
        }

        public int GetRank()
        {
            if (rank == -1)
                GetDeterminant();
            return rank;
        }

        public int GetSize()
        {
            return size;
        }

        public number GetElem(int i, int j)
        {
            return Matrix[i][j];
        }

        public void SetElem(int i, int j, number value)
        {
            Matrix[i][j] = value;
            determinant = new number(0, 1);
            rank = -1;
        }

        public void SetSize(int newSize)
        {
            size = newSize;
            number[][] newMatrix = new number[size][];
            for (int i = 0; i < size; i++)
                newMatrix[i] = new number[size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                        newMatrix[i][j] = new number(0, 1);
                    else newMatrix[i][j] = new number(1, 1);
                }
            for (int i = 0; i < Matrix[0].Length;i++)
                for (int j = 0; j < Matrix[i].Length;j++)
                    if ((i<size)&&(j<size))
                        newMatrix[i][j] = Matrix[i][j];
            Matrix = newMatrix;
            determinant = new number(0,1);
            rank = -1;
        }
        private number[][] Matrix;
        private int size;
        private int rank;
        private number determinant;
    }
}