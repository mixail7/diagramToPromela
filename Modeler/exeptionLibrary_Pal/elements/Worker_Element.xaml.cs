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
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Skills { get; set; }
        public Resource()
        {
            Id = Guid.NewGuid();
        }
        public Resource(Guid id, string name, string skills)
        {
            Id = id;
            Name = name;
            Skills = skills;
        }
    }
    /// <summary>
    /// Interaction logic for WorkerElement.xaml
    /// </summary>
    public partial class WorkerElement : UserControl, INotifyPropertyChanged, IBlockElementInterface
    {
        public Resource Ts = new Resource();
        public MainWindow MainWindow;

        public WorkerElement(object Tag)
        {
            MainWindow = (MainWindow) Tag;
            InitializeComponent();
            DataContext = this;
            BorderBrush = new SolidColorBrush(BorderColor);
        }

        public WorkerElement()
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

        public const string ElementName = "Worker_Element";

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
            var serializer = new XmlSerializer(typeof(Resource));
            using (var reader = new StringReader(TextValue))
            {
                Ts = (Resource)serializer.Deserialize(reader);
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
                EditResources ts;
                if (DataGrid1.Items.Count == 1)
                {
                    Ts = (Resource)DataGrid1.Items[0];
                    ts = new EditResources(Ts.Id, Ts.Name, Ts.Skills, MainWindow);
                }
                else
                {
                    ts = new EditResources(MainWindow);
                }
                ts.ShowDialog();
                if (ts.DialogResult == true)
                {

                    if (DataGrid1.Items.Count == 1)
                    {
                        Ts = new Resource(ts.Id, ts.Nme, ts.Skills);
                    }
                    else
                    {
                        Ts = new Resource { Name = ts.Nme, Skills = ts.Skills };
                    }
                    DataGrid1.Items.Clear();
                    DataGrid1.Items.Add(Ts);
                }
            }
        }
    }


}
