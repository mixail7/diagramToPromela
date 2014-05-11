using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using GraphSharp.Algorithms.Layout.Simple.Hierarchical;
using GraphSharp.Algorithms.Layout.Simple.Tree;
using GraphSharp.Controls;
using QuickGraph;
using testGraphLayout;

namespace DiagramDesigner
{
    public partial class DesignerCanvas : Canvas
    {
        private Point? rubberbandSelectionStartPoint = null;
        /// <summary>
        /// массив индексных ссылок
        /// </summary>
        private Dictionary<Guid, IndexClass> indexs = new Dictionary<Guid, IndexClass>();
        /// <summary>
        /// имя файла, который сейчас открыт в редакторе диаграмм
        /// </summary>
        private string fileName = "";

        /// <summary>
        /// массив комментариев
        /// </summary>
        private Dictionary<Guid, string> comments = new Dictionary<Guid, string>();

        public DesignerItem FindById(string id)
        {
            for (var i = 0; i < Children.Count; i++)
            {
                var elem = Children[i] as DesignerItem;
                if (elem != null && elem.ID.ToString() == id)
                {
                    return elem;
                }
            }
            return null;
        }
        /*
         * все исходящие связи исходного элемента превращает в исходящие связи элемента 
         */
        public void replaceAllLinks(DesignerItem source, DesignerItem dest)
        {
            for (int i = 0, max = Children.Count; i < max; i++)
            {
                
                var elem1 = Children[i] as Connection;
                if (elem1 != null && elem1.Source.ParentDesignerItem.ID == source.ID && dest.ID != elem1.Sink.ParentDesignerItem.ID)
                {
                    addConnection(new Connection(dest.Connectors[0], elem1.Sink.ParentDesignerItem.Connectors[0], true));
                    this.Children.Remove(elem1);
                    break;
                }
            }
        }

        public void autoPozitions()
        {
            return;
            /*var g = new BidirectionalGraph<object, IEdge<object>>();
            for (int i = 0; i < Children.Count; i++)
            {
                var elem = Children[i] as DesignerItem;
                if (elem != null)
                {
                    g.AddVertex(elem.ID);
                }
                var elem1 = Children[i] as Connection;
                if (elem1 != null)
                {
                    g.AddEdge(new Edge<object>(elem1.Source.ParentDesignerItem.ID, elem1.Sink.ParentDesignerItem.ID));
                }
            }
            var gl = new GraphLayout {Graph = g};
            var sl = new EfficientSugiyamaLayoutParameters();
            //sl.EdgeRouting
            sl.VertexDistance = 50;
            sl.LayerDistance = 50;
            sl.OptimizeWidth = true;
            gl.Graph = g;
            gl.LayoutParameters = sl;
            
            gl.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            gl.Arrange(new Rect(0, 0, 1000, 1000));
            gl.LayoutAlgorithmType = "EfficientSugiyama";
            //gl.LayoutAlgorithmType = "Tree";
            gl.IsAnimationEnabled = false;
            gl.Relayout(true);
            string buf = "";
            int minx = 1000, miny = 1000;
            foreach (var elem in gl.Children)
            {
                var ver = elem as VertexControl;
                if (ver != null)
                {
                    var x = GraphCanvas.GetX(ver);
                    var y = GraphCanvas.GetY(ver);
                    var obj = FindById(ver.Vertex.ToString());
                    int valid_x = (int) Math.Round(x - obj.DesiredSize.Width/2);
                    int valid_y = (int)Math.Round(y - obj.DesiredSize.Height / 2);
                    if (minx > valid_x )
                        minx = valid_x;
                    if (miny > valid_y)
                        miny = valid_y;
                    buf += String.Format("[{0},{1}][{2},{3}]\r\n", valid_x, valid_y, obj.DesiredSize.Width, obj.DesiredSize.Height);
                }
            }
            foreach (var elem in gl.Children)
            {
                var ver = elem as VertexControl;
                if (ver != null)
                {
                    var x = GraphCanvas.GetX(ver);
                    var y = GraphCanvas.GetY(ver);
                    var obj = FindById(ver.Vertex.ToString());
                    SetLeft(obj, Math.Round(x - obj.DesiredSize.Width / 2 + Math.Abs(minx)));
                    SetTop(obj, Math.Round(y - obj.DesiredSize.Height / 2 + Math.Abs(miny)));
                   // buf += String.Format("[{0},{1}]\r\n", Math.Round(x), Math.Round(y));
                }
            }*/
           // MessageBox.Show(buf);
        }

        public void createIndexCopy(Guid oldGuid, Guid newGuid)
        {
            if (indexs.ContainsKey(oldGuid) == false)
            {
                return;
            }
            if (indexs.ContainsKey(newGuid) == false)
            {
                indexs.Add(newGuid, null);
            }
            indexs[newGuid] = new IndexClass() { indexType = indexs[oldGuid].indexType, value = indexs[oldGuid].value };
        }


        /// <summary>
        /// масштаб диаграммы
        /// </summary>
        private double scaleSize = 1;

        private bool _simpleConnections = false;
        public bool simpleConnections
        {
            get
            {
                return _simpleConnections;
            }
            set
            {
                _simpleConnections = value;
            }
        }

        private ArrowSymbol _currentArrowSymbol = ArrowSymbol.Arrow;
        public ArrowSymbol currentArrowSymbol
        {
            get
            {
                return _currentArrowSymbol;
            }
            set
            {
                _currentArrowSymbol = value;
            }
        }
        /// <summary>
        /// цвет стрелки, когда она создана, но еще не подсоединена ко второму коннектору
        /// <para>(когда пользователь еще таскает её)</para>
        /// </summary>
        public Brush TempConnectorColorBrush
        {
            get { return (Brush)GetValue(TempConnectorColorBrushProperty); }
            set
            {
                SetValue(TempConnectorColorBrushProperty, value);
            }
        }

        public static readonly DependencyProperty TempConnectorColorBrushProperty =
            DependencyProperty.Register("TempConnectorColorBrush", typeof(Brush), typeof(Connection), new PropertyMetadata(Brushes.Green));


        private SelectionService selectionService;
        public SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                SelectionService.ClearSelection();
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    var adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
                    adornerLayer.Add(adorner);
                }
            }
            e.Handled = true;
        }



        private IToolBoxPalleter[] palleters = new IToolBoxPalleter[0];

        public void initPalleters(IToolBoxPalleter[] _palleters)
        {
            palleters = _palleters;
        }

        public DesignerItem getDisignerItemFromBuffer(DragObject data)
        {
            foreach (var palleter in palleters)
            {
                if (palleter.isFormatOwner(data.formatStr) == false)
                {
                    continue;
                }
                data.Tag = this.Tag;
                return palleter.getElement(data);
            }
            Debugger.Break();
            throw new ApplicationException("Неизвестный тип данных");
        }
        /// <summary>
        /// событие, которое происходит при броске на диаграмму
        /// </summary>
        public event DesignerDragEvent DesignerDrop;

        public void addDesignerItem(DesignerItem newItem, int posX, int posY)
        {
            var value = Math.Max(0, posX);
            SetLeft(newItem, value);
            SetTop(newItem, Math.Max(0, posY));

            this.Children.Add(newItem);
        }

        public void addConnection(Connection connection)
        {
            this.Children.Add(connection);
        }


        protected override void OnDrop(DragEventArgs e)
        {
            //вызываем обработчик события Drop у родителя
            base.OnDrop(e);
            //получаем позицию мыши
            Point position = e.GetPosition(this);
            if (DesignerDrop != null)
            {
                DesignerDrop(this, new DesignerDragEventArgs() { dragArgs = e, position = position });
            }
            //получаем объект, который бросили на Cancas
            var dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            //если его нет, то
            if (dragObject == null)
            {
                //и выходим
                return;
            }
            DesignerItem newItem = getDisignerItemFromBuffer(dragObject);
            if (newItem == null)
            {
                //и выходим
                return;
            }
            //если у объекта есть размер
            if (dragObject.DesiredSize.HasValue)
            {
                //получаем размер
                Size desiredSize = dragObject.DesiredSize.Value;
                //устанавливаем у нового объекта размер
                newItem.Width = desiredSize.Width;
                newItem.Height = desiredSize.Height;
                //и делаем так, что бы центр оказался под указателем мыши, но на Canvas
                SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2));
                SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2));
            }
            else
            {
                //если размеры не известны, то делаем так, что бы правый верхний угол оказался под мышкой
                SetLeft(newItem, Math.Max(0, position.X));
                SetTop(newItem, Math.Max(0, position.Y));
            }

            //добавляем новый объект в дочерние Canvas
            int res = this.Children.Add(newItem);

            //убираем выделение со всех выделенных объектов
            this.SelectionService.SelectItem(newItem);
            newItem.Focus();

            //e.Handled = true;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            size.Width += 10;
            size.Height += 10;
            return size;
        }

        public void SetConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                ControlTemplate template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Template = template;
            }
        }
    }

    public class DesignerDragEventArgs
    {
        public DragEventArgs dragArgs { get; set; }
        public Point position { get; set; }
    }
    /// <summary>
    /// возможные типы ссылок
    /// </summary>
    [Serializable]
    public enum IndexTypesEnum
    {
        /// <summary>
        /// абсолютная ссылка на файл
        /// </summary>
        FileLink = 0,
        /// <summary>
        /// относительная ссылка на файл
        /// </summary>
        RelativeFileLink = 1
    }

    public static class IndexTypesEnumConverter
    {
        public static int getIntValue(IndexTypesEnum value)
        {
            var res = (int)value;
            return res;
            /*
            if (value == IndexTypesEnum.FileLink) {
              return 0;
            }
            Debugger.Break();
            throw new ApplicationException("Не предусмотреный тип ссылки");
             * */
        }

        public static IndexTypesEnum getEnumValue(int value)
        {
            var res = (IndexTypesEnum)value;
            return res;
            //if (value == 0) {
            //  return IndexTypesEnum.FileLink;
            //}
            //Debugger.Break();
            //throw new ApplicationException("Не предусмотреный тип ссылки");
        }
    }

    /// <summary>
    /// индексная ссылка
    /// </summary>
    [Serializable]
    public class IndexClass
    {
        /// <summary>
        /// тип ссылки
        /// </summary>
        public IndexTypesEnum indexType { get; set; }
        /// <summary>
        /// значение ссылки
        /// </summary>
        public string value { get; set; }
    }

    public delegate void DesignerDragEvent(object sender, DesignerDragEventArgs args);
}
