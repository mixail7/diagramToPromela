﻿using System.Xml.Linq;
using DiagramDesigner;

namespace BPELDiagram_Pal
{
    public partial class AD_Division3_Element : IXMLSaveable
    {
        public const string ElementName = "AD_Division3_Element";
        public AD_Division3_Element()
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
    }
}
