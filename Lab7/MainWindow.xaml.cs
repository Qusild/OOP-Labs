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
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TextFiles|*.txt";
            Nullable<bool> dialogCompleted = ofd.ShowDialog();
            if (dialogCompleted == true) 
            {
                String fileName = ofd.FileName;
                FileReader fr = new FileReader();
                GraphVisualizer gv = new GraphVisualizer(fr.readMatrix(fileName),ref graphCanvas);
            }
        }
    }
}
