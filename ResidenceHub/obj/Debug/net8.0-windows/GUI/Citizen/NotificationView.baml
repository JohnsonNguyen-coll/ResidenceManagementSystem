   M S B A M L   `   `   `  ����  ResidenceHub0    *ResidenceHub.GUI.Citizen.NotificationsView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Notifications��5      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,20q�6;   .+�5      �� $��Notifications��6   $��24(�6-   $
��Bold"�6;   �� 5      -   5         ��Name$ lblUnreadCount��5      $��
(3 unread)��60   $��16(�6B   $��Center=�6P   $��10,0,0,0q�6k   ���#2E7D326}   5      �� 5   
   -   5         ��Name$ lvNotifications��5      $��1��6,   $��0q�69   $��0,0,0,10q�6M    �� SelectionMode$ Single=�5      ��5      �� 5      .R�5      ��  �� Padding$ 10q�6   $��0,0,0,5q�6*    �� CornerRadius$ 5��6b   ��6;   �� *BackgroundColor+._�5      �  � ColumnDefinitions 5      �� 5   "   $��* �63   �� 5    "   $
��Auto �63   .+�5#      �� $��0��6)   .+�5$   "   �� $
��Wrap=�6E   $��0,0,0,5q�6Y   ��6,   �� *	Message+6Y   �� 5%   "   $ 
Horizontal=�6-   .+�5&   &   �� $��11(�6J   ���#707070��60   �� *
SentDate+6X   �� 5'   &   $�� • ��60   $��11(�6;   ���#707070$	��5,0q�6^   �� 5(   &   $��11(�6H   ���#707070��60   �� *Status+6V   5)   #   5*      �� 5,      -   5        $��1��5,   %   $��Mark as Read��65   $	��5,2q�6L   
������ ���#2E7D32$��1q�5.   T   ���#2E7D32y�5-   %   �� *MarkButtonVisibility+
	 � Tag	 5/   :   �� *NotificationId+6Y   50      51      52      54      �� 57   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   .+�58      �� -   5        
 ��Name$
 btnMarkAllAsRead��58      $��Mark All as Read��6/   $	��120��6J   $��35��6V   ���#2E7D32
�������$��0q�59   =   $��0,0,10,0q�6Q    � 	Resources 5:       �� 
TargetType �� Property �� Value��%^   '��       �� 5;      " ��6   .�5<      ��  �@��CornerRadius$ 3��69   5=      5>      5?      �� 5A      -   5        $
 
btnRefresh��5A      $��Refresh��6)   $��80��6;   $��35��6F   ���#E0E0E0$��0q�5B   *    5C      ��%^   '��       �� 5D      " ��6   .�5E      ��  �@��CornerRadius$ 3��69   5F      5G      5H      5I      5J      5K      