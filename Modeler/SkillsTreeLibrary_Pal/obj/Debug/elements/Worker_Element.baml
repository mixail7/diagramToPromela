   M S B A M L   `   `   `  ����  ResourceLibrary_Pal'    !ResourceLibrary_Pal.WorkerElement   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � -   5        $	��100��5   &   $	��140��63   
��� � �.+�5   
   �� -   5        $��
MainBorder��5   '   
��� � �$��2q�6]     �� CornerRadius$  0��6q   $��Stretch=�6�   $��5,5,5,5q�6�   ��69   �� *BorderBrush+._�5	      &   System.Windows.Controls.DataGrid  -   5        $��	DataGrid1��5	   V   
��� � �   AutoGenerateColumns .  $
��auto��6H   $
��auto��6g      CanUserResizeColumns .     CanUserSortColumns .     CanUserReorderColumns .   J� IsManipulationEnabled . !   EnableColumnVirtualization .    
IsReadOnly . ��6�   ��  � LayoutTransform 5
      V� 5      .~�5      �� 	 �� Angle$	 90Q�6*   u� 5      
 u� Matrix$
 -1,0,0,1,0,0x�6*   5         ColumnHeaderStyle 5      �� 5       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    �� BasedOn# [B 6G   .�5      ��  �� Property �@�LayoutTransform �� Value 5      V� 5   "   .~�5   &   �� $		 -90Q�66   �� 5   &    �� ScaleX$ 1Q�65    �� ScaleY$ -1Q�6@   5   #   5      5         	CellStyle 5      �� 5      *  $System.Windows.Controls.DataGridCell"  6   .�5      ��  �@�LayoutTransform 5      V� 5    "   .~�5!   &   �� $		 -90Q�66   �� 5"   &   $ 1Q�65   $ -1Q�6@   5#   #   5%      5&         Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6)      
IsReadOnly .    
Visibility$ Visible=�6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   ��  �� Path$ Id�6c     5*      $
 Name��6)    67   �� $
 Name�  5+      $ Skills��6)    69   �� $ Skills�5-      5.      50      51      