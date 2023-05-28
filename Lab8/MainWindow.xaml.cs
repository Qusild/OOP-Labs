using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Data;

namespace OOPLab7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GraphSlider.IsEnabled = false;
            SimulationButton.IsEnabled = false;
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TextFiles|*.txt";
            Nullable<bool> dialogCompleted = ofd.ShowDialog();
            if (dialogCompleted == true) 
            {
                if ((!(updateThread is null))&&(updateThread.IsAlive))
                    { updateThread.Abort(); }
                String fileName = ofd.FileName;
                FileReader fr = new FileReader();
                gv = new GraphVisualizer(fr.readMatrixDouble(fileName),ref graphCanvas);
                GraphSlider.IsEnabled = true;
                SimulationButton.IsEnabled = true;
                GraphSlider.Maximum = gv.getGraphSize()-1;
                updateThread = new Thread(updateCycle);
                updateThread.IsBackground = true;
                updateThread.Start();
            }
        }
        private void updateCycle()
        {
            while(true)
            {
                Thread.Sleep(1000);
                //gv.changeCurrentElem(, ref graphCanvas);
                int tmp = gv.updateState();
                this.Dispatcher.Invoke(() => { gv.changeCurrentElem(tmp, ref graphCanvas); });
            }
        }
        private void GraphSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            gv.changeCurrentElem((int)GraphSlider.Value, ref graphCanvas);
        }
        private Thread updateThread;
        private GraphVisualizer gv;

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (isOnPause)
                updateThread.Resume(); 
            else updateThread.Suspend();
            isOnPause = !isOnPause;
        }
        private bool isOnPause = false;
    }
}
