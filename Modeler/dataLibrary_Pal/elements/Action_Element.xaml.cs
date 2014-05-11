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
    public class ActionResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public ActionResource()
        {
            Id = Guid.NewGuid();
        }
        public ActionResource(Guid id, string name, string skills)
        {
            Id = id;
            Name = name;
            Model = skills;
        }
    }
    /// <summary>
    /// Interaction logic for ActionElement.xaml
    /// </summary>
    public partial class ActionElement : UserControl, INotifyPropertyChanged, IBlockElementInterface
    {
        public ActionResource Ts = new ActionResource();
        public MainWindow MainWindow;

        public ActionElement(object Tag)
        {
            MainWindow = (MainWindow) Tag;
            InitializeComponent();
            DataContext = this;
            BorderBrush = new SolidColorBrush(BorderColor);
        }

        public ActionElement()
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

        public const string ElementName = "ActionElement";

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
            var serializer = new XmlSerializer(typeof(ActionResource));
            using (var reader = new StringReader(TextValue))
            {
                Ts = (ActionResource)serializer.Deserialize(reader);
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
                EditAction ts;
                if (DataGrid1.Items.Count == 1)
                {
                    Ts = (ActionResource)DataGrid1.Items[0];
                    ts = new EditAction(Ts.Id, Ts.Name, Ts.Model, MainWindow);
                }
                else
                {
                    ts = new EditAction(MainWindow);
                }
                ts.ShowDialog();
                if (ts.DialogResult == true)
                {

                    Ts = new ActionResource(ts.Id, ts.Name, ts.Model);
                    
                    DataGrid1.Items.Clear();
                    DataGrid1.Items.Add(Ts);
                }
            }
        }
    }


}
