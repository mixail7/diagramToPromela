   M S B A M L   `   `   `  ����  Modeler6    0DiagramDesigner.PropertyEditor.StringArrayWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$%  Строковые данные��5   	   $	��300��6"   $	��300��6/    � WindowStartupLocation$ CenterScreen=�6;   .��5      �� -   5        $��MainListBox��5      ��5      �� 5       �� 
TargetType" ��6   .�5   
   ��  �� Property � �� �� Value$ Stretch=�67   �� 5	   
    � �� 5
      �� 5      .R�5      �� $��	theBorder��5      $��0q�5       �� Padding$ 1,1,1,1q�5      
������  J� SnapsToDevicePixels . ._�5      ]� ��. .+�5      ��  ��Name$ AddButtonInListBox��6   $��Bottom=�67   ��5      ��  �� Path$ AddCommandName���5      �� $ 
AddCommand��� 5      #��z��6!   |�.  .��5      �� # z 6%   5      5      5      5      5      �� 5   
    � �� 5       �� 5!      " ��6   .�5"      ��  � ��$ 
10,10,10,0q�6)   �� 5#       � �� 5$      �� 5%      	 �� 
TargetType"	 ��6$   .R�5&      � .+�5'      �� 
 ��Name$
 GelBackground��6"    J� Opacity$ 1Q�5(      #��z��6&   ���	#66ffffff$��1��5)   -   �� 5*      $
 GelShine��6"   $ 1Q�5+      $��2,2,2,0q�6&   $	��top=�67   
������ $��15��5,   /   ��5-      �� 5.       �� 
StartPoint$	 0,0&�60    �� EndPoint$	 0,1&�6A   ��50       � � 51   "    � Color$ 	#ccffffff��6/    � Offset$ 0Q�6A   � 52   "   $ transparent��6/   $ 1Q�6C   53   !   55      57      ]� 58      $��MainDockPanel��67   ��. .+�59      �� $��buttonsListBox��66   $��0q�6"   $��Right=�6L   ��6c   �� $ DataContext.commands� �� RelativeSource ��  �� AncestorType$ ListBoxL���5:      �� 5;      .�5<       ��  � �� 5=   "   �� 5>   $   .R�5?   &   ��  �� Orientation$ 
Horizontal=�61   $��Center=�5@   -   $��Center=�5A   -   5B   %   5D   !   5E      ��5G      �� 5H      .R�5I       �� $��0,0,0,0q�6'   ��5J   #   �� $ commandName���5K   #   �� $ DataContext� �� $ ListBoxItemL� �� AncestorLevel$ 2����5L   #   �� $ command�5M      5O      �� 5P      5Q      5R       �� Triggers 5S      P� 5T       P� Property �@��
IsSelected P� Value .  . �5U      ��  �� 
TargetName$ buttonsListBox��6!    � y�$ 	Collapsed=�6S   5V      P� 5W       �@��
IsSelected . . �5Z      ��  � ��
 �����67   5[      :� 5\       :� RoutedEvent$ Mouse.MouseEnter��6%   [�5]      �� 5^      .��5_      ��  �� 
TargetName$ GelBackground��6)    �� TargetProperty$* $(Shape.Fill).(SolidColorBrush.Color)�5`   !   .��5a       �� 	 �� To$ 	LimeGreenX�6/    g� Duration$ 0:0:0.1D�6>   5b      5c      5e      :� 5f      $ Mouse.MouseLeave��6%   [�5g      �� 5h      .��5i      �� $ GelBackground��6)   $* $(Shape.Fill).(SolidColorBrush.Color)�5j   !   .��5k       �� $ 0:0:0.1D�6/   5l      5m      5o      5q      5s      5t      5v      5w   	   5y      5z      