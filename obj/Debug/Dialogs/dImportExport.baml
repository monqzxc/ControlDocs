   M S B A M L   `   `   `  ����F  BDocsControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DocsControl.DialogsDocsControl.Dialogs  5   	   '    !DocsControl.Dialogs.dImportExport   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:DocsControl.Dialogs     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   
�������  � 
ResizeMode$  NoResize=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	   $��Century Gothic)�5   	    � Title$ 
MainDialog��5   	   $��216.744��5   	   $��565.317��5   	    � WindowStyle$
 None=�5   	   .��5      � -   5         � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��40 �6   �� 5      $	��310 �6   .+�5   
   �� -   5         ��Name$ lblTitle��5      $��1��5      
�������
����� �$��25(�5       $��DemiBold"�5!      $��MANAGE FILE��5"      �� 5$   
   $��2��6    �� Padding$ 1q�5%      
����� � �� CornerRadius$ 5��5'      $��2q�5(      $��10,5,10,187q�5)      ._�5+      �� -   5        $	��sp1��5,      $��2��5+      
�������$��0,0,0,7q�5.      .+�5/      �� -   5        	 ��Name$	 	btnImport��50      $��	15,60,0,0q�51      $
��Left=�52      $	��Top=�53      $��14(�54      $	��235��55      )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 ACRONYM*��56      $��35��57      .��69   EXPORT EXCEL59      �� 5:      -   5        $	 	btnExport��5;      $��280,-36,0,0q�5<      $
��Left=�5=      $	��Top=�5?      $��14(�5A      $	��238��5B      $
 	POSITION*��5C      $��36��5D      .��69   IMPORT EXCEL6G   5F      5H      �� 5J   
   $��2��6   
����������#EC4D37$��25(�5N      $��DemiBold"�5O      $��CHOOSE ACTION��5P      $��
0,15,0,257q�6%   �� 5S   
   $��2��6   ���#1D1B1B$��Right=�5U       �� Orientation$ 
Horizontal=�5W      $	��565��6.   $��0,133q�6:   .+�5Y      �� -   5        $��	btnCancel��5]      $��CANCEL��5[      $��	455,0,0,0q�5\       $   MaterialDesignRaisedAccentButton#��[  5^      $	��100��5_       � ToolTip$ CANCEL��5`      5a      5b      5c      5d      