   M S B A M L   `   `   `  ����I  EInspur.Billing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Inspur.Billing.View.SettingInspur.Billing.View.Setting  5   	   )    #Inspur.Billing.View.Setting.PinView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Inspur.Billing.View.Setting     5   	     � WindowStyle$  
ToolWindow=�5       � Title$ Please Input PIN��5	   	   $	��120��6"   $	��400��6/    � WindowStartupLocation$ CenterScreen=�6;    � DataContext 5   	   ��  �� Path$	 Pin�   Locator �� Source# [  .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      $
��Auto �6   .+�5   
   �� $��30��6     M_3_P_3_TbStyle#��[ 6   $	��Top=�6\   $��	10,20,0,0q�6t   ��6G   �� *Pin+6t   �� 5   
   $��1��6   $��Confirm��6!   $��30��63   $��
10,20,10,0q�6?   $	��Top=�6S   ��6k   �� *ConfirmCommand+�� 5   
   $��2��6   $����Attention:  E-SDC's PIN code is not entered or the connection has become invalid. Please reenter the PIN code and establish a connection with the E-SDC.��6(   
���  ��$��Bottom=�5      $��10(�6/   $
��Wrap=�6=   6S   5      5      