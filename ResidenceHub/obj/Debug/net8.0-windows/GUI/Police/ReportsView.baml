   M S B A M L   `   `   `  ����  ResidenceHub)    #ResidenceHub.GUI.Police.ReportsView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Reports��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Reports and Statistics��6!   $��24(�6?   $
��Bold"�6M   $��0,0,0,15q�6_   � 5   
   $��1��6   $��0,0,0,15q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5      �� $��Report Type:��6   $��Center=�60   $��SemiBold"�6K   $��0,0,10,0q�6a   �� 5      -   5         ��Name$ cmbReportType��5      $	��150��62   $��30��6>   $��Center=�6J   .@�5       �� $��Resident Statistics��6#    �� 
IsSelected$
 True��6A   �� 5!      $��Household Statistics��6#   �� 5"      $��Registration Statistics��6#   5#      5$      �� 5&      $��2��6   $ 
Horizontal=�6)   .+�5'      �� -   5         ��Name$ 
btnRefresh��5'      $��Refresh��6-   $��80��6?   $��30��6J   ���#E0E0E0$��0q�5(   .   $��0,0,10,0q�6B    � 	Resources 5)       �� 
TargetType	 �� Property
 �� Value��%^   '��       �� 5*      " ��6    .�5+      �� 	 �@��CornerRadius$
 3��6=   5,      5-      5.      �� 50      -   5        $ 	btnExport��50      $��Export��6,   $��80��6=   $��30��6H   ���#C62828
�������$��0q�51   A    52      ��%^   '��       �� 53      " ��6    .�54      �� 	 �@��CornerRadius$
 3��6=   55      56      57      58      59      �� 5<   
   $��2��6   ���#F5F5F5 �� Padding$ 15q�63   $��0,0,0,15q�6@   ���#E0E0E0$��1q�6h   ._�5=      �  5>      �� 5?      $��* �6'   �� 5@      $��* �6'   �� 5A      $��* �6'   .+�5D      �� $��0��6   .+�5E      �� -   5         ��Name$ lblSummary1Title��5E       $��Total Residents��6:   $��SemiBold"�6Q   $��0,0,0,5q�6g   �� 5F      -   5        $ lblSummary1Value��5F       $��0��6:   $��24(�6C   $
��Bold"�6Q   ���#C628286c   5G      �� 5I      $��1��6   .+�5J      �� -   5        $ lblSummary2Title��5J       $��Permanent Residents��6:   $��SemiBold"�6U   $��0,0,0,5q�6k   �� 5K      -   5        $ lblSummary2Value��5K       $��0��6:   $��24(�6C   $
��Bold"�6Q   ���#2E7D326c   5L      �� 5N      $��2��6   .+�5O      �� -   5        $ lblSummary3Title��5O       $��Temporary Residents��6:   $��SemiBold"�6U   $��0,0,0,5q�6k   �� 5P      -	   5        $ lblSummary3Value��5P       $��0��6:   $��24(�6C   $
��Bold"�6Q   ���#1565C06c   5Q      5R      5S      &   System.Windows.Controls.DataGrid  5V   
   -
   5          Name$ dgReportData��5V      $��3��6)      AutoGenerateColumns$ False��5W         
IsReadOnly$
 True��5X      $��0q�5Y      ���#E0E0E0$��1q�5[      "   VerticalScrollBarVisibility$
 Auto=�5\      $   HorizontalScrollBarVisibility$
 Auto=�5]      
�������   AlternatingRowBackground �#F5F5F5   RowBackground
 �����   CanUserResizeColumns$
 True��5a         CanUserSortColumns$
 True��5b         GridLinesVisibility$	 All=�5c         VerticalGridLinesBrush �#E0E0E0   HorizontalGridLinesBrush �#E0E0E0   HeadersVisibility$ Column=�5f       5g      =  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell��%�  '        '  �     �� 5h      "  6   .�5i      �� 	 � ��
 �#EEEEEE63   �� 5j      	 � ��$
 SemiBold"�63   �� 5k      	 � ��$	
 8,5q�60   �� 5l      	 � ��
 �#CCCCCC64   �� 5m      	 � ��$
 0,0,1,1q�68   5n      �� 5o      "  6   .�5p      �� 	 � ��$	
 8,5q�60   �� 5q      	 � ��
 �#E0E0E064   �� 5r      	 � ��$
 0,0,1,0q�68   5s      5t      5u      5v      5w      