using System.Windows;

namespace DiagramDesigner
{
    /// <summary>
    /// информация о том, что перетаскивют на Canvas
    /// </summary>
    public class DragObject
    {
        /*
         * доп поле для хранения различного рода данных
         */
        public object Tag;

        /// <summary>
        /// данные, которые создал компонент палитры
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// строка формата, которая описывает того, кто обрабатывает данные
        /// </summary>
        public string formatStr { get; set; }

        /// <summary>
        /// размеры DesignerItem, когда её бросают на DesignerCanvas
        /// </summary>
        public Size? DesiredSize { get; set; }
    }
}
