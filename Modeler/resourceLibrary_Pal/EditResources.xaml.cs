using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using AvalonDock.Layout;
using Modeler;

namespace dataLibrary_Pal
{
    /// <summary>
    /// Логика взаимодействия для EditResources.xaml
    /// </summary>
    public partial class EditResources : Window
    {
        public EditResources(Guid id, string name, string skills, MainWindow mv)
        {
            InitializeComponent();
            MainWindow = mv;
            textBox1.Text = id.ToString();
            Id = id;
            textBox2.Text = name;
            textBlock1.Text = skills;
            loadModelFiles();
        }
        public EditResources(MainWindow mv)
        {
            MainWindow = mv;
            InitializeComponent();
            loadModelFiles();
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox1.SelectedIndex != -1 && ComboBox2.SelectedIndex != -1)
            {



                if (textBlock1.Text.Length == 0)
                {
                    textBlock1.Text += "";
                }
                else
                {
                    textBlock1.Text += "\n";
                }
                textBlock1.Text += ComboBox1.SelectedValue + ":" + ComboBox2.SelectedValue;
            }
            else
            {
                MessageBox.Show("Навык не может буть пустым!");
            }
        }

        private void loadModelFiles()
        {
            var entries = Directory.GetFileSystemEntries(Path.GetDirectoryName(MainWindow.current_model));
            foreach (var entrie in entries)
            {
                if (Directory.Exists(entrie))
                {
                    continue;
                }
                if (Path.GetExtension(entrie) == ".model")
                {
                    ComboBox1.Items.Add(Path.GetFileNameWithoutExtension(entrie));
                    Paths.Add(entrie);
                }
            }
        }

        private void ComboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //MessageBox.Show(Paths[ComboBox1.SelectedIndex]);
            var arr = getElementTitesFromModel("SkillsTree.WorkerElement", Paths[ComboBox1.SelectedIndex]);
            ComboBox2.IsEnabled = true;
            ComboBox2.Items.Clear();
            foreach (var str in arr)
            {
                ComboBox2.Items.Add(str);
            }
        }

        private static string GetAttributeValue(string attributeName, XElement element)
        {
            XAttribute xAttribute = element.Attribute(attributeName);

            string value = string.Empty;
            if (xAttribute != null)
            {
                value = xAttribute.Value;
            }

            return value;
        }

        public List<string> getElementTitesFromModel(string type_elem, string file)
        {
            var arr = new List<string>();
            var buf = File.ReadAllText(file);
            const string tag = "<Root>", close_tag = "</Root>";
            var start = buf.IndexOf(tag, StringComparison.CurrentCultureIgnoreCase);
            var len = buf.IndexOf(close_tag, StringComparison.CurrentCultureIgnoreCase) - start + close_tag.Length;
            buf = buf.Substring(start, len);
            var xmlTree = XElement.Parse(buf);
            var items = xmlTree.DescendantsAndSelf("DesignerItem");
            foreach (var item in items)
            {

                string tp = GetAttributeValue("ObjectType",
                                             item.DescendantsAndSelf("Content_UserXML").First());
                if (tp == type_elem)
                {
                    arr.Add(item.DescendantsAndSelf("Content_UserXML").First().DescendantsAndSelf("Text").First().Value);
                }
            }
            return arr;
        }
    }
}
