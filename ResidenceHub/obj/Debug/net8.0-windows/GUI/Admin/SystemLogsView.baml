   M S B A M L   `   `   `  ����  ResidenceHub+    %ResidenceHub.GUI.Admin.SystemLogsView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  System Logs��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��System Logs��6!   $��24(�64   $
��Bold"�6B   $��0,0,0,15q�6T   � 5   
   $��1��6   $��0,0,0,15q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5       �� $��Date Range:��6   $��Center=�6/   $��0,0,10,0q�6J   (  "System.Windows.Controls.DatePicker  5!      -   5          Name$ dpStartDate��5!      $	��120��62   $��30��6>   $��Center=�6J   $��0,0,5,0q�6l   �� 5"      $��to��6   $��Center=�6&   $��0,0,5,0q�6A     5#      -   5        $ 	dpEndDate��5#      $	��120��60   $��30��6<   $��Center=�6H   5$      �� 5&      $��1��6   $ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5'      �� $��User:��6   $��Center=�6)   $��0,0,10,0q�6D   �� 5(      -   5         ��Name$ cmbUserFilter��5(      $	��150��62   $��30��6>   $��Center=�6J    �� DisplayMemberPath$ FullName��5)      68   5*      �� 5,      $��3��6   $ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5-      �� $��Action:��6   $��Center=�6+   $��0,0,10,0q�6F   �� 5.      -   5         ��Name$ txtActionFilter��5.      $	��150��63   $��30��6?   $��5q�6K   $��Center=�6W   5/      �� 51      -   5         ��Name$ 	btnFilter��51   %   $��4��6   $��Apply Filter��68   $	��100��6O   $��30��6[   ���#6A1B9A
�������$��0q�52   =   	 � 	Resources	 53      
 �� 
TargetType �� Property �� Value��%^   '��       �� 54      "
 ��6   .�55      ��  �@��CornerRadius$ 3��69   56      57      58      59      &   System.Windows.Controls.DataGrid  5<   
   -   5          Name$ dgLogs��5<      $��2��6#      AutoGenerateColumns$ False��60      
IsReadOnly$
 True��6L   $��0,0,0,15q�5=         SelectionMode$ Single=�6%      Columns 5>      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *LogId+6K     5@      $
 User��6%   $	 150 6P    63   �� *
UserName+6P     5A      $ Action��6%   $ * 6P    65   �� *Action+6P     5B      $ 	Timestamp��6%   $	 150 6V    68   �� *	Timestamp+6V   5D      � 5G   
   $��3��6    5H      �� 5I      $
��Auto �6#   �� 5J      $��* �6#   �� 5K      $
��Auto �6#   .+�5N      �� -   5         ��Name$ 	lblStatus��5N   (   $��0��6   $�� ��6;   $��Center=�6C   $��SemiBold"�6^   �� 5P      $��2��6   $ 
Horizontal=�6)   .+�5Q      �� -   5        $ 	btnExport��5Q      $��Export Logs��6,   $	��120��6B   $��35��6N   ���#6A1B9A
�������$��0q�5R   A   $��0,0,10,0q�6U   	 5S      ��%^   '��       �� 5T      "
 ��6    .�5U      ��  �@��CornerRadius$ 3��6=   5V      5W      5X      �� 5Z      -	   5        $ 
btnRefresh��5Z      $��Refresh��6-   $��80��6?   $��35��6J   ���#E0E0E0$��0q�5[   .   	 5\      ��%^   '��       �� 5]      "
 ��6    .�5^      ��  �@��CornerRadius$ 3��6=   5_      5`      5a      5b      5c      5d      5e      