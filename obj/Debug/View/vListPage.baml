   M S B A M L   `   `   `  ����F  BDocsControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DocsControl.ViewDocsControl.View  5      &     DocsControl.ViewModel.vmListPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:DocsControl.View     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $��Century Gothic)�5      $	��856��5      $
��1034��5      .��5      �   � RowDefinitions  5   
   �� 5      $��1 �6   �� 5      $��80 �6   �� 5      $��80 �6   �� 5      $	��800 �6   .+�5   
   �� $��1��6   .+�5      �� -   5        $��lblTitle��5      $��HATDOG��5      ���#008080$��55(�5      5      �� 5   
   -   5         ��Name$	 sp2��5      $��2��6"    �� Orientation$ 
Horizontal=�6/   .+�5       �� -   5        $��cmbName��5!      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ SUBJECT��5"       �� 
IsEditable . $
��Left=�5$       &  "MaterialDesignFloatingHintComboBox#��[  5%      $	��362��6M   $��
30,10,0,26q�5&      5'      �� 5)      -   5        $��cmbItem��5,      $ DESCRIPTION��5-       . $
��Left=�5/      #��[  50      $	��362��6M   $��
30,10,0,26q�51      52      54      �� 56   
   $��3��6   $	��685��6"   $	��Top=�6/   .+�57      &   System.Windows.Controls.DataGrid  -   5          Name$ 	dgDocList��5;      
�������$	��640��59         
IsReadOnly .    AlternatingRowBackground
 �����   AutoGenerateColumns .  $	   HorizontalScrollBarVisibility$	 Disabled=�5>      ��5?      �� 
 �� Path$
 NewDirectories�   Columns 5@      4  .System.Windows.Controls.DataGridTemplateColumn  5A      ,  &System.Windows.Controls.DataGridColumn   Header$  NAME��5B         MinWidth$	 300Q�5C         Width5  /System.Windows.Controls.DataGridLengthConverter$	 400 5D         CellTemplate 5E      �� 5F      .R�5G   "   �� $��Center=�5H   !   ��5I   !   �� $

 Name�5J      5L        5N      $  DESCRIPTION��5O      $	 440Q�5P      $	 500 5Q       5R      �� 5S      .R�5T   "   �� $��Center=�5U   !   ��5V   !   �� $
 Description�5W      5Y        5[       5\      �� 5]      .R�5^   "   �� -   5         � ToolTip$ SEE DETAILS��5_   %   $��90��5a   %   $
��VIEW��5c   %    $  MaterialDesignRaisedAccentButton#��[ 5d   %   
 � Tag 5b   %   �� $
 Id�5d   %   5e      5g       � 	Resources 5j      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn �� 
TargetType �� Property �� Value��%�   '        7 �� 5k      8   "  6U   .�5l      ��  � �� �#30313467   �� 5m       � ��
 �����67   �� 5n       � ��$
 Bold"�67   5o      5p      5q      �� 5s      -   5         ��Name$ btnAdd��5u      $��950,-100,0,0q�5v       & "MaterialDesignFloatingActionButton#��[ 5w      $ ADD NEW DOCUMENT��5x      .��5y      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�5z      $��24��5{      $��24��5|      5}      5~      5      5�      