   M S B A M L   `   `   `  ����E  APlannerHRN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PlannerHRN
PlannerHRN  5   	       PlannerHRN.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PlannerHRN     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �  � Orientation$ Vertical=�6   $��Center=�6+   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5        $��Add Rand Process��5      $	��104��64   $��40��6@   6L   �� 5      -   5         ��Name$ textBox1��5      $��40��6,    �� TextWrapping$
 Wrap=�68   $	��124��6L   6X   �� 5      -   5        $��Add Given Process��5      $	��106��65   $��40��6A   6M   5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ 
myDataGrid��5   j   $��5q�6   $
��Auto��6"   $
��Auto��60   5      �� 5      $ 
Horizontal=�6   $��Center=�62   .+�5      �� -   5        $��40��5      $��20��6'   $
��FCFS��66   6E   �� 5      -   5        $��40��5      $��20��6'   $	��HRN��66   6D   5      5      5      5      