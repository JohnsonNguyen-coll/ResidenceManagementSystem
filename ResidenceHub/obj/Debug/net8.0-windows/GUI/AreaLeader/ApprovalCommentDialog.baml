   M S B A M L   `   `   `  ����  ResidenceHub7    1ResidenceHub.GUI.AreaLeader.ApprovalCommentDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Comments��5   	   $	��250��6   $	��400��6'    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 	lblPrompt��5   !   $��0��6   $��Enter comments:��64   $��SemiBold"�6K   $��0,0,0,10q�6a   �� 5   
   -   5         ��Name$ txtComments��5      $��1��6    �� TextWrapping$
 Wrap=�64    �� AcceptsReturn$
 True��6H   $��5q�6]   �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5      �� -   5        	 ��Name$	 	btnSubmit��5      $��Submit��6(   $��80��69   $��30��6D   ���#1565C0
�������$��0q�5   =   $��0,0,10,0q�6Q   
 � 	Resources
 5       �� 
TargetType �� Property �� Value��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 3��69   5      5      5      �� 5!      -   5        $	 	btnCancel��5!      $��Cancel��6(   $��80��69   $��30��6D   ���#E0E0E0$��0q�5"   *   
 5#      ��%^   '��       �� 5$      " ��6   .�5%      ��  �@��CornerRadius$ 3��69   5&      5'      5(      5)      5*      5+      