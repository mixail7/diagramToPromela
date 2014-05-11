using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DiagramDesigner;

namespace TaskLibrary_Pal
{
    /// <summary>
    /// класс, который отвечает за палитру компонентов
    /// </summary>
    [Serializable]
    public class TasksToolBoxPalleter : IToolBoxPalleter
    {

        public const string blockFormatName = "Tasks";

        public string[] getNames()
        {
            return new string[] { ComplexTaskElement.ElementName, TaskElement.ElementName };
        }

        public UserControl[] getControls(params string[] names)
        {
            var res = new List<UserControl>();

            if (names.Contains(ComplexTaskElement.ElementName))
            {
                res.Add(new ComplexTaskToolBox());
            }
            
            if (names.Contains(TaskElement.ElementName))
            {
                res.Add(new TaskToolBox());
            }

            return res.ToArray();
        }

        public string getPalleterName()
        {
            return "Модель задач";
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



            toolBox.Children.Add(new ComplexTaskToolBox());
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
            if (str == ComplexTaskElement.ElementName)
            {
                var res = new DesignerItem { Content = new ComplexTaskElement(data.Tag) };
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top"),
                                 new Connector(ConnectorOrientation.Bottom, res, 0.5, 1, "Bottom")};
                return res;
            }
            if (str == TaskElement.ElementName)
            {
                var res = new DesignerItem { Content = new TaskElement(data.Tag) };
                res.Connectors = new Connector[] { new Connector(ConnectorOrientation.Top, res, 0.5, 0, "Top"),
                                 new Connector(ConnectorOrientation.Bottom, res, 0.5, 1, "Bottom")};
                return res;
            }

            Debugger.Break();
            throw new ApplicationException("Непредусмотеное название фигуры");
        }
    }
}
