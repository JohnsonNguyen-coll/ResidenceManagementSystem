   M S B A M L   `   `   `  ����  ResidenceHub/    )ResidenceHub.GUI.Admin.UserManagementView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  User Management��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��User Management��6!   $��24(�68   $
��Bold"�6F   $��0,0,0,15q�6X   � 5   
   $��1��6   $��0,0,0,15q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5      �� $��Role:��6   $��Center=�6)   $��0,0,10,0q�6D   �� 5       -   5         ��Name$ cmbRoleFilter��5       $	��120��62   $��30��6>   $��Center=�6J   .@�5!      �� $	��All��6#    �� 
IsSelected$
 True��61   �� 5"      $��Citizen��6#   �� 5#      $��
AreaLeader��6#   �� 5$      $��Police��6#   �� 5%      $��Admin��6#   5&      5'      �� 5)      $��2��6   $ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5*      �� $��Search:��6   $��Center=�6+   $��0,0,10,0q�6F   �� 5+      -   5         ��Name$ 	txtSearch��5+      $	��200��6-   $��30��69   $��5q�6E   $��Center=�6Q   5,      �� 5.      -   5         ��Name$ 	btnSearch��5.   %   $��3��6   $��Search��68   $��80��6I   $��30��6T   ���#6A1B9A
�������$��0q�5/   =    � 	Resources 50      	 �� 
TargetType
 �� Property �� Value��%^   '��       �� 51      "	 ��6   .�52      �� 
 �@��CornerRadius$ 3��69   53      54      55      56      &   System.Windows.Controls.DataGrid  59   
   -   5          Name$ dgUsers��59      $��2��6$      AutoGenerateColumns$ False��61      
IsReadOnly$ False��6M   $��0,0,0,15q�5:         SelectionMode$ Single=�6%      Columns 5;      0  *System.Windows.Controls.DataGridTextColumn  5<      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6L      
IsReadOnly$
 True��6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *UserId+6W     5=      $ 	Full Name��6%   $	 150 6U    68   �� *
FullName+6U     5>      $ Email��6%   $	 150 6N    64   �� *Email+6N     5?      $ Address��6%   $ * 6R    66   �� *	Address+6R   4  .System.Windows.Controls.DataGridComboBoxColumn  5@      $
 Role��6)   $	 100 6\      SelectedItemBinding 67   �� *Role+   ElementStyle 5A      �� 5B      "	 ��6    .�5C      �� 
 � �� 6<   �� *DataContext.Roles+ �� RelativeSource ��  �� AncestorType DataGrid5D         EditingElementStyle 5F      �� 5G      "	 ��6    .�5H      �� 
 � �� 6<   �� *DataContext.Roles+ ��  DataGrid5I      5K      4  .System.Windows.Controls.DataGridTemplateColumn  5L      $ Actions��6)   $	 120 6:      CellTemplate 5M      �� 5N      .R�5O      �� $ 
Horizontal=�6)   .+�5P   "   �� -   5        $
��Save��5P   )   $��0,0,5,0q�6O   $	��5,2q�6`   ���#4CAF50
�������$��0q�5Q   g   
 � Tag 5P   8   �� *UserId+ 5R   &   ��%^   '��       �� 5S   *   "	 ��60   .�5T   .   �� 
 �@��CornerRadius$ 2��6M   5U   +   5V   '   5W   #   �� 5X   "   -   5        $��Delete��5X   )   $	��5,2q�6Q   ���#F44336
�������$��0q�5Y   i    5X   :   �� *UserId+ 5Z   &   ��%^   '��       �� 5[   *   "	 ��60   .�5\   .   �� 
 �@��CornerRadius$ 2��6M   5]   +   5^   '   5_   #   5`      5a      5c      5e      � 5h   
   $��3��6    5i      �� 5j      $
��Auto �6#   �� 5k      $��* �6#   �� 5l      $
��Auto �6#   .+�5o      �� -   5         ��Name$ 	lblStatus��5o   (   $��0��6   $�� ��6;   $��Center=�6C   $��SemiBold"�6^   �� 5q      $��2��6   $ 
Horizontal=�6)   .+�5r      �� -   5        $ btnAdd��5r      $��Add New User��6)   $	��120��6@   $��35��6L   ���#6A1B9A
�������$��0q�5s   A   $��0,0,10,0q�6U    5t      ��%^   '��       �� 5u      "	 ��6    .�5v      �� 
 �@��CornerRadius$ 3��6=   5w      5x      5y      �� 5{      -	   5        $ 
btnRefresh��5{      $��Refresh��6-   $��80��6?   $��35��6J   ���#E0E0E0$��0q�5|   .    5}      ��%^   '��       �� 5~      "	 ��6    .�5      �� 
 �@��CornerRadius$ 3��6=   5�      5�      5�      5�      5�      5�      5�      