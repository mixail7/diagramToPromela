using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DiagramDesigner;

namespace dataLibrary_Pal
{
    /// <summary>
    /// класс, который отвечает за палитру компонентов
    /// </summary>
    [Serializable]
    public class BlockToolBoxPalleter : IToolBoxPalleter
    {

        public const string blockFormatName = "Exeption";

        public string[] getNames()
        {
            return new string[] { WorkerElement.ElementName };
        }

        public UserControl[] getControls(params string[] names)
        {
            var res = new List<UserControl>();

            if (names.Contains(WorkerElement.ElementName))
            {
                res.Add(new TaskToolBox());
            }

            return res.ToArray();
        }

        public string getPalleterName()
        {
            return "Модель исключений";
        }

        public Expander getPanel()
        {
            var toolBox = new WrapPanel();

            //toolBox.Background = Brushes.Azure;
            toolBox.Orientation = Orientation.Horizontal;

            toolBox.ItemHeight = 80;
            toolBox.ItemWidth = 80;

            toolBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            toolBox.VerticalAlignment = VerticalAlignment.Stretch;
            toolBox.Children.Add(new TaskToolBox());
            var res = new Expander { Header = getPalleterName(), IsExpanded = true, Content = toolBox };
            return res;
        }

        public bool isFormatOwner(string formatName)
        {
            return (formatName == blockFormatName);
        }

        public DesignerItem getElement(DragObject data)
        {
            var str = (string)data.data;


            if (str == WorkerElement.ElementName)
            {
                var res = new DesignerItem { Content = new WorkerElement(data.Tag) };
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top"),
                                 new Connector(ConnectorOrientation.Bottom, res, 0.5, 1, "Bottom")};
                return res;
            }

            Debugger.Break();
            throw new ApplicationException("Непредусмотеное название фигуры");
        }
    }
}
