   M S B A M L   `   `   `  ����  ResidenceHub8    2ResidenceHub.GUI.AreaLeader.HouseholdDetailsDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Household Details��5   	   $	��450��6#   $	��600��60    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Household Details��6!   $��20(�6:   $
��Bold"�6H   $��0,0,0,15q�6Z   �� 5   
   $��1��6   ���#E0E0E0$��1q�64    �� Padding$ 15q�6H   ���#F5F5F5$��0,0,0,15q�6j   ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   �� 5      $��* �6'    5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   .+�5#      �� $��0��6   $��0��6)   $��Household ID:��69   $��SemiBold"�6N   $��0,0,10,5q�6d   �� 5$      -   5         ��Name$ lblHouseholdId��5$   9   $��0��6   $��1��6)   $��12345��6Q   $��0,0,20,5q�6^   �� 5&      $��0��6   $��2��6)   $��Created Date:��69   $��SemiBold"�6N   $��0,0,10,5q�6d   �� 5'      -   5        $ lblCreatedDate��5'   9   $��0��6   $��3��6)   $��
2023-01-01��6Q   $��0,0,0,5q�6c   �� 5)      $��1��6   $��0��6)   $��Head of Household:��69   $��SemiBold"�6S   $��0,0,10,5q�6i   �� 5*      -   5        $ lblHeadOfHousehold��5*   M   $��1��6   $��1��6)   $��3��69   $��
John Smith��6i   $��0,0,0,5q�6{   �� 5,      $��2��6   $��0��6)   $��Address:��69   $��SemiBold"�6I   $��0,0,10,5q�6_   �� 5-      -   5        $ 
lblAddress��5-   M   $��2��6   $��1��6)   $��3��69   $��123 Main St��6a   $
��Wrap=�6t   $��0,0,0,5q�6�   5.      5/      � 52   
   $��2��6    53      �� 54      $
��Auto �6    �� 55      $��* �6    .+�58      �� $��0��6   $��Household Members��6%   $
��Bold"�6>   $��0,0,0,10q�6P   &   System.Windows.Controls.DataGrid  5:      -   5          Name$ 	dgMembers��5:      $��1��6*      AutoGenerateColumns$ False��67   	   
IsReadOnly$
	 True��6S   
   SelectionMode$
 Single=�6e      Columns 5;      0  *System.Windows.Controls.DataGridTextColumn  5<      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
FullName+6T     5=      $ Relationship��6)   $	 100 6`    6?   �� *Relationship+6`     5>      $ Email��6)   $	 150 6R    68   �� *Email+6R     5?      $ Address��6)   $ * 6V    6:   �� *	Address+6V   5A      5B      �� 5E   
   -   5        $��3��5E      $��Close��6   $��80��6.   $��30��69   ���#1565C0
�������$��0q�5F   9   $��Right=�5G      $��0,15,0,0q�6-    � 	Resources 5H       �� 
TargetType �� Property �� Value��%^   '��       �� 5I      " ��6   .�5J      ��  �@��CornerRadius$ 3��65   5K      5L      5M      5N      5O      