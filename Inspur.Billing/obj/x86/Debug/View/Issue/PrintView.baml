   M S B A M L   `   `   `  ����I  EInspur.Billing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Inspur.Billing.View.IssueInspur.Billing.View.Issue  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   )    #Inspur.Billing.View.Issue.PrintView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Inspur.Billing.View.Issue     5   	   7date&clr-namespace:System;assembly=mscorlib     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	     � WindowStartupLocation$  CenterScreen=�5	       � Title$ Print��5
   	   $	��700��6   $	��550��6$    � DataContext 5   	   ��  �� Path$ Print�   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     CommandParameter$ Loaded��6@   	   Command	 6$   �� *	Command+6@   5        5   
   $ Unloaded��6   . 5        $ Unloaded��6@   	 6$   �� *	Command+6@   5      .��5      � 
 � RowDefinitions
 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Disabled=�6   $
��Auto=�6M   .��5      � $
��10,0q�6   
 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   �� 5!      $
��Auto �6$   �� 5"      $
��Auto �6$   �� 5#      $
��Auto �6$   �� 5$      $
��Auto �6$   �� 5%      $
��Auto �6$   �� 5&      $
��Auto �6$   �� 5'      $
��Auto �6$   �� 5(      $
��Auto �6$   �� 5)      $
��Auto �6$   �� 5*      $
��Auto �6$   .+�5,      ��   H_C_TitleTbkStyle#��[ 6   $��0,30,0,0q�6G   �� 6Z   $�Order Number: ��6^   �� 6v   �6z   �� *Credit.OrderNumber+6�   �� 5-      $��1��6     H_C_ContentTbkStyle#��[ 6F   ��6)   �� *CurrentTime+6F   � 5.      $��2��6   $
��10,0q�6$   
 5/      �� 50      $
��Auto �6(   �� 51      $
��Auto �6(   �� 52      $
��Auto �6(   �� 53      $
��Auto �6(   �� 54      $
��Auto �6(   �� 55      $
��Auto �6(   �� 56      $
��Auto �6(   .+�58      ��   BaseContentTbkStyle#��[ 6    �� 6N   $�POSID:��6R   �� 6b   �6f   �� *Credit.PosNumber+6�   �� 59        H_R_ContentTbkStyle#��[ 6    �� 6N   $�Cashier:��6R   �� 6d   �6h   �� *Credit.Cashier+6�   �� 5:      $��1��6    $��
Buyer TIN:��6-   #��[ 6?   �� 5;      $��1��6    #��[ 6O   ��6-   �� *Credit.Buyer.Tin+6O   �� 5<      $��2��6    $��Buyer Name:��6-   #��[ 6@   �� 5=      $��2��6    #��[ 6P   ��6-   �� *Credit.Buyer.Name+6P   �� 5>      $��3��6    $��Buyer Address:��6-   #��[ 6C   �� 5?      $��3��6    #��[ 6S   ��6-   �� *Credit.Buyer.Address+6S   �� 5@      $��4��6    $��
Buyer Tel:��6-   #��[ 6?   �� 5A      $��4��6    #��[ 6T   ��6-   �� *Credit.Buyer.TelPhone+6T   �� 5B      $��5��6    $��Invoice Code:��6-   #��[ 6B   �� 5C      $��5��6    #��[ 6J   ��6-   �� *InvoiceCode+6J   �� 5D      $��6��6    $��Invoice Number:��6-   #��[ 6D   �� 5E      $��6��6    #��[ 6L   ��6-   �� *InvoiceNumber+6L   5F      �� 5G      $��3��6     DotLineStyle#��[ 6$   $
��0,10q�6J   � 5H      $��4��6   
 5I      �� 5J      $
��Auto �6(   �� 5K      $
��Auto �6(   �� 5L      $
��Auto �6(   .+�5N      �� $��Items��6    #��[ 6-   &   System.Windows.Controls.DataGrid  5O      $��1��6     DataGridPrintStyle#��[ 6U      CanUserSortColumns .     
IsReadOnly . $
��0,10q�6�   ��6,   �� *Credit.Productes+   Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��61     DataGridColumnLeft1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 6X      Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	 6�      Binding 6?   �� *Name+6�     5S      $ Price��61   $ *	 6r     DataGridColumnRight# [ 5T   1    5S   @   �� *Price+ �� StringFormat$ {0:0.00}��5T   1     5U      $
 Qty.��61   $ *	 6Y   # [ 5V   1    5U   ?   �� *Count+5V   1     5Y      $ Amount��61   $ *	 6t   # [ 5Z   1    5Y   A   �� *Amount+$ {0:0.00}��5Z   1   5\      5_      �� 5`      $��5��6   #��[ 6$   $	��0,5q�6J   � 5a      $��6��6   
 5b      �� 5c      $
��Auto �6(   �� 5d      $
��Auto �6(   �� 5e      $
��Auto �6(   .+�5g      �� $��
Tax Amount��6    #��[ 62     5h      $��1��6   #��[ 6L    .  $
��0,10q�5i   :    . ��5h   ,   �� *	TaxList+ 5j        5k      $ Label��61    	 DataGridColumnCenter# [	 6`   $ *	 6�    6@   �� *TaxItemCode+6�     5l      $
 Name��61   # [	 6_   $ *	 6�    6?   �� *TaxItemDesc+6�     5m      $ Rate(%)��61   # [	 6^   $ *	 6�    6B   �� *	TaxRate+6�     5n      $ 
Tax Amount��61   # [ 6{   $ *	 6�    6E   �� *	TaxAmount+$ {0:0.00}��6�   5p      �� 5q      $��2��6    $��
Total Tax:��6-   #��[ 6?   �� 5r      $��2��6    #��[ 6e   ��6-   �� *TotalTaxAmount+$ {0:0.00}��6e   5s      �� 5t      $��7��6   #��[ 6$   $	��0,5q�6J   � 5u      $��8��6   
 5v      �� 5w      $
��Auto �6(   �� 5x      $
��Auto �6(   �� 5y      $
��Auto �6(   �� 5z      $
��Auto �6(   .+�5|      �� $��0��6    $��Total Amount:��6-   #��[ 6B   �� 5}      $��0��6    #��[ 6h   ��6-   �� *Credit.GrandTotal+$ {0:0.00}��6h   �� 5~      $��1��6    $��Payment Mode:��6-   #��[ 6B   �� 5      $��1��6    #��[ 6^   ��6-   �� *!Credit.SelectedPaymentType.Name+6^   �� 5�      $��2��6    $��Actual Payment:��6-   #��[ 6D   �� 5�      $��2��6    
 NoStateTbStyle#��[
 6�   $��Right=�5�      $	��100��6:   $��Right=�6F   ��5�   +   �� *	ActualPay+$ {0:0.00}�� �� UpdateSourceTrigger$ PropertyChanged=�5�   F   �� 5�      $��3��6    $��Change:��6-   #��[ 6<   �� 5�      $��3��6    #��[ 6]   ��6-   �� *Change+$ {0:0.00}��6]   5�      �� 5�      $��9��6   #��[ 6$   $
��0,10q�6J   � 5�      $��10��6   
 5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6    $
��TIN:��6-   #��[ 69   �� 5�      $��0��6    #��[ 6O   ��6-   �� *TaxPayerInfo.Tin+6O   �� 5�      $��1��6    $��Name:��6-   #��[ 6:   �� 5�      $��1��6    #��[ 6P   ��6-   �� *TaxPayerInfo.Name+6P   �� 5�      $��2��6    $��Address:��6-   #��[ 6=   �� 5�      $��2��6    #��[ 6S   ��6-   �� *TaxPayerInfo.Address+6S   �� 5�      $��3��6    $
��Tel:��6-   #��[ 69   �� 5�      $��3��6    #��[ 6T   ��6-   �� *TaxPayerInfo.Telphone+6T   �� 5�      $��4��6    $��Terminal ID:��6-   #��[ 6A   �� 5�      $��4��6    #��[ 6I   ��6-   �� *
TerminalID+6I   �� 5�      $��5��6    $��Fiscal Code:��6-   #��[ 6A   �� 5�      $��5��6    #��[ 6I   ��6-   �� *
FiscalCode+6I   5�      �� 5�      $��11��6   #��[ 6%   $
��0,10q�6K   �� 5�      $��12��6   $	��200��6@   $
��0,10q�6L   ��6&   �� *QrPath+6L   �� 5�      $��13��6   $Q��KDear sir madam,please keep the invoice properly so as to refunds & replaces��6*   $
��Wrap=�6�   #��[ 6�   �� 5�      $��14��6   $#��Thank You & Please Come Again��6*   #��[ 6S   $��0,10,0,0q�6�   5�      5�      �� 5�   
   $��14��6   $��Print��6   $��10,10q�6/   ��6>   �� *PrintCommand+5�      5�      