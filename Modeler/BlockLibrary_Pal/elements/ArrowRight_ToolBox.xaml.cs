﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiagramDesigner;

namespace BlockLibrary_Pal {
  /// <summary>
  /// Interaction logic for Arrow45_ToolBox.xaml
  /// </summary>
  public partial class ArrowRight_ToolBox : UserControl {
    public ArrowRight_ToolBox() {
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

      var dataObject = new DragObject { formatStr = BlockToolBoxPalleter.blockFormatName, data = ArrowRight_Element.ElementName };
      dataObject.DesiredSize = new Size(90, 90);
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
