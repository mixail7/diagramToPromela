using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using DiagramDesigner;
using Modeler;

namespace dataLibrary_Pal
{
    public class DataResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DataResource()
        {
            Id = Guid.NewGuid();
        }
        public DataResource(Guid id, string name, string link)
        {
            Id = id;
            Name = name;
            Link = link;
        }
    }
    /// <summary>
    /// Interaction logic for DataElement.xaml
    /// </summary>
    public partial class DataElement : UserControl, INotifyPropertyChanged, IBlockElementInterface
    {
        public DataResource Ts = new DataResource();
        public MainWindow MainWindow;

        public DataElement(object Tag)
        {
            MainWindow = (MainWindow) Tag;
            InitializeComponent();
            DataContext = this;
            BorderBrush = new SolidColorBrush(BorderColor);
        }

        public DataElement()
        {
            MainWindow = null;
            InitializeComponent();
            DataContext = this;
            BorderBrush = new SolidColorBrush(BorderColor);
        }
        
        private void setMainWindow()
        {
            if (MainWindow == null)
                MainWindow = (MainWindow)this.Tag;
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

        public const string ElementName = "DataElement";

        public event PropertyChangedEventHandler PropertyChanged;

        public XElement getData()
        {
            var serializer = new XmlSerializer(Ts.GetType());
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, Ts);
                TextValue = writer.ToString();
            }
            return this.getXmlDescription();
        }

        public void loadData(XElement data)
        {
            this.loadFromXmlDescription(data);
            var serializer = new XmlSerializer(typeof(DataResource));
            using (var reader = new StringReader(TextValue))
            {
                Ts = (DataResource)serializer.Deserialize(reader);
                DataGrid1.Items.Add(Ts);
            }
        }


        public object getCover(DesignerItem item)
        {
            return new BlockElement_ControlCoverForPropertyEditor(item);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                setMainWindow();
                EditData ts;
                if (DataGrid1.Items.Count == 1)
                {
                    Ts = (DataResource)DataGrid1.Items[0];
                    ts = new EditData(Ts.Id, Ts.Name, Ts.Link, MainWindow);
                }
                else
                {
                    ts = new EditData(MainWindow);
                }
                ts.ShowDialog();
                if (ts.DialogResult == true)
                {

                    if (DataGrid1.Items.Count == 1)
                    {
                        Ts = new DataResource(ts.Id, ts.Nme, ts.Skills);
                    }
                    else
                    {
                        Ts = new DataResource { Name = ts.Nme, Link = ts.Skills };
                    }
                    DataGrid1.Items.Clear();
                    DataGrid1.Items.Add(Ts);
                }
            }
        }
    }


}
