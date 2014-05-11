using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DiagramDesigner;
using Modeler;

namespace TaskLibrary_Pal
{
    public class ComplexTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        
        public ComplexTask()
        {
            Id = Guid.NewGuid();
            Name = Link = "";
        }
        
        public ComplexTask(Guid id, string name, string link)
        {
            Id = id;
            Name = name;
            Link = link;
        }
    }
    /// <summary>
    /// Interaction logic for ComplexTaskElement.xaml
    /// </summary>
    public partial class ComplexTaskElement : UserControl, INotifyPropertyChanged, IBlockElementInterface
    {
        public ComplexTask Ts = new ComplexTask();
        public MainWindow MainWindow;
        
        public ComplexTaskElement()
        {
            MainWindow = (MainWindow)null;
            InitializeComponent();
            DataContext = this;
            BorderBrush = new SolidColorBrush(BorderColor);

        }

        public ComplexTaskElement(object tag)
        {
            MainWindow = (MainWindow)tag;
            InitializeComponent();
            DataContext = this;
            BorderBrush = new SolidColorBrush(BorderColor);

        }

        public void setMainWindow()
        {
            if (MainWindow == null)
                MainWindow = (MainWindow) this.Tag;
        }

        private HatchType _hatchAngle = HatchType.НетШтриховки;
        /// <summary>
        /// угол, под которым направлена штриховка
        /// </summary>
        public HatchType Hatch
        {
            get
            {
                return _hatchAngle;
            }
            set
            {
                _hatchAngle = value;
                if (value == HatchType.Наклон_0)
                {
                    MainBorder.Background = Block_Extentions.getBrush(0);
                }
                else if (value == HatchType.Наклон_45)
                {
                    MainBorder.Background = Block_Extentions.getBrush(45);
                }
                else if (value == HatchType.Наклон_90)
                {
                    MainBorder.Background = Block_Extentions.getBrush(90);
                }
                else if (value == HatchType.Наклон_135)
                {
                    MainBorder.Background = Block_Extentions.getBrush(135);
                }
                else
                {
                    MainBorder.Background = null;
                }
                OnPropertyChanged("HatchAngle");
            }
        }

        private bool _isBorderVisible = true;

        public bool IsBorderVisible
        {
            get
            {
                return _isBorderVisible;
            }
            set
            {
                _isBorderVisible = value;
                if (IsBorderVisible == false)
                {
                    BorderBrush = new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    BorderBrush = new SolidColorBrush(BorderColor);
                }
            }
        }

        private Color _colorBrush = Colors.Black;
        public Color BorderColor
        {
            get
            {
                if (IsBorderVisible == false)
                {
                    return Colors.Transparent;
                }
                return _colorBrush;
            }
            set
            {
                _colorBrush = value;
                if (IsBorderVisible == false)
                {
                    return;
                }
                BorderBrush = new SolidColorBrush(value);
            }
        }

        private string _textValue = "Действие";

        public string TextValue
        {
            get
            {
                return _textValue;
            }
            set
            {
                _textValue = value;
                OnPropertyChanged("TextValue");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public const string ElementName = "ComplexTaskElement";

        public event PropertyChangedEventHandler PropertyChanged;

        public XElement getData()
        {
            var serializer = new XmlSerializer(Ts.GetType());
            var settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false, false); // no BOM in a .NET string
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;
            using (var writer = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer, settings))
                {
                    serializer.Serialize(xmlWriter, Ts);
                }
                writer.Flush();
                TextValue = writer.ToString();
            }
            return this.getXmlDescription();
        }

        public void loadData(XElement data)
        {
            try
            {
                this.loadFromXmlDescription(data);
                var serializer = new XmlSerializer(typeof (ComplexTask));
                XmlReaderSettings settings = new XmlReaderSettings();
                using (var reader = new StringReader(TextValue))
                {
                    using (XmlReader xmlReader = XmlReader.Create(reader, settings))
                    {
                        Ts = (ComplexTask) serializer.Deserialize(xmlReader);
                        DataGrid1.Items.Add(Ts);
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public object getCover(DesignerItem item)
        {
            return new BlockElement_ControlCoverForPropertyEditor(item);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            setMainWindow();
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                EditComplex ts;
                if (DataGrid1.Items.Count == 1)
                {
                    Ts = (ComplexTask)DataGrid1.Items[0];
                    ts = new EditComplex(MainWindow._project.FileModelPath, Ts.Id, Ts.Name, Ts.Link);
                }
                else
                {
                    ts = new EditComplex(MainWindow._project.FileModelPath);
                }

                ts.ShowDialog();
                if (ts.res)
                {
                    if (DataGrid1.Items.Count == 1)
                    {
                        Ts = new ComplexTask(ts.Id, ts.Nme, ts.Link);
                    }
                    else
                    {
                        Ts = new ComplexTask { Name = ts.Nme, Link = ts.Link};
                    }
                    DataGrid1.Items.Clear();
                    DataGrid1.Items.Add(Ts);
                }
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                var Ts = (ComplexTask)DataGrid1.Items[0];
                MainWindow.OpenModel(MainWindow._project.FileModelPath + Ts.Link);
            }
        }
    }


}
