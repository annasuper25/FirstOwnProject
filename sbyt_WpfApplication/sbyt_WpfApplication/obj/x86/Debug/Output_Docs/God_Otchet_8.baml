   M S B A M L   `   `   `  ����  sbyt_WpfApplication2    ,sbyt_WpfApplication.Output_Docs.God_Otchet_8   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $��92 �6   �� 5      $
��358* �6   .+�5   
   �� -   5        $��
textBlock1��5   N   $��23��6   $
��Left=�6    $��	23,23,0,0q�6;   $q��k Введите год, на который необходимо сформировать документ:��6`   $	��Top=�6�   $	��470��6�   $��14(�6�   �� 5   
   -   5        $��textBox2��5   M   $	��162��6   $
��Left=�6   $��	23,80,0,0q�6:   $	��Top=�6]   $	��665��6u   $��1��6�   �� 5   
   -   5        $��button1��5   c   $��Сформировать��6   $��35��6(   $
��Left=�64   $��
260,52,0,0q�6O   $	��Top=�6r   $	��103��6�   6�   �� 5   
   -   5        $��
textBlock2��5   N   $��23��6   $
��Left=�6    $��	23,13,0,0q�6;   $Z��T Годовой отчет о реализации ЖБИ по заказчикам:��6`   $	��Top=�6�   $	��311��6�   $��Italic$�6�   $��Normal"�6�   $��1��6�   �� 5   
   -   5        $��	comboBox1��5   N   $��20��6   $
��Left=�6    $��	94,59,0,0q�6;   $	��Top=�6�   $��66��6�   5      �� 5   
   -   5        $��
textBlock5��5   u   $��Normal$�6   $��Normal"�6'   $��20��6;   $
��Left=�6G   $��	59,59,0,0q�6b   $�� Год:��6�   $	��Top=�6�   $��51��6�   �� 5   
   -   5        $��	_ListView��5      $��1��6$   $��
5,42,12,30q�61    �� View 5      �� 5      P�5      �� 5      $1��+Наименование заказчика��6)    �� CellTemplate 5      �� 5   "   .R�5    &   �� ��60   �� *CUSTOMER_NAME+5!   #   5#      �� 5%      $.��(Объем реализации, руб.��6)    5&      �� 5'   "   .R�5(   &   �� ��60   �� *	AllSchets+5)   #   5+      �� 5-      $&�� Суммы оплаты, руб.��6)    5.      �� 5/   "   .R�50   &   �� ��60   �� *ALLPayments+51   #   53      �� 55      $��Разница, руб.��6)    56      �� 57   "   .R�58   &   �� ��60   �� *
AllRaznica+59   #   5;      5>      5@      5B      5C      