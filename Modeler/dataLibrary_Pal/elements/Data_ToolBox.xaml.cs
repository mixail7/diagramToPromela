using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiagramDesigner;

namespace dataLibrary_Pal {
  /// <summary>
  /// Interaction logic for TaskToolBox.xaml
  /// </summary>
  public partial class DataToolBox : UserControl {
    public DataToolBox() {
      InitializeComponent();
    }


    /// <summary>
    /// флаг того, что объект должен начать операцию DragDrop
    /// </summary>
    private bool isDragging;// = false;


    protected override void OnPreviewMouseDown(MouseButtonEventArgs e) {
      base.OnPreviewMouseDown(e);
      //если на объекте нажали мышку, то если сдинется мышка, то начнется процесс таскания
      this.isDragging = true;
    }


    private void Grid_MouseMove(object sender, MouseEventArgs e) {
      if (e.LeftButton != MouseButtonState.Pressed) {
        isDragging = false;
      }

      if (isDragging == false) {
        return;
      }

      var dataObject = new DragObject { formatStr = BlockToolBoxPalleter.blockFormatName, data = DataElement.ElementName };
      dataObject.DesiredSize = new Size(140, 100);
      //получаем панель, на которой расположен элемент
      //начанаем операцию DragDrop
      DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);
      //сообщение обработали
      e.Handled = true;
      //что бы не начинать операцию Drag&Drop повторно при любом сдвиге мыши, сбрасываем флаг переноса
      isDragging = false;
    }

    private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
      this.isDragging = true;
    }
  }
}
