   M S B A M L   `   `   `  ����  ResidenceHub6    0ResidenceHub.GUI.AreaLeader.SendNotificationView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Send Notifications��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Send Notifications��6!   $��24(�6;   $
��Bold"�6I   $��0,0,0,20q�6[   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   ���#E0E0E0$��1q�6;    �� Padding$ 20q�6O   $��0,0,10,0q�6\   ._�5      �� .+�5      �� $��New Notification��6    $
��Bold"�68   $��0,0,0,15q�6J   �� 5      $��Recipient Type��6    $��SemiBold"�66   $��0,0,0,5q�6L   �� 5       -   5         ��Name$ cmbRecipientType��5       $��30��69   $��0,0,0,15q�6E   .@�5!      �� $��All Residents in Area��6'    �� 
IsSelected$
 True��6G   �� 5"      $��Specific Resident��6'   �� 5#      $��Area Police��6'   5$      �� 5&      -   5         ��Name$ pnlSpecificResident��5&   !   $y�	Collapsed=�6>   .+�5'      �� $��Select Resident��6$   $��SemiBold"�6;   $��0,0,0,5q�6Q   �� 5(      -   5        $ cmbResidents��5(   #   $��30��69   $��0,0,0,15q�6E    �� DisplayMemberPath$ FullName��6W   5)      �� 5+      $��Message��6    $��SemiBold"�6/   $��0,0,0,5q�6E   �� 5,      -   5         ��Name$ 
txtMessage��5,      $	��150��62   	 �� TextWrapping$
	 Wrap=�6?   
 �� AcceptsReturn$

 True��6S   $��5q�6h   $��0,0,0,15q�6t   �� 5.      -   5         ��Name$ btnSend��5.      $��Send Notification��6.   $	��150��6J   $��35��6V   ���#1565C0
�������$��0q�5/   E   $��Right=�50       � 	Resources 51       �� 
TargetType �� Property �� Value��%^   '��       �� 52      " ��6$   .�53   "   ��  �@��CornerRadius$ 3��6A   54      55      56      �� 58      -   5         ��Name$ 	lblStatus��58       $��0,10,0,0q�63   
��� � �$��SemiBold"�6X   59      5:      �� 5=      $��1��6   ���#E0E0E0$��1q�6;   $ 20q�6O   $��10,0,0,0q�6\   ._�5>      �  5?      �� 5@      $
��Auto �6(   �� 5A      $��* �6(   �� 5B      $
��Auto �6(   .+�5E      �� $��0��6    $��Recent Notifications��6-   $
��Bold"�6I   $��0,0,0,15q�6[   �� 5G      -   5         ��Name$ lstNotifications��5G      $��1��68   $��0q�6E   ��5H      �� 5I      .R�5J   "   �� ���#E0E0E0$��0,0,0,1q�6?   $ 0,5,0,5q�6Y   $	��300��6k   ._�5K   &   �� .+�5L   *   �� $��SemiBold"�6S   ��64   �� *RecipientType+6S   �� 5M   *   $
��Wrap=�6M   $��0,5,0,5q�6a   ��64   �� *	Message+6a   �� 5N   *   $��11(�6N   ���#707070��64   �� *
SentDate+6\   5O   '   5P   #   5Q      5S      �� 5U      -   5        $ 
btnRefresh��5U   *   $��2��6   $��Refresh��6>   $��80��6P   $��30��6[   ���#E0E0E0$��0q�5V   2   $��Right=�6F   $��0,10,0,0q�5W       5X      ��%^   '��       �� 5Y      " ��6$   .�5Z   "   ��  �@��CornerRadius$ 3��6A   5[      5\      5]      5^      5_      5`      5a      5b      