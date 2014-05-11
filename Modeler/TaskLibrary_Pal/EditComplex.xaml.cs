using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TaskLibrary_Pal
{
    /// <summary>
    /// Логика взаимодействия для EditComplex.xaml
    /// </summary>
    public partial class EditComplex : Window
    {
        public EditComplex(string base_path)
        {
            InitializeComponent();
            basePath = base_path;
        }

        public EditComplex(string base_path, Guid id, string name, string link)
        {
            InitializeComponent();
            textBox1.Text = id.ToString();
            Id = id;
            textBox2.Text = name;
            textBox3.Text = link;
            basePath = base_path;
        }

        private string basePath;
        public string Nme, Link;
        public Guid Id;
        public bool res;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
            {
                MessageBox.Show("Поля не могут быть пустыми");
                return;
            }
            Nme = textBox2.Text;
            Link = textBox3.Text;
            res = true;
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            var dlg = new Microsoft.Win32.OpenFileDialog
                {
                    DefaultExt = ".model",
                    Filter = "Task models (.model)|*.model",
                    InitialDirectory = basePath
                };

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                int poz = filename.IndexOf(basePath);
                if (poz < 0)
                {
                    MessageBox.Show("Сылки могут быть только на модели внутри проекта");
                    return;
                }
                textBox3.Text = filename.Substring(basePath.Length);

                //FileNameTextBox.Text = filename;
            }
        }
    }
}
