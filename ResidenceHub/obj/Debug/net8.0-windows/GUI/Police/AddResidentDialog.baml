   M S B A M L   `   `   `  ����  ResidenceHub/    )ResidenceHub.GUI.Police.AddResidentDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Add New Resident��5   	   $	��450��6"   $	��500��6/    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Add New Resident��6!   $��20(�69   $
��Bold"�6G   $��0,0,0,15q�6Y   �� 5   
   $��1��6   $
��Auto=�6$   .��5      �� .+�5      �� $��	Full Name��6   $��SemiBold"�6-   $��0,0,0,5q�6C   �� 5      -   5         ��Name$ txtFullName��5      $��30��6/   $��0,0,0,15q�6;   $��5q�6M   �� 5      $��Email��6   $��SemiBold"�6)   $��0,0,0,5q�6?   �� 5      -   5        $ txtEmail��5      $��30��6,   $��0,0,0,15q�68   $��5q�6J   �� 5      $��Address��6   $��SemiBold"�6+   $��0,0,0,5q�6A   �� 5      -   5        $ 
txtAddress��5      $��60��6.   $��0,0,0,15q�6:   $��5q�6L    �� TextWrapping$
 Wrap=�6X   �� 5      $
��Role��6   $��SemiBold"�6(   $��0,0,0,5q�6>   �� 5       -   5         ��Name$ cmbRole��5       $��30��6,   $��0,0,0,15q�68   .@�5!      �� $��Citizen��6#    �� 
IsSelected$
 True��65   �� 5"      $��
AreaLeader��6#   �� 5#      $��Police��6#   5$      �� 5&      $��Password��6   $��SemiBold"�6,   $��0,0,0,5q�6B   G� 5'      -   5         G�Name$ txtPassword��5'      $��30��63   $��0,0,0,15q�6?   $��5q�6Q   �� 5)      $��Confirm Password��6   $��SemiBold"�64   $��0,0,0,5q�6J   G� 5*      -   5        $ txtConfirmPassword��5*      $��30��6:   $��0,0,0,15q�6F   $��5q�6X   �� 5,      -   5        	 ��Name$	 lblError��5,      
���  ��$
��Wrap=�6?   $��0,0,0,10q�6S   5-      5.      �� 51   
   $��2��6   
 �� Orientation$
 
Horizontal=�6"   $��Right=�6;   $��0,15,0,0q�6W   .+�52      �� -   5         ��Name$ btnSave��52      $
��Save��6&   $��80��65   $��30��6@   ���#C62828
�������$��0q�53   =   $��0,0,10,0q�6Q    � 	Resources 54       �� 
TargetType �� Property �� Value��%^   '��       �� 55      " ��6   .�56      ��  �@��CornerRadius$ 3��69   57      58      59      �� 5;      -	   5        $ 	btnCancel��5;      $��Cancel��6(   $��80��69   $��30��6D   ���#E0E0E0$��0q�5<   *    5=      ��%^   '��       �� 5>      " ��6   .�5?      ��  �@��CornerRadius$ 3��69   5@      5A      5B      5C      5D      5E      