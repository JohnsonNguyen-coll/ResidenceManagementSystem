   M S B A M L   `   `   `  ����  ResidenceHub0    *ResidenceHub.GUI.Police.AddHouseholdDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Add New Household��5   	   $	��400��6#   $	��500��60    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Add New Household��6!   $��20(�6:   $
��Bold"�6H   $��0,0,0,15q�6Z   �� 5   
   $��1��6   $
��Auto=�6$   .��5      �� .+�5      �� $��Head of Household��6   $��SemiBold"�65   $��0,0,0,5q�6K   �� 5      -   5         ��Name$ cmbHeadOfHousehold��5      $��30��67   $��0,0,0,15q�6C    �� DisplayMemberPath$ FullName��6U   �� 5      $��Address��6   $��SemiBold"�6+   $��0,0,0,5q�6A   �� 5      -   5         ��Name$ 
txtAddress��5      $��60��6.   $��0,0,0,15q�6:   $��5q�6L    �� TextWrapping$
 Wrap=�6X   �� 5      $ ��Initial Members (Optional)��6   $��SemiBold"�6>   $��0,0,0,5q�6T   �� 5      -   5         ��Name$ lstAvailableResidents��5      $	��100��69   $��0,0,0,5q�6F   	 �� SelectionMode$	 Multiple=�6W   ��5      �� 5      .R�5       �� 
 �� Orientation$
 
Horizontal=�6)   .+�5!   "   �� $��Center=�6L   $��0,0,5,0q�6g    d� 	IsChecked 6+   �� *
IsSelected+6g   �� 5"   "   $��Center=�6F   ��6,   �� *
FullName+6F   5#      5$      5&      �� 5'      $I��CNote: Selected residents will be added to the household as members.��6   $��Italic$�6g   $��11(�6z   $��0,0,0,15q�6�   �� 5)      -   5         ��Name$ lblError��5)      
���  ��$
��Wrap=�6?   $��0,0,0,10q�6S   5*      5+      �� 5.   
   $��2��6   $
 
Horizontal=�6"   $��Right=�6;   $��0,15,0,0q�6W   .+�5/      �� -   5         ��Name$ btnSave��5/      $
��Save��6&   $��80��65   $��30��6@   ���#C62828
�������$��0q�50   =   $��0,0,10,0q�6Q    � 	Resources 51       �� 
TargetType �� Property �� Value��%^   '��       �� 52      " ��6   .�53      ��  �@��CornerRadius$ 3��69   54      55      56      �� 58      -   5        $ 	btnCancel��58      $��Cancel��6(   $��80��69   $��30��6D   ���#E0E0E0$��0q�59   *    5:      ��%^   '��       �� 5;      " ��6   .�5<      ��  �@��CornerRadius$ 3��69   5=      5>      5?      5@      5A      5B      