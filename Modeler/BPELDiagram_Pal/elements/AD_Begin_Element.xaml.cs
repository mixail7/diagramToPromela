using System.Windows;
using System.Xml.Linq;
using DiagramDesigner;

namespace BPELDiagram_Pal
{
    public partial class AD_Begin_Element : IXMLSaveable, IBpelElement
    {
        public bool isEndElement { get; set; }
        public const string ElementName = "AD_Begin_Element";
        public AD_Begin_Element()
        {
            InitializeComponent();
            DataContext = this;
        }
        public XElement getData()
        {
            var res = new XElement("Content_UserXML");
            res.Add(new XElement("Name", ElementName));
            return res;
        }

        public void loadData(XElement data)
        {
        }

        public bool isDeletedConnect()
        {
            return true;
        }
        
        public void addAutoConnect(DesignerCanvas ds, DesignerItem di)
        {
            /*var res = new DesignerItem {Content = new AD_End_Element(), CanResize = false, CanRotate = false};
            res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
            ds.addDesignerItem(res, 0, 0);
            ds.addConnection(new Connection(di.Connectors[0], res.Connectors[0], true));
            ds.autoPozitions();*/
        }

    }
}
