   M S B A M L   `   `   `  ����  ResidenceHub1    +ResidenceHub.GUI.Common.RegisterAccountView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$&   Residence Hub - Register Account��5   	   $	��580��62   $	��600��6?    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5	      � ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #E0F7FA��6    � Offset$ 0Q�6/   � 5      $ #80DEEA��6   $ 1Q�6/   5      .+�5   
   ��  �� CornerRadius$ 15��6   
�������$��60,40,60,40q�66    �� Padding$ 30q�6K   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 25Q�6#      ShadowDepth$ 8Q�63      Opacity$	 0.2Q�6C   ._�5      �� $
��Auto=�6   .��5      �� .+�5      �� $��CREATE ACCOUNT��6    $��24(�66   $
��Bold"�6D   $��Center=�6V   $��0,0,0,25q�6s   ���#00796B6�   �� 5      $��	Full Name��6    $��0,0,0,5q�61   $��SemiBold"�6B   ���#00796B6X   �� 5      -   5         ��Name$ txtFullName��5      $��35��63   $��8q�6?   $��0,0,0,15q�6K   ���#00796B$��1q�6s    � FocusVisualStyle 6�   Z� �� 5      $��Email Address��6    $��0,0,0,5q�65   $��SemiBold"�6F   ���#00796B6\   �� 5      -   5        $ txtEmail��5      $��35��60   $��8q�6<   $��0,0,0,15q�6H   ���#00796B$��1q�6p    6�   Z� �� 5!      $��Password��6    $��0,0,0,5q�60   $��SemiBold"�6A   ���#00796B6W   G� 5"      -   5         G�Name$ txtPassword��5"   "   $��35��67   $��8q�6C   $��0,0,0,15q�6O   ���#00796B$��1q�6w    6�   Z� �� 5%      $��Confirm Password��6    $��0,0,0,5q�68   $��SemiBold"�6I   ���#00796B6_   G� 5&      -   5        $ txtConfirmPassword��5&   "   $��35��6>   $��8q�6J   $��0,0,0,15q�6V   ���#00796B$��1q�6~    6�   Z� �� 5)      $��Address��6    $��0,0,0,5q�6/   $��SemiBold"�6@   ���#00796B6V   �� 5*      -   5        $ 
txtAddress��5*      $��35��62   $��8q�6>   $��0,0,0,15q�6J   ���#00796B$��1q�6r    6�   Z� �� 5-      $��Account Type��6    $��0,0,0,5q�64   $��SemiBold"�6E   ���#00796B6[   �� 5.      -   5         ��Name$ cmbRole��5.      $��35��60   $��8q�6<   $��0,0,0,15q�6H   ���#00796B$��1q�6p    6�   Z� .@�5/      �� $��Citizen��6'    �� 
IsSelected$
 True��69   �� 50      $��
AreaLeader��6'   �� 51      $��Police��6'   �� 52      $��Admin��6'   53      �� 56      -   5         ��Name$ chkTerms��56      $)��#I agree to the terms and conditions��61   $��0,5,0,20q�6_   ���#00796B6q   �� 59      -   5         ��Name$ btnRegister��59      $��REGISTER��62   $��45��6E   ���#00796B
�������$��0q�5:      $
��Bold"�61   $��Stretch=�6]   ��5;      �� 5<       �� 
TargetType" ��6$   .�5=   "   ��  �� Property � �� �� Value$
 10,0q�6<    �� Triggers 5>   "   P� 5?   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5@   *   ��  � �� �#004D406G   �� 5A   *    �@�Cursor$
 Hand��6C   5B   '   5D      5F      �� 5I      -	   5         ��Name$ lblError��5I       
���  ��$��Center=�6C   $��
20,15,20,0q�6`   $��14(�6t   $
��Wrap=�6�   �� 5L       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��0,20,0,0q�6W   .+�5M      �� $��Already have an account? ��6$   ���#4242426E   �� 5N      -
   5        $ txtLoginLink��5N   $   $��Login��6:   ���#00796B$��SemiBold"�6\    � Cursor$
 Hand��6�   5O      5P      5Q      5R      5S      5T      