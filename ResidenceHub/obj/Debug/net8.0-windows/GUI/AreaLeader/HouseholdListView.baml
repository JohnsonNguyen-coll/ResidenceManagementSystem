   M S B A M L   `   `   `  ����  ResidenceHub3    -ResidenceHub.GUI.AreaLeader.HouseholdListView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Household List��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Household List��6!   $��24(�67   $
��Bold"�6E   $��0,0,0,15q�6W   � 5   
   $��1��6   $��0,0,0,15q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5      �� $��Area:��6   $��Center=�6)   $��SemiBold"�6D   $��0,0,10,0q�6Z   �� 5       -   5         ��Name$ lblArea��5       $��Your Managed Area��6-   $��Center=�6F   $
��Bold"�6a   ���#1565C06s   5!      �� 5#      $��2��6   $ 
Horizontal=�6)   $��0,0,15,0q�6B   .+�5$      �� $��Search:��6   $��Center=�6+   $��0,0,10,0q�6F   �� 5%      -   5         ��Name$ 	txtSearch��5%      $	��200��6-   $��30��69   $��5q�6E   $��Center=�6Q   5&      �� 5(      -   5         ��Name$ 	btnSearch��5(   %   $��3��6   $��Search��68   $��80��6I   $��30��6T   ���#1565C0
�������$��0q�5)   =    � 	Resources 5*       �� 
TargetType	 �� Property
 �� Value��%^   '��       �� 5+      " ��6   .�5,      �� 	 �@��CornerRadius$
 3��69   5-      5.      5/      50      &   System.Windows.Controls.DataGrid  53   
   -   5          Name$ dgHouseholds��53      $��2��6)      AutoGenerateColumns$ False��66      
IsReadOnly$
 True��6R   $��0,0,0,15q�54         SelectionMode$ Single=�6%      Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *HouseholdId+6Q     57      $ Head of Household��6%   $	 150 6h    6@   �� *HeadOfHouseholdName+6h     58      $ Address��6%   $ * 6R    66   �� *	Address+6R     59      $ Created Date��6%   $	 120 6[    6;   �� *CreatedDate+6[     5:      $ Members Count��6%   $	 100 6]    6<   �� *MembersCount+6]   4  .System.Windows.Controls.DataGridTemplateColumn  5;      $ Actions��6)   $	 120 6:      CellTemplate 5<      �� 5=      .R�5>      �� -   5        $��View Details��5>   %   $	��5,2q�6X   ���#1565C0
�������$��0q�5?   j   
 � Tag 5>   <   �� *HouseholdId+ 5@   "   ��%^   '��       �� 5A   &   " ��6,   .�5B   *   �� 	 �@��CornerRadius$
 2��6I   5C   '   5D   #   5E      5F      5H      5J      � 5M   
   $��3��6    5N      �� 5O      $
��Auto �6#   �� 5P      $��* �6#   �� 5Q      $
��Auto �6#   .+�5T      �� -   5        $ 	lblStatus��5T   (   $��0��6   $��Displaying 0 households��6;   $��Center=�6Z   �� 5V      -   5        $ 
btnRefresh��5V   %   $��2��6   $��Refresh��69   $��80��6K   $��30��6V   ���#E0E0E0$��0q�5W   *    5X      ��%^   '��       �� 5Y      " ��6   .�5Z      �� 	 �@��CornerRadius$
 3��69   5[      5\      5]      5^      5_      5`      