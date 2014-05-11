using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiagramDesigner;

namespace TaskLibrary_Pal {
  /// <summary>
  /// Interaction logic for ComplexTaskToolBox.xaml
  /// </summary>
  public partial class ComplexTaskToolBox : UserControl {
    public ComplexTaskToolBox() {
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

      var dataObject = new DragObject { formatStr = TasksToolBoxPalleter.blockFormatName, data = ComplexTaskElement.ElementName };
      dataObject.DesiredSize = new Size(197, 113);
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
