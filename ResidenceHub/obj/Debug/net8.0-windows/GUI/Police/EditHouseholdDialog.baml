   M S B A M L   `   `   `  ����  ResidenceHub1    +ResidenceHub.GUI.Police.EditHouseholdDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Edit Household��5   	   $	��500��6    $	��600��6-    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Edit Household��6!   $��20(�67   $
��Bold"�6E   $��0,0,0,15q�6W   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0,0,10,0q�6)   .+�5      �� $��Household Information��6   $
��Bold"�69   $��0,0,0,15q�6K   �� 5      $��Household ID��6   $��SemiBold"�60   $��0,0,0,5q�6F   �� 5      -   5         ��Name$ txtHouseholdId��5      $��30��62   $��0,0,0,15q�6>   $��5q�6P    �� 
IsReadOnly$
 True��6\   �� 5!      $��Head of Household��6   $��SemiBold"�65   $��0,0,0,5q�6K   �� 5"      -   5         ��Name$ cmbHeadOfHousehold��5"      $��30��67   $��0,0,0,15q�6C    �� DisplayMemberPath$ FullName��6U   �� 5$      $��Address��6   $��SemiBold"�6+   $��0,0,0,5q�6A   �� 5%      -   5        $ 
txtAddress��5%      $��60��6.   $��0,0,0,15q�6:   $��5q�6L   	 �� TextWrapping$
	 Wrap=�6X   �� 5'      $��Created Date��6   $��SemiBold"�60   $��0,0,0,5q�6F   �� 5(      -   5        $ txtCreatedDate��5(      $��30��62   $��0,0,0,15q�6>   $��5q�6P   $
 True��6\   �� 5*      -   5        
 ��Name$
 lblError��5*      
���  ��$
��Wrap=�6?   $��0,10,0,0q�6S   5+      � 5.      $��1��6   $��10,0,0,0q�6#    5/      �� 50      $
��Auto �6$   �� 51      $��* �6$   �� 52      $
��Auto �6$   �� 53      $��* �6$   .+�57      �� $��0��6   $��Current Members��6)   $
��Bold"�6@   $��0,0,0,5q�6R   &   System.Windows.Controls.DataGrid  59      -   5          Name$ dgCurrentMembers��59   (   $��1��6      AutoGenerateColumns$ False��6B      
IsReadOnly$
 True��6^      SelectionMode$ Single=�5:      $��0,0,0,10q�62      Columns 5;      0  *System.Windows.Controls.DataGridTextColumn  5<      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
FullName+6X     5=      $ Relationship��6-   $	 100 6d    6C   �� *Relationship+6d   4  .System.Windows.Controls.DataGridTemplateColumn  5>      $ Actions��61   $ 70 6B      CellTemplate 5?      �� 5@   "   .R�5A   &   �� -   5        $��Remove��5A   -   $	��3,1q�6W   ���#F44336
�������$��0q�5B   s   
 � Tag 5A   >   �� *
MemberId+ � 	Resources 5C   *    �� 
TargetType �� Property �� Value��%^   '��       �� 5D   .   " ��64   .�5E   2   ��  �@��CornerRadius$ 2��6Q   5F   /   5G   +   5H   '   5I   #   5K      5M      �� 5P      $��2��6   $��Add New Members��6)   $
��Bold"�6@   $��0,10,0,5q�6R   � 5R      $��3��6    5S      �� 5T      $��* �6(   �� 5U      $
��Auto �6(   .+�5X      �� -   5         ��Name$ lstAvailableResidents��5X   +   $��0��6    �� SelectionMode$ Multiple=�6J   ��5Y      �� 5Z      .R�5[   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5\   &   �� $��Center=�6P   $��0,0,5,0q�6k    d� 	IsChecked 6/   �� *
IsSelected+6k   �� 5]   &   $��Center=�6J   ��60   �� *
FullName+6J   5^   #   5_      5a      �� 5c      -	   5         ��Name$ btnAddMembers��5c   *   $��1��6   $��Add Selected Members��6A   $��30��6`   ���#4CAF50
�������$��0q�5d   E   $��0,10,0,0q�6Y    5e      ��%^   '��       �� 5f      " ��6$   .�5g   "   ��  �@��CornerRadius$ 3��6A   5h      5i      5j      5k      5l      5m      �� 5p   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��0,15,0,0q�6W   .+�5q      �� -
   5        $ btnSave��5q      $��Save Changes��6&   $	��120��6=   $��30��6I   ���#C62828
�������$��0q�5r   =   $��0,0,10,0q�6Q    5s      ��%^   '��       �� 5t      " ��6   .�5u      ��  �@��CornerRadius$ 3��69   5v      5w      5x      �� 5z      -   5        $ 	btnCancel��5z      $��Cancel��6(   $��80��69   $��30��6D   ���#E0E0E0$��0q�5{   *    5|      ��%^   '��       �� 5}      " ��6   .�5~      ��  �@��CornerRadius$ 3��69   5      5�      5�      5�      5�      5�      