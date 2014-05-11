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
    public partial class EditAction : Window
    {
        public EditAction(Guid id, string name, string model, MainWindow mv)
        {
            InitializeComponent();
            MainWindow = mv;
            textBox1.Text = id.ToString();
            Id = id;
            loadModelFiles();
        }
        public EditAction(MainWindow mv)
        {
            MainWindow = mv;
            InitializeComponent();
            loadModelFiles();
        }

        public string Name, Model;
        public MainWindow MainWindow;
        public List<string> Paths = new List<string>();
        public Guid Id;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Name = ComboBox2.SelectedItem.ToString();
            Model = ComboBox1.SelectedItem.ToString();
            int start = Name.IndexOf('('), end = Name.IndexOf(')');

            Id = Guid.Parse(Name.Substring(start + 1, end - start -1));
            DialogResult = true;
            Close();
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
                if (Path.GetExtension(entrie) == ".modelcf")
                {
                    ComboBox1.Items.Add(Path.GetFileNameWithoutExtension(entrie));
                    Paths.Add(entrie);
                }
            }
        }

        private void ComboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //MessageBox.Show(Paths[ComboBox1.SelectedIndex]);
            var arr = getElementTitesFromModel("BPELDiagram_Pal.", Paths[ComboBox1.SelectedIndex]);
            arr.AddRange(getElementTitesFromModel("ActyvityDiagram_Pal.", Paths[ComboBox1.SelectedIndex]));
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
                if (tp.StartsWith(type_elem))
                {
                    string id = item.DescendantsAndSelf("ID").First().Value;
                    try
                    {
                        arr.Add(
                            item.DescendantsAndSelf("Content_UserXML").First().DescendantsAndSelf("Text").First().Value +
                            "(" + id + ")");
                    }
                    catch (Exception)
                    {
                        arr.Add(item.DescendantsAndSelf("Content_UserXML").First().DescendantsAndSelf("Name").First().Value +
                            "(" + id + ")");
                    }
                    
                }
            }
            return arr;
        }
    }
}
