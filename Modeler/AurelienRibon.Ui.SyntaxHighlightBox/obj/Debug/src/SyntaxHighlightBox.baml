   M S B A M L   `   `   `  ����^  ZAurelienRibon.Ui.SyntaxHighlightBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:AurelienRibon.Ui.SyntaxHighlightBox#AurelienRibon.Ui.SyntaxHighlightBox  5      <    6AurelienRibon.Ui.SyntaxHighlightBox.SyntaxHighlightBox   5   
   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Clocal1clr-namespace:AurelienRibon.Ui.SyntaxHighlightBox     5        �� TextWrapping$  NoWrap=�6C   ���	#00000000.��6p   ��5      �� 5       �� 
TargetType<   6AurelienRibon.Ui.SyntaxHighlightBox.SyntaxHighlightBox"  6
   .�5      ��  �� Property �@��AcceptsReturn �� Value . 6%   �� 5	       �@��
AcceptsTab . 6"   �� 5
      % �@��HorizontalScrollBarVisibility$
 Auto=�65   �� 5      # �@��VerticalScrollBarVisibility$
 Auto=�63   �� 5       � ��$ Courier New)�6"   �� 5       � ��$	 9pt(�6    5      .��5      ��5      �� 5       �� 
TargetType"  6   .R�5      �� 
�������#��z��6   #��z��5      ._�5      �  � ColumnDefinitions 5      �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5      8   2AurelienRibon.Ui.SyntaxHighlightBox.DrawingControl    Name$ PART_LineNumbersCanvas��6   $��0��6<   }�. 6L   �� 5       ��Name$ lineNumbersSeparator��5    ?   $��1��5      $��2,0,5,0q�6   
�������$��1��6;   	 �� Y2$ 1��6O   	 �� Stretch$
	 Fill=�6V   
 �� StrokeDashArray$	
 1,1R�5        �� EdgeMode$ Aliased=�6   6?   � 5"      $��2��6   .+�5$      � #��z��6   .+�5%   	     $ PART_RenderCanvas��6   }�. &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5&   
      TextFormattingMode$ Display=�64   5'   	   �� 5)       ��Name$ PART_ContentHost��6   ���	#00FFFFFF6/   5+      5-      5.       �� Triggers 51      P� 52       P� Property" �@ IsLineNumbersMarginVisible P� Value .  . �53      ��  �� 
TargetName$ PART_LineNumbersCanvas��6    � y�$ 	Collapsed=�6H   �� 54      $ lineNumbersSeparator��6    � y�$ 	Collapsed=�6F   55      57      .��58      5:      