   M S B A M L   `   `   `  ����B  >Modeler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DiagramDesignerDiagramDesigner  5   @   "    DiagramDesigner.UserPalleter   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9DiagramDesignerclr-namespace:DiagramDesigner     6@     � Title$  UserPalleter��6v   $	��300��6�   $	��300��6�    � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5       �� DataType"   DiagramDesigner.PalleterInfo �� ItemsSource �� Path!   DiagramDesigner.ElementInfo �� Orientation d� 	IsChecked �� Mode��%b  (��       5      *, +)(�� q     5	      *, +)�� 5      "  6    5      �� $ elements�.R�5      �� ��6   �� $
 Name�6/   5      �� 5	      "  6   .R�5
      �� $ 
Horizontal=�6   .+�5   
   ��  6   �� $ 
IsSelected�$ TwoWay=�6H   �� 5   
   ��6   �� $
 Name�61   5   	   5      5      .��5      � 	 � RowDefinitions	 5      �� 5      6   �� 5      $��30 �6   6$   .+�5      R� -   5        $��MainTree��5      6!   � 5      $��1��6   
 � ColumnDefinitions
 5      �� 5   
   6   �� 5   
   6   .+�5      �� -   5        .��5   &   Сохранить61   �� 5      -   5        $��1��5      .��64   Отмена6<   5      5      5      