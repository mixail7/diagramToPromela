   M S B A M L   `   `   `  ����B  >Modeler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DiagramDesignerDiagramDesigner  5      C&clr-namespace:DiagramDesigner.ControlsDiagramDesigner.Controls  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ConnectionContextMenu.   (System.Windows.Input.ApplicationCommands     Delete m� Command m� Icon$   DiagramDesigner.DesignerCanvas   BringForward   BringToFront   SendBackward   
SendToBack    DiagramDesigner.Connection �� 
TargetType �� 	Resources  Key	 �� Property
 �� Value T� X T� Y �� 
TargetType �� RadiusX �� RadiusY �� Path  ArrowSymbolBaseStyle �� BasedOn � ContextMenu � DataContext �� RelativeSource �� StrokeStartLineCap �� StrokeEndLineCap �� StrokeLineJoin J� SnapsToDevicePixels �� StrokeDashArray  ArrowGridStyle �� Left
 �� Top � LayoutTransform �� Angle �� Triggers �� Value  �� Binding! �� 
TargetName 	 Arrow  
ArrowEmpty  Diamond 
 Circle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      #sclr-namespace:DiagramDesigner  5      ,c&clr-namespace:DiagramDesigner.Controls  5      %�  &	        '  �    7  7  7  7  7   7  7  7  7  7  7  7  7  7  7  �� 5      .@�5      m� $��Delete��6   # Z  6    5      �� 5	   
   $��Images/Delete.png��6   $��16��6+   5      �� 5      m� 5      $��Order��6   .@�5      m� $��Bring Forward��6   # Z 6(    5   
   �� 5      $��Images/BringForward.png��6   $��16��63   5   	   m� 5      $��Bring To Front��6   # Z 6)    5   
   �� 5      $��Images/BringToFront.png��6   $��16��63   5   	   m� 5      $��Send Backward��6   # Z 6(    5   
   �� 5      $��Images/SendBackward.png��6   $��16��63   5   	   m� 5      $��Send To Back��6   # Z 6'    5   
   �� 5      $��Images/SendToBack.png��6   $��16��61   5!   	   5"      5#      �� 5%      "  6
    5&      ���� 5(      ConnectionAdornerThumbStyle " n�62   .�5)   
   �� 	 � ��$
 12��6"   �� 5*   
   	 � ��$
 12��6#   �� 5+   
   	 �@J�SnapsToDevicePixels
 . 60   �� 5,   
   	 � z�
 5-      T� 5.      $ -6Q�6!   $ -6Q�6(   50      �� 51   
   	 � ��
 52      �� 53      " n�6   .R�55      �� ���	#AADCDCDC
������$��1��6?   $ 0��6S   $ 0��6_   56      58      59   	   �� 5;      ArrowGridStyle " �6%   .�5<   
   �� 	 � ��$
 10��6"   �� 5=   
   	 � ��$
 10��6#   �� 5>   
   	 � z�
 5?      T� 5@      $ -5Q�6!   $ -5Q�6(   5B      5C   	   �� 5E      ArrowSymbolBaseStyle " F�6+   .�5F   
   �� 	 � ��
 6!   �� $ 
ColorBrush��� 5G   
   	 �@��Stretch$

 Fill=�6$   5H   	   �� 5J      	Arrow " F�6   8   .�5K   
   �� 	 � ��"
 ��        � z  	= �     z 	6!   5L   	   �� 5N      Diamond " F�6   8  .�5O   
   �� 	 � ��+
 ������    �    �����@KL     �    @KL 	6!   5P   	   �� 5S      
Circle " F�6   8  .�5T   
   �� 	 � ��%
 ��@KL     �@KL @B @KL @KL 	6!   5U   	   �� 5W      
ArrowEmpty " F�6!   8  .�5X   
   �� 	 � ��=
 ��        � z  	= �     z �    ��j ���j  	= �    @B 	6!   5Y   	   5Z      .�5[      �� 	 �@J�SnapsToDevicePixels
 . 6,   �� 5\      	 � ��
 5]      �� 5^   
   "  6   .R�5_      �� 8   6   ��  �� *TemplatedParent+.+�5a      F� $��PART_ConnectionPath��6   $��2��5b      $ Round=�5d      $ Round=�5e      $ Round=�5f       . ��5c      �� $ 
ColorBrush� 5g      �� *StrokeDashArray+��5i      �� *PathGeometry+5j      � 5k      8��  5l      �� *AnchorPositionSource.X+ 5m      �� *AnchorPositionSource.Y+.+�5n      F� $��PART_SourceAnchorPath��6    5o      �� 5p       6"   �� *AnchorAngleSource+5r      � 5s      8��  5t      �� *AnchorPositionSink.X+ 5u      �� *AnchorPositionSink.Y+.+�5v      F� $��PART_SinkAnchorPath��6    5w      �� 5x       6"   �� *AnchorAngleSink+5z      �� 5|      $��startTextBlock��6   ��5}   (   �� $ 	StartText� 5~   (   �� *StartLabelPosition.X+ 5   (   �� *StartLabelPosition.Y+5�      �� 5�      $��endTextBlock��6   ��5�   (   �� $ EndText� 5�   (   �� *EndLabelPosition.X+ 5�   (   �� *EndLabelPosition.Y+5�      �� 5�      $��mainTextBlock��6   ��5�   (   �� $
 Text� 5�   (   �� *LabelPosition.X+ 5�   (   �� *LabelPosition.Y+z�5�       V� 5�   "   .~�5�   "   T� $ 5Q�65   $ 5Q�6;   �� 5�   "    62   �� $ 
LabelAngle�5�   #   5�   %   5�      5�       5�      �� 5�      $ Arrow��6     6(   ��  �� *Self+$ SourceArrowSymbol�.a�5�      �� $! PART_SourceAnchorPath��6   8
  	 � ��6K   5�      �� 5�      $ 
ArrowEmpty��6     6-   ��  �� *Self+$ SourceArrowSymbol�.a�5�      �� $! PART_SourceAnchorPath��6   8
  	 � ��6K   5�      �� 5�      $ Diamond��6     6*   ��  �� *Self+$ SourceArrowSymbol�.a�5�      �� $! PART_SourceAnchorPath��6   8
 	 	 � ��6K   5�      �� 5�      $ Circle��6     6)   ��  �� *Self+$ SourceArrowSymbol�.a�5�      �� $! PART_SourceAnchorPath��6   8
 
 	 � ��6K   5�      �� 5�      $ Arrow��6     6(   ��  �� *Self+$ SinkArrowSymbol�.a�5�      �� $! PART_SinkAnchorPath��6   8
  	 � ��6I   5�      �� 5�      $ 
ArrowEmpty��6     6-   ��  �� *Self+$ SinkArrowSymbol�.a�5�      �� $! PART_SinkAnchorPath��6   8
  	 � ��6I   5�      �� 5�      $ Diamond��6     6*   ��  �� *Self+$ SinkArrowSymbol�.a�5�      �� $! PART_SinkAnchorPath��6   8
  	 � ��6I   5�      �� 5�      $ Circle��6     6)   ��  �� *Self+$ SinkArrowSymbol�.a�5�      �� $! PART_SinkAnchorPath��6   8
  	 � ��6I   5�      5�      5�      5�      5�      