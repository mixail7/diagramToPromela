using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DiagramDesigner;

namespace BPELDiagram_Pal
{
    /// <summary>
    /// класс, который отвечает за палитру компонентов
    /// </summary>
    [Serializable]
    public class BlockToolBoxPalleter : IToolBoxPalleter
    {

        public const string blockFormatName = "BPELDiagram";

        public string[] getNames()
        {
            return new string[]{AD_Begin_Element.ElementName, AD_Action_Element.ElementName,
                                AD_Condition_Element.ElementName, 
                                AD_End_Element.ElementName, AD_EndThread_Element.ElementName,
                                AD_Thread_Element.ElementName, AD_While_Element.ElementName, AD_End_While_Element.ElementName,
                                AD_End_Condition_Element.ElementName};
        }

        public UserControl[] getControls(params string[] names)
        {
            var res = new List<UserControl>();
            if (names.Contains(AD_Begin_Element.ElementName))
                res.Add(new AD_Begin_ToolBox());
            if (names.Contains(AD_Action_Element.ElementName))
                res.Add(new AD_Action_ToolBox());
            if (names.Contains(AD_Condition_Element.ElementName))
                res.Add(new AD_Condition_ToolBox());
            if (names.Contains(AD_Thread_Element.ElementName))
                res.Add(new AD_Thread_ToolBox());
            if (names.Contains(AD_End_Element.ElementName))            
               res.Add(new AD_End_ToolBox());
            if (names.Contains(AD_EndThread_Element.ElementName))
                res.Add(new AD_EndThread_ToolBox());
            if (names.Contains(AD_While_Element.ElementName))
                res.Add(new AD_While_ToolBox());
            if (names.Contains(AD_End_While_Element.ElementName))
                res.Add(new AD_End_While_ToolBox());
            if (names.Contains(AD_End_Condition_Element.ElementName))
                res.Add(new AD_End_Condition_Element());
            return res.ToArray();
        }

        public string getPalleterName()
        {
            return "Модель потока(BPEL diagram)";
        }

        public Expander getPanel()
        {
            var toolBox = new WrapPanel();
            
            toolBox.Orientation = Orientation.Horizontal;

            toolBox.ItemHeight = 80;
            toolBox.ItemWidth = 80;
            toolBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            toolBox.VerticalAlignment = VerticalAlignment.Stretch;
            toolBox.Children.Add(new AD_Begin_ToolBox());            
            toolBox.Children.Add(new AD_Action_ToolBox());
            toolBox.Children.Add(new AD_Condition_ToolBox());
            toolBox.Children.Add(new AD_Thread_ToolBox());
            toolBox.Children.Add(new AD_End_ToolBox());
            toolBox.Children.Add(new AD_EndThread_ToolBox());
            toolBox.Children.Add(new AD_While_ToolBox());
            toolBox.Children.Add(new AD_End_While_ToolBox());
            toolBox.Children.Add(new AD_End_Condition_ToolBox());
            var res = new Expander { Header = getPalleterName(), IsExpanded = true, Content = toolBox };
            return res;
        }

        public bool isFormatOwner(string formatName)
        {
            return (formatName == blockFormatName);
        }

        public DesignerItem getElement(DragObject data)
        {
            string element_name = (string)data.data;
            if (element_name == AD_Begin_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_Begin_Element() };
                res.CanResize = false;
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                return res;
            }
            if (element_name == AD_Action_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_Action_Element() };
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                /*res.Connectors = new Connector[] {new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top"),
                                                  new Connector(ConnectorOrientation.Bottom, res, 0.5, 1, "Bottom")};*/
                return res;
            }
            if (element_name == AD_Condition_Element.ElementName)
            {                
                var res = new DesignerItem { Content = new AD_Condition_Element() };
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                /*res.Connectors = new Connector[] {new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top"),
                                                  new Connector(ConnectorOrientation.Left, res, 0, 0.5, "Left"),
                                                  new Connector(ConnectorOrientation.Right, res,1,  0.5, "Right")};*/
                return res;
            }
            
            
            if (element_name == AD_End_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_End_Element() };                
                res.CanResize = false;
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                //res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top") };
                return res;
            }

            if (element_name == AD_EndThread_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_EndThread_Element() };
                res.CanResize = false;
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                //res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top") };
                return res;
            }
            if (element_name == AD_Thread_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_Thread_Element() };
                res.CanResize = false;
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                //res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top") };
                return res;
            }
            if (element_name == AD_While_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_While_Element() };
                res.CanResize = false;
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                //res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top") };
                return res;
            }
            if (element_name == AD_End_Condition_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_End_Condition_Element() };
                res.CanResize = false;
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                //res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top") };
                return res;
            }
            if (element_name == AD_End_While_Element.ElementName)
            {
                var res = new DesignerItem { Content = new AD_End_While_Element() };
                res.CanResize = false;
                res.CanRotate = false;
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.None, res, 0.5, 0.5, "Center") };
                //res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top") };
                return res;
            }
            Debugger.Break();
            throw new ApplicationException("Непредусмотеное название фигуры");
        }
    }
}
