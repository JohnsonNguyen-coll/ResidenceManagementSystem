   M S B A M L   `   `   `  ����  ResidenceHub)    #ResidenceHub.GUI.Common.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Residence Hub - Login��5   	   $	��450��6'   $	��600��64    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5	   	   .��5
      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #E0F7FA��6    � Offset$ 0Q�6/   � 5      $ #80DEEA��6   $ 1Q�6/   5      .+�5   
   ��  �� CornerRadius$ 15��6   
�������$��80,10,80,10q�66    �� Padding$ 30q�6K   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 25Q�6#      ShadowDepth$ 8Q�63      Opacity$	 0.2Q�6C   ._�5      �� $��Center=�6   .+�5      �� $��RESIDENCE HUB��6   $��28(�61   $
��Bold"�6?   $��Center=�6Q   $��0,0,0,30q�6n   ���#00796B6�   �� 5      $��Email��6   $��0,0,0,5q�6)   $��SemiBold"�6:   ���#00796B6P   �� 5      -   5         ��Name$ txtEmail��5      $��35��6,   $��8q�68   $��0,0,0,20q�6D   ���#00796B$��1q�6l    � FocusVisualStyle 6�   Z� �� 5      $��Password��6   $��0,0,0,5q�6,   $��SemiBold"�6=   ���#00796B6S   G� 5      -   5         G�Name$ txtPassword��5      $��35��63   $��8q�6?   $��0,0,0,25q�6K   ���#00796B$��1q�6s    6�   Z� �� 5      -   5         ��Name$ btnLogin��5      $��LOGIN��6+   $��45��6;   ���#00796B
�������$��0q�5      $
��Bold"�6-   $��Stretch=�6V   ��5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � �� �� Value$
 10,0q�68    �� Triggers 5       P� 5!   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �5"   &   ��  � �� �#004D406C   �� 5#   &    �@�Cursor$
 Hand��6?   5$   #   5&      5(      �� 5)      -   5         ��Name$ lblError��5)      
���  ��$��Center=�6?   $��
20,15,20,0q�6\   $��14(�6p   �� 5,       �� Orientation$ 
Horizontal=�6   $��Center=�66   $��0,20,0,0q�6S   .+�5-      �� $��Don't have an account? ��6    ���#4242426?   �� 5.      -   5        $ txtRegister��5.       $��Register Account��65   ���#00796B$��SemiBold"�6b    � Cursor$
 Hand��6�   5/      50      51      52      53      