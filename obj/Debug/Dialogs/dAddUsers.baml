   M S B A M L   `   `   `  ����F  BDocsControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DocsControl.DialogsDocsControl.Dialogs  5   	   #    DocsControl.Dialogs.dAddUsers   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:DocsControl.Dialogs     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   
�������  � 
ResizeMode$  NoResize=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	   $��Century Gothic)�5   	    � Title$ 
MainDialog��5   	   $��401.744��5   	   $��565.317��5   	    � WindowStyle$
 None=�5   	   .��5      � -   5         � DataContext 5   -   ��  �� Path$ Users� � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��40 �6   �� 5      $	��310 �6   �� 5      $	��427 �6   .+�5   
   �� -   5         ��Name$ lblTitle��5      $��1��5      
�������
����� �$��25(�5      $��DemiBold"�5       $��ADD USER��5!      �� 5#   
   $��2��6   	 �� Padding$	 1q�5$      
����� �
 �� CornerRadius$
 5��5&      $��2q�5'      $��10q�5(      ._�5*      �� -   5        $	��sp1��5*   (   $��2��6   
�������$��0,0,0,26q�5,      .+�5-      �� -   5         ��Name$ txtUsername��5.      $��	15,45,0,0q�5/      $
��Left=�50       %  !MaterialDesignFloatingHintTextBox#��[  52      $	��Top=�53       �� AcceptsReturn . $��14(�55      $	��235��56      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	USERNAME*��57      $
��Auto��58      ��51      �� *
UserName+58      �� 59      -   5        $ txtPassword��5=      #��[  5;      
 � Tag$; 5if is not empty, then change password. otherwise. not��5<      $��280,-40,0,0q�5>      $
��Left=�5?      $��14(�5@      $	��235��5A      $ 	PASSWORD*��5B      $��36��5C      ��5:      �� *
Password+5C      �� 5D      -   5        $ txtFirstName��5E      $��	15,10,0,0q�5F      $
��Left=�5G      #��[  5I      $	��Top=�5J       . $��14(�5L      $	��235��5M      $ FIRST NAME*��5N      $
��Auto��5O      ��5H      �� *	FirstName+5O      �� 5P      -   5        $ txtLastName��5Q      $��280,-40,0,0q�5R      $
��Left=�5S      #��[  5U      $	��Top=�5V       . $��14(�5X      $	��235��5Y      $ 	LASTNAME*��5Z      $
��Auto��5[      ��5T      �� *
LastName+5[      �� 5]      -   5        $��cmbSex��5d       & "MaterialDesignFloatingHintComboBox#��[ 5`      $
��Left=�5a      $	��Top=�5b      $��14(�5c      $
 SEX*��5e      $��	15,10,0,0q�5f      $
��Auto��5g      $	��235��5h       �� SelectedIndex 5_      �� $	 Sex�.@�5i      �� .��6#   Male5k      �� 5l      .��6#   Female5n      5o      �� 5q      -	   5        $ txtNickName��5r      $��280,-40,0,0q�5s      $
��Left=�5t      #��[  5v      $	��Top=�5w       . $��14(�5y      $	��235��5z      $ 	NICKNAME*��5{      $
��Auto��5|      ��5u      �� *
NickName+5|      �� 5      -
   5        $��cmbRole��5�      #��[ 5�      $
��Left=�5�      $	��Top=�5�      $��14(�5�      $ ROLE*��5�      $��	15,10,0,0q�5�      $
��Auto��6(   $	��235��66    5�      �� $ RoleID�.@�5�      �� .��6#   Super Admin5�      �� 5�      .��6#   Admin5�      �� 5�      .��6#   User5�      5�      5�      5�      �� 5�   
   $��2��6   
����������#EC4D37$��25(�5�      $��DemiBold"�5�      $��USER DETAILS��5�      $��
0,20,0,252q�6$   �� 5�   
   $��3��6   ���#1D1B1B$��Right=�5�       �� Orientation$ 
Horizontal=�5�      $	��565��6.   $��	0,2,0,375q�6:   .+�5�      �� -   5        $��btnSave��5�      $
��SAVE��5�      $��	345,0,0,0q�5�       " MaterialDesignRaisedDarkButton#��[ 5�      $	��100��5�       � ToolTip$
 SAVE��5�      5�      �� 5�      -   5        $��	btnCancel��5�      $��CANCEL��5�      $��10,0,0,0q�5�       $  MaterialDesignRaisedAccentButton#��[ 5�      $	��100��5�      $ CANCEL��5�      5�      5�      5�      5�      