using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab7
{
    public class OrientedGraph
    {
        public OrientedGraph(List<List<int>> inputView) { matrixView = new MatrixLayout(inputView); }
        public int getSize() { return matrixView.getSize(); }
        public int getElem(int i, int j) { return matrixView.getElem(i, j); }
        public List<int> getAllOutputs(int i)
        {
            List<int> outputs = new List<int>();
            for (int j = 0; j< matrixView.getSize(); j++)
            {
                if (matrixView.getElem(i,j) == 1)
                    outputs.Add(j);
            }
            return outputs;
        }
        protected OrientedGraph() { }
        protected MatrixLayout matrixView;
    }
}
