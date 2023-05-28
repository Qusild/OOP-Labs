using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab7
{
    public class MatrixLayout
    {
        public MatrixLayout(List<List<int>> inputScheme) { matrixScheme = inputScheme; }
        public int getElem(int i, int j) { return matrixScheme[i][j]; }
        public int getSize() { return matrixScheme.Count(); }
        private List<List<int>> matrixScheme;
    }
}
