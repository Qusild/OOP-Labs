using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLab7
{
    public class StateGraph : OrientedGraph
    {
        public StateGraph(List<List<double>> inputView) : base()
        {
            List<List<int>> intInput = new List<List<int>>();
            prob = new List<List<double>>();
            for (int i = 0;i <inputView.Count;i++)
            {
                prob.Add(new List<double>());
                intInput.Add(new List<int>());
                for (int j = 0; j < inputView.Count; j++)
                {
                    if (inputView[i][j] > 0)
                    {
                        prob[i].Add(inputView[i][j]);
                        intInput[i].Add(1);
                    }
                    else intInput[i].Add(0);
                }
                    

            }
            matrixView = new MatrixLayout(intInput);
            random = new Random();
        }
        public int updateState(int currentState)
        {
            double stateTranscation = random.NextDouble();
            int newStatement = -1;
            for (int i = 0; i < prob[currentState].Count;i++)
            {
                if (stateTranscation > prob[currentState][i])
                    stateTranscation -= prob[currentState][i];
                else  { newStatement = i; break; }
            }
            return getAllOutputs(currentState)[newStatement];
        }

        private Random random;
        private List<List<double>> prob;
    }
}
