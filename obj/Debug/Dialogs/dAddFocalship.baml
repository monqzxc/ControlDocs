   M S B A M L   `   `   `  ����F  BDocsControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:DocsControl.DialogsDocsControl.Dialogs  5   	   '    !DocsControl.Dialogs.dAddFocalship   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:DocsControl.Dialogs     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   
�������  � 
ResizeMode$  NoResize=�5
   	    � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	   $��Century Gothic)�5   	    � Title$ 
MainDialog��5   	   $��216.744��5   	   $��565.317��5   	    � WindowStyle$
 None=�5   	   .��5      � -   5         � DataContext 5   -   �� *	Focalship+ � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��40 �6   �� 5      $	��310 �6   .+�5   
   �� -   5         ��Name$ lblTitle��5      $��1��5      
�������
����� �$��25(�5       $��DemiBold"�5!      $��ADD FOCALSHIP��5"      �� 5$   
   $��2��6    �� Padding$ 1q�5%      
����� �	 �� CornerRadius$	 5��5'      $��2q�5(      $��10,5,10,187q�5)      ._�5+      �� -   5        $	��sp1��5,      $��2��5+      
�������$��0,0,0,7q�5.      .+�5/      �� -   5        
 ��Name$
 
txtAcronym��50      $��	15,45,0,0q�51      $
��Left=�52       %  !MaterialDesignFloatingHintTextBox#��[  54      $	��Top=�55       �� AcceptsReturn . $��14(�57      $	��235��58      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ ACRONYM*��59      $
��Auto��5:      ��53      �� *	Acronym+5:      �� 5;      -   5        $
 txtPosition��5<      $��280,-40,0,0q�5=      $
��Left=�5>      #��[  5@      $	��Top=�5A       . $��14(�5C      $	��235��5D      $ 
FOCALSHIP*��5E      $
��Auto��5F      ��5?      �� *FocalshipName+5F      5H      5J      �� 5L   
   $��2��6   
����������#EC4D37$��25(�5P      $��DemiBold"�5Q      $��FOCALSHIP DETAILS��5R      $��
0,13,0,259q�6)   �� 5U   
   $��2��6   ���#1D1B1B$��Right=�5W       �� Orientation$ 
Horizontal=�5Y      $	��565��6.   $��0,133q�6:   .+�5Z      �� -   5        $��btnSave��5\      $
��SAVE��5[      $��	345,0,0,0q�5]       " MaterialDesignRaisedDarkButton#��[ 5^      $	��100��5_       � ToolTip$
 SAVE��5`      5a      �� 5b      -   5        $��	btnCancel��5f      $��CANCEL��5d      $��10,0,0,0q�5e       $  MaterialDesignRaisedAccentButton#��[ 5g      $	��100��5h      $ CANCEL��5i      5j      5k      5m      5n      