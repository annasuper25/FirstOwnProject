   M S B A M L   `   `   `  ����  sbyt_WpfApplication7    1sbyt_WpfApplication.Input_Docs.Waybill_out_In_doc   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $��92 �6   �� 5      $
��358* �6   .+�5   
   �� -   5        $��
textBlock1��5   N   $��23��6   $
��Left=�6    $��	23,23,0,0q�6;   $��y Введите год и месяц, на который необходимо сформировать документ:��6`   $	��Top=�6�   $	��470��6�   $��14(�6�   �� 5   
   -   5        $��button1��5   c   $��Сформировать��6   $��35��6(   $
��Left=�64   $��
286,49,0,0q�6O   $	��Top=�6r   $	��103��6�   6�   �� 5   
   -   5        $��
textBlock2��5   M   $��23��6   $
��Left=�6    $��23,5,0,0q�6;   $V��PНакладная на отпущенную продукцию за месяц:��6_   $	��Top=�6�   $	��311��6�   $��Italic$�6�   $��Normal"�6�   $��1��6�   �� 5   
   -   5        $��	comboBox1��5   N   $��20��6   $
��Left=�6    $��	48,56,0,0q�6;   $	��Top=�6�   $��66��6�   5      �� 5   
   -   5        $��
textBlock5��5   u   $��Normal$�6   $��Normal"�6'   $��20��6;   $
��Left=�6G   $��	20,56,0,0q�6b   $��Год:��6�   $	��Top=�6�   $��33��6�   �� 5   
   -   5        $��	_ListView��5      $��1��6$   $��
5,27,89,46q�61    �� View 5      �� 5      P�5      �� 5      $��№ накладной ��6)    �� CellTemplate 5      �� 5   "   .R�5   &   �� ��60   �� *WAYBILL_OUT_ID+5    #   5"      �� 5%      $��№ договора ��6)    5&      �� 5'   "   .R�5(   &   �� ��60   �� *CONTRACT_ID+5)   #   5+      �� 5-      $��№ заявки��6)    5.      �� 5/   "   .R�50   &   �� ��60   �� *REQUEST_FOR_SHIPPING_ID+51   #   53      �� 55      $&��  Фактическая дата��6)    56      �� 57   "   .R�58   &   �� ��60   �� *FACT_DATE_OF_RELEASE+59   #   5;      �� 5=      $1��+Наименование заказчика��6)    5>      �� 5?   "   .R�5@   &   �� ��60   �� *CUSTOMER_NAME+5A   #   5C      �� 5E      $'��! Наименование ЖБИ ��6)    5F      �� 5G   "   .R�5H   &   �� ��60   �� *
JBI_NAME+5I   #   5K      �� 5M      $��Ед. изм.��6)    5N      �� 5O   "   .R�5P   &   �� ��60   �� *UNITS+5Q   #   5S      �� 5V      $�� Количество��6)    5W      �� 5X   "   .R�5Y   &   �� ��60   �� *AMOUNT+5Z   #   5\      �� 5_      $�� ИТОГО��6)    5`      �� 5a   "   .R�5b   &   �� ��60   �� *SUMMAAMOUNT+5c   #   5e      5h      5j      �� 5k   
   -   5        $��	comboBox2��5k   N   $��20��6   $
��Left=�6   $��
187,56,0,0q�6:   $	��Top=�6�   $��66��6�   �� 5l   
   -   5        $��
textBlock3��5l   v   $��Normal$�6   $��Normal"�6'   $��20��6;   $
��Left=�6G   $��
141,56,0,0q�6b   $�� Месяц:��6�   $	��Top=�6�   $��51��6�   5p      5q      