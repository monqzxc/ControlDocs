   M S B A M L   `   `   `  ����F  BDocsControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DocsControl.DialogsDocsControl.Dialogs  5   	   $    DocsControl.Dialogs.dAddFocals   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:DocsControl.Dialogs     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   
�������  � 
ResizeMode$  NoResize=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	   $��Century Gothic)�5   	    � Title$ 
MainDialog��5   	   $��401.744��5   	   $��565.317��5   	    � WindowStyle$
 None=�5   	   .��5      � -   5         � DataContext 5   ,   ��  �� Path$ Focal� � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��40 �6   �� 5      $	��310 �6   �� 5      $	��427 �6   .+�5   
   �� -   5         ��Name$ lblTitle��5      $��1��5      
�������
����� �$��25(�5      $��DemiBold"�5       $��	ADD FOCAL��5!      �� 5#   
   $��2��6   	 �� Padding$	 1q�5$      
����� �
 �� CornerRadius$
 5��5&      $��2q�5'      $��10q�5(      ._�5*      �� -   5        $	��sp1��5+      $��2��5*      
�������$��0,0,0,7q�5-      .+�5.      �� -   5         ��Name$ txtFullName��5/      $��	15,45,0,0q�50      $
��Left=�51       %  !MaterialDesignFloatingHintTextBox#��[  53      $	��Top=�54       �� AcceptsReturn . $��14(�56      $	��235��57      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
FULL NAME*��58      $
��Auto��59      ��52      �� *
FullName+59      �� 5:      -   5        $ txtNickname��5;      $��280,-40,0,0q�5<      $
��Left=�5=      #��[  5?      $	��Top=�5@       . $��14(�5B      $	��235��5C      $ 	NICKNAME*��5D      $
��Auto��5E      ��5>      �� *
NickName+5E      �� 5F      -   5        $ txtEmail��5G      $��	15,10,0,0q�5H      $
��Left=�5I      #��[  5K      $	��Top=�5L       . $��14(�5N      $	��235��5O      $ EMAIL*��5P      $
��Auto��5Q      ��5J      �� *Email+5Q      �� 5R      -   5        $ 
txtContact��5S      $��280,-40,0,0q�5T      $
��Left=�5U      #��[  5W      $	��Top=�5X       . $��14(�5Z      $	��235��5[      $ CONTACT NUMBER��5\      $
��Auto��5]      ��5V      �� *ContactNumber+5]      �� 5_      -   5        $��cmbPosition��5e       & "MaterialDesignFloatingHintComboBox#��[ 5a      $
��Left=�5b      $	��Top=�5c      $��14(�5d      $ POSITION��5f      $��	15,10,0,0q�5g      $
��Auto��6(   $	��235��66    �� SelectedIndex 5`      �� $ PlantillaID�.@�5h      �� .��6#   MALE5j      �� 5k      .��6#   FEMALE5m      5n      �� 5p      -	   5        $��cmbFocalship��5v      #��[ 5r      $
��Left=�5s      $	��Top=�5t      $��14(�5u      $ 	FOCALSHIP��5w      $��280,-40,0,0q�5x      $
��Auto��6/   $	��235��6=    5q      �� $ FocalshipID�.@�5y      �� .��6#   MALE5{      �� 5|      .��6#   FEMALE5~      5      �� 5�      -
   5        $��	cmbOffice��5�      #��[ 5�      $
��Left=�5�      $	��Top=�5�      $��14(�5�      $ OFFICE��5�      $��	15,10,0,0q�5�      $
��Auto��6(   $	��235��66    5�      �� $ OfficeID�.@�5�      �� .��6#   Super Admin5�      �� 5�      .��6#   Admin5�      �� 5�      .��6#   User5�      5�      5�      5�      �� 5�   
   $��2��6   
����������#EC4D37$��25(�5�      $��DemiBold"�5�      $��FOCAL DETAILS��5�      $��
0,20,0,252q�6%   �� 5�   
   $��3��6   ���#1D1B1B$��Right=�5�       �� Orientation$ 
Horizontal=�5�      $	��565��6.   $��	0,2,0,375q�6:   .+�5�      �� -   5        $��btnSave��5�      $
��SAVE��5�      $��	345,0,0,0q�5�       " MaterialDesignRaisedDarkButton#��[ 5�      $	��100��5�       � ToolTip$
 SAVE��5�      5�      �� 5�      -   5        $��	btnCancel��5�      $��CANCEL��5�      $��10,0,0,0q�5�       $  MaterialDesignRaisedAccentButton#��[ 5�      $	��100��5�      $ CANCEL��5�      5�      5�      5�      5�      