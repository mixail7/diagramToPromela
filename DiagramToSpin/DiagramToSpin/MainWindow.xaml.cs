using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MainLib;
using MessageBox = System.Windows.MessageBox;

namespace DiagramToSpin
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                browse_str.Text = fbd.SelectedPath;
            }
        }

        public string outCode = "";
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show(browse_str.Text);
            MainHelper.InitCounters();
            string outcode = "";
            DiagramsToPromela dp = new DiagramsToPromela(browse_str.Text,out outcode);
            outCode = outcode;
            MessageBox.Show(outcode);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string res = "";
            bool ret = SpinExecutor.RunPromelaCode(outCode, ref res);
            MessageBox.Show(ret + "\r\n" + res);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string res = "";
            bool ret = SpinExecutor.CheckPromelaCode(outCode, ref res);
            MessageBox.Show(ret + "\r\n" + res);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string res = "";
            bool ret = SpinExecutor.ContrExamplePromelaCode(outCode, ref res);
            MessageBox.Show(ret + "\r\n" + res);
        }

    }
}
