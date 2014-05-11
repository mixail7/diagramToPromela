using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using AvalonDock.Layout;
using Microsoft.Win32;
using Modeler;

namespace dataLibrary_Pal
{
    /// <summary>
    /// Логика взаимодействия для EditAction.xaml
    /// </summary>
    public partial class EditData : Window
    {
        public EditData(Guid id, string name, string link, MainWindow mv)
        {
            InitializeComponent();
            MainWindow = mv;
            textBox1.Text = id.ToString();
            Id = id;
            textBox2.Text = name;
            textBlock1.Text = link;
        }
        public EditData(MainWindow mv)
        {
            MainWindow = mv;
            InitializeComponent();
            
        }

        public string Nme, Skills;
        public MainWindow MainWindow;
        public List<string> Paths = new List<string>();
        public Guid Id;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Поля не могут быть пустыми");
                return;
            }
            Nme = textBox2.Text;
            Skills = textBlock1.Text;
            DialogResult = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var od = new OpenFileDialog();
            od.Filter = "All files (*.*)|*.*";
            od.FilterIndex = 2;
            od.RestoreDirectory = true;

            if (od.ShowDialog() == true)
            {
                textBlock1.Text = od.FileName;
            }
        }
    }
}
