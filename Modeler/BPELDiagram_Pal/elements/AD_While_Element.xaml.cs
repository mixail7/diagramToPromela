﻿using System.ComponentModel;
using System.Windows.Media;
using System.Xml.Linq;
using DiagramDesigner;
using Modeler;

namespace BPELDiagram_Pal
{
    public partial class AD_While_Element : INotifyPropertyChanged, IBlockElementInterface, IBpelElement
    {
        public bool isEndElement { get; set; }

        public bool isDeletedConnect()
        {
            return true;
        }

        public void addAutoConnect(DesignerCanvas ds, DesignerItem di)
        {
            /*var res = new DesignerItem { Content = new AD_End_While_Element(), CanResize = false, CanRotate = false };
            (res.Content as IBpelElement).isEndElement = true;
            res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
            ds.addDesignerItem(res, 0, 0);
            ds.addConnection(new Connection(di.Connectors[0], res.Connectors[0], true));
            ds.replaceAllLinks(di, res);
            ds.autoPozitions();*/
        }

        public AD_While_Element()
        {
            isEndElement = false;
            InitializeComponent();
            DataContext = this;
            BorderBrush = new SolidColorBrush(BorderColor);
        }

        private HatchType _hatchAngle = HatchType.НетШтриховки;
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
                    MainBorder.Fill = Block_Extentions.getBrush(0);
                }
                else if (value == HatchType.Наклон_45)
                {
                    MainBorder.Fill = Block_Extentions.getBrush(45);
                }
                else if (value == HatchType.Наклон_90)
                {
                    MainBorder.Fill = Block_Extentions.getBrush(90);
                }
                else if (value == HatchType.Наклон_135)
                {
                    MainBorder.Fill = Block_Extentions.getBrush(135);
                }
                else
                {
                    MainBorder.Fill = null;
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

        private string _textValue = "while";

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

        public const string ElementName = "AD_While_Element";

        public event PropertyChangedEventHandler PropertyChanged;

        public XElement getData()
        {
            return this.getXmlDescription();
        }

        public void loadData(XElement data)
        {
            this.loadFromXmlDescription(data);
        }

        public object getCover(DesignerItem item)
        {
            return new BlockElement_ControlCoverForPropertyEditor(item);
        }
    }

}
