   M S B A M L   `   `   `  ����  ResidenceHub2    ,ResidenceHub.GUI.AreaLeader.ResidentListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Resident List��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Resident List��6!   $��24(�66   $
��Bold"�6D   $��0,0,0,15q�6V   � 5   
   $��1��6   $��0,0,0,15q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   .+�5      �� $��Area:��6   $��Center=�6)   $��SemiBold"�6D   $��0,0,10,0q�6Z   �� 5      -   5         ��Name$ lblArea��5      $��Your Managed Area��6-   $��Center=�6F   $
��Bold"�6a   ���#1565C06s   5       �� 5"      $��1��6   $ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5#      �� $��Search:��6   $��Center=�6+   $��0,0,10,0q�6F   �� 5$      -   5         ��Name$ 	txtSearch��5$      $	��200��6-   $��30��69   $��5q�6E   $��Center=�6Q   5%      �� 5'      -   5         ��Name$ 	btnSearch��5'   %   $��2��6   $��Search��68   $��80��6I   $��30��6T   ���#1565C0
�������$��0q�5(   =    � 	Resources 5)       �� 
TargetType	 �� Property
 �� Value��%^   '��       �� 5*      " ��6   .�5+      �� 	 �@��CornerRadius$
 3��69   5,      5-      5.      5/      &   System.Windows.Controls.DataGrid  52   
   -   5          Name$ dgResidents��52      $��2��6(      AutoGenerateColumns$ False��65      
IsReadOnly$
 True��6Q   $��0,0,0,15q�53         SelectionMode$ Single=�6%      Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *UserId+6L     56      $ 	Full Name��6%   $	 150 6U    68   �� *
FullName+6U     57      $ Email��6%   $	 150 6N    64   �� *Email+6N     58      $ Address��6%   $ * 6R    66   �� *	Address+6R     59      $
 Role��6%   $	 100 6L    63   �� *Role+6L   4  .System.Windows.Controls.DataGridTemplateColumn  5:      $ Actions��6)   $	 120 6:      CellTemplate 5;      �� 5<      .R�5=      �� $ 
Horizontal=�6)   .+�5>   "   �� -   5        $
��View��5>   )   $��0,0,5,0q�6O   $	��5,2q�6`   ���#1565C0
�������$��0q�5?   n   
 � Tag 5>   8   �� *UserId+ 5@   &   ��%^   '��       �� 5A   *   " ��60   .�5B   .   �� 	 �@��CornerRadius$
 2��6M   5C   +   5D   '   5E   #   �� 5F   "   -   5        $��Notify��5F   )   $	��5,2q�6Q   ���#FF9800
�������$��0q�5G   s    5F   :   �� *UserId+ 5H   &   ��%^   '��       �� 5I   *   " ��60   .�5J   .   �� 	 �@��CornerRadius$
 2��6M   5K   +   5L   '   5M   #   5N      5O      5Q      5S      � 5V   
   $��3��6    5W      �� 5X      $
��Auto �6#   �� 5Y      $��* �6#   �� 5Z      $
��Auto �6#   .+�5]      �� -   5        $ 	lblStatus��5]   (   $��0��6   $��Displaying 50 residents��6;   $��Center=�6Z   �� 5_      $��2��6   $ 
Horizontal=�6)   .+�5`      �� -   5        $ btnPrevious��5`      $��Previous��6.   $��80��6A   $��30��6L   ���#E0E0E0$��0q�5a   .   $��0,0,10,0q�6B    5b      ��%^   '��       �� 5c      " ��6    .�5d      �� 	 �@��CornerRadius$
 3��6=   5e      5f      5g      �� 5i      -	   5        $ lblPage��5i      $��Page 1 of 5��6-   $��Center=�6@   $��0,0,10,0q�6[   �� 5k      -
   5        $ btnNext��5k      $
��Next��6*   $��80��69   $��30��6D   ���#E0E0E0$��0q�5l   .    5m      ��%^   '��       �� 5n      " ��6    .�5o      �� 	 �@��CornerRadius$
 3��6=   5p      5q      5r      5s      5t      5u      5v      