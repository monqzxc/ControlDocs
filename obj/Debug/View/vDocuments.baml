   M S B A M L   `   `   `  ����F  BDocsControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DocsControl.ViewDocsControl.View  5      '    !DocsControl.ViewModel.vmDocuments   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:DocsControl.View     5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $��Century Gothic)�5      $	��856��5
   7   $
��1034��6D   .��5      � .+�5   
   � .+�5      �� $��	DOCUMENTS��5      ���#008080$��55(�5      $��2,0,674,776q�6   �� 5      -   5        $��lblTotalDocs��5      $��Total Documents��5      ���#008080$��14(�5      $
��Auto��5      $��731,805,20,10q�5      $��Right=�5      �� 5      -   5        $��icPersonnel��5   6   $
��Left=�6   $	��523��5      $��10,805,0,10q�5      ��5      ��   �� Path$  
NewButtons���5      �� 5       .R�5!      �  K� IsItemsHost . 6$   5"      5$      5%      � 5'   
   $��10,91,10,695q�6   .+�5(      �� -   5        $��
cmbSubject��5)      $��34,10,690,19q�5*      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ SUBJECT��5+       �� 
IsEditable . $
��Left=�5-       &  "MaterialDesignFloatingHintComboBox#��[  5.      5/      �� 51      -   5        $��
cmbDocType��52      $��335,9,493,20q�53      $ DOCUMENT TYPE��54       . $
��Left=�56      #��[  57      58      �� 5:      -   5        $��cmbControlNumber��5;      $��541,10,287,19q�5<      $ CONTROL NUMBER��5=       . $
��Left=�5?      #��[  5@      5A      (  "System.Windows.Controls.DatePicker  5C      -   5        $��dtpDate��5D      $ 
DATE ADDED��5E      $��743,9,143,19q�5F       ( $MaterialDesignFloatingHintDatePicker#��[ 5G      $��42��5H      �� 5I      -   5         � ToolTip$ SEARCH DOCUMENT��5J      $��SEARCH��5L      $
��Left=�5M      $��
890,18,0,0q�5N      $	��Top=�5O      $	��105��5P      5Q      &   System.Windows.Controls.DataGrid  5R   
   -   5          Name$ 	dgDocList��5W      $	��631��5S      $��
10,166,0,0q�5T      
�������   
IsReadOnly . $
��Left=�5X      $	��Top=�5Y         AutoGenerateColumns .  $   HorizontalScrollBarVisibility$ Disabled=�5[      $
��1014��5\      	   AlternatingRowBackground
	 �������5^      �� $  DocDatas�
   Columns
 5_      4  .System.Windows.Controls.DataGridTemplateColumn  5`      ,  &System.Windows.Controls.DataGridColumn   Header$  DATE ADD/RECEIVED��6*      MinWidth$	 100Q�5a         Width5  /System.Windows.Controls.DataGridLengthConverter$	 176 5b         CellTemplate 5c      �� 5d      .R�5e      �� $��Center=�5f   !   ��5g   !   �� $  DateAdd� �� StringFormat$ MMM dd, yyyy HH:mm tt��5h      5j        5k      $  DOCUMENT TYPE��6*   $ 15Q�5l      $	 150 5m       5n      �� 5o      .R�5p      ��  �� TextWrapping$
 Wrap=�5q   !   $��Center=�5r   !   ��5s   !   �� $  	DoctTypes�5t      5v        5x      $  SUBJECT��6)   $	 180Q�5y      $	 400 5z       5{      �� 5|      .R�5}      �� $��Center=�5~   !   $
 Wrap=�5   !   ��5�   !   �� $  
DocSubject�5�      5�        5�      $  CONTROL NUMBER��6)   $	 150Q�5�      $	 155 5�       5�      �� 5�      .R�5�      �� $��Center=�5�   !   $
 Wrap=�5�   !   ��5�   !   �� $  DocControlNumber�5�      5�        5�       5�      �� 5�      .R�5�      �� -	   5        $ SEE DETAILS��5�   !   $��90��5�   !   $
��VIEW��5�   !    $  MaterialDesignRaisedAccentButton#��[ 5�   !   
 � Tag 5�   !   �� $  Id�5�   !   5�      5�       � 	Resources 5�      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� BasedOn �� 
TargetType �� Property �� Value��%�   '        7 �� 5�      8   "  6Q   .�5�      ��  � �� �#30313463   �� 5�       � ��
 �����63   �� 5�       � ��$
 Bold"�63   5�      5�      5�      5�      5�      