   M S B A M L   `   `   `  ����  ResidenceHub5    /ResidenceHub.GUI.Police.HouseholdManagementView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Household Management��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Household Management��6!   $��24(�6=   $
��Bold"�6K   $��0,0,0,15q�6]   � 5   
   $��1��6   $��0,0,0,15q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5      �� $��Area:��6   $��Center=�6)   $��SemiBold"�6D   $��0,0,10,0q�6Z   �� 5       -   5         ��Name$ lblArea��5       $��Your Managed Area��6-   $��Center=�6F   $
��Bold"�6a   ���#C628286s   5!      �� 5#      $��2��6   $ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5$      �� $��Search:��6   $��Center=�6+   $��0,0,10,0q�6F   �� 5%      -   5         ��Name$ 	txtSearch��5%      $	��200��6-   $��30��69   $��5q�6E   $��Center=�6Q   5&      �� 5(      -   5         ��Name$ 	btnSearch��5(   %   $��3��6   $��Search��68   $��80��6I   $��30��6T   ���#C62828
�������$��0q�5)   =    � 	Resources 5*       �� 
TargetType	 �� Property
 �� Value��%^   '��       �� 5+      " ��6   .�5,      �� 	 �@��CornerRadius$
 3��69   5-      5.      5/      50      &   System.Windows.Controls.DataGrid  53   
   -   5          Name$ dgHouseholds��53      $��2��6)      AutoGenerateColumns$ False��66      
IsReadOnly$
 True��6R   $��0,0,0,15q�54         SelectionMode$ Single=�6%      Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *HouseholdId+6Q     57      $ Head of Household��6%   $	 150 6h    6@   �� *HeadOfHouseholdName+6h     58      $ Address��6%   $ * 6R    66   �� *	Address+6R     59      $ Created Date��6%   $	 120 6[    6;   �� *CreatedDate+6[     5:      $ Members Count��6%   $	 100 6]    6<   �� *MembersCount+6]   4  .System.Windows.Controls.DataGridTemplateColumn  5;      $ Actions��6)   $	 180 6:      CellTemplate 5<      �� 5=      .R�5>      �� $ 
Horizontal=�6)   .+�5?   "   �� -   5        $
��View��5?   )   $��0,0,5,0q�6T   $	��5,2q�6e   ���#C62828
�������$��0q�5@   g   
 � Tag 5?   8   �� *HouseholdId+ 5A   &   ��%^   '��       �� 5B   *   " ��60   .�5C   .   �� 	 �@��CornerRadius$
 2��6M   5D   +   5E   '   5F   #   �� 5G   "   -   5        $
��Edit��5G   )   $��0,0,5,0q�6T   $	��5,2q�6e   ���#FFC107
�������$��0q�5H   g    5G   8   �� *HouseholdId+ 5I   &   ��%^   '��       �� 5J   *   " ��60   .�5K   .   �� 	 �@��CornerRadius$
 2��6M   5L   +   5M   '   5N   #   �� 5O   "   -   5        $��Delete��5O   )   $	��5,2q�6V   ���#F44336
�������$��0q�5P   i    5O   :   �� *HouseholdId+ 5Q   &   ��%^   '��       �� 5R   *   " ��60   .�5S   .   �� 	 �@��CornerRadius$
 2��6M   5T   +   5U   '   5V   #   5W      5X      5Z      5\      � 5_   
   $��3��6    5`      �� 5a      $
��Auto �6#   �� 5b      $��* �6#   �� 5c      $
��Auto �6#   .+�5f      �� -   5        $ 	lblStatus��5f   (   $��0��6   $�� ��6;   $��Center=�6C   $��SemiBold"�6^   �� 5h      $��2��6   $ 
Horizontal=�6)   .+�5i      �� -	   5        $ btnAdd��5i      $��Add New Household��6)   $	��150��6E   $��35��6Q   ���#C62828
�������$��0q�5j   A   $��0,0,10,0q�6U    5k      ��%^   '��       �� 5l      " ��6    .�5m      �� 	 �@��CornerRadius$
 3��6=   5n      5o      5p      �� 5r      -
   5        $ 
btnRefresh��5r      $��Refresh��6-   $��80��6?   $��35��6J   ���#E0E0E0$��0q�5s   .    5t      ��%^   '��       �� 5u      " ��6    .�5v      �� 	 �@��CornerRadius$
 3��6=   5w      5x      5y      5z      5{      5|      5}      