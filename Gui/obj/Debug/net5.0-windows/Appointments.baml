   M S B A M L   `   `   `  ����>  :Gui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GuiGui  5   	       Gui.Appointments   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:Gui     5   	     � Title$  Appointments��5   	   $	��400��6   $	��800��6+   .��5
      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ AppointmentBox��5         
IsReadOnly$
 True��6[      AutoGenerateColumns$ False��6m   $��0,0,10,0q�6�   ��6+   �� *CleaningAppointmentList+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Appointment Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6g   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *CleaningAppointmentId+6g     5      $ Customer Id��6%   $ 85 6Y    6:   �� *
CustomerId+6Y     5      $ Company��6%   $ 65 6R    66   �� *	Company+6R     5      $ 
Hour Price��6%   $	 100 6W    69   �� *	HourPrice+6W     5      $ Appointment Description��6%   $	 400 6k    6G   �� *AppointmentDesc+   ElementStyle 5      �� 5      .�5      �� 	 �� Property	 � ��
 �� Value$

 Wrap=�6G   5      5      5      �� 5   
   -   5        $
��Back��5      $
��Left=�6    $��10(�6;   $��	676,3,0,0q�6I   $	��Top=�6\   $��22��6t   $��64��6�   6�   �� 5   
   -   5        $��	Add Order��5      $
��Left=�6%   $��	583,3,0,0q�6@   $	��Top=�6S   $��22��6k   $��88��6w   6�   5      5      