   M S B A M L   `   `   `  ����I  EInspur.Billing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Inspur.Billing.View.SettingInspur.Billing.View.Setting  5      2    ,Inspur.Billing.View.Setting.ParameterSetting   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:Inspur.Billing.View.Setting     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        � DataContext  5      ��  �� Path$ ParameterSetting�   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     CommandParameter$ Loaded��6@      Command 6$   �� *	Command+6@   5      .��5      � $	��600��6   $	��430��6   $��Center=�6)   $��	0,100,0,0q�6F    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      $
�� 120 �6   	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��
E-SDC URL:��6     H_R_M10_ContentTbkStyle#��[ 6&   �� 5   
   $��0��6   $��1��6     M_3_P_3_TbStyle#��[ 6G   $��Center=�6p   ��6/   �� *SdcUrl+{�6�   �� *IsParameterEnable+�� 5   
   $
��Test��6   $��2��6    $	��100��60   $	��0,3q�6<   $��SDCTest��6e   ��6I   �� *	Command+6e   �� 5   
   $��1��6   $��Printer Serial Port:��6!   #��[ 6=   �� 5   
   $��1��6   $��1��6   #��[ 6J   $��Center=�6s   ��6/   �� *	PrintPort+{�6�   �� *IsParameterEnable+�� 5    
   $
��Test��6   $��1��6    $��2��6-   $	��100��6=   $��PrinterPortTest��6e   $	��0,3q�6�   ��6I   �� *	Command+6�   �� 5!   
   $��2��6   $��3��6"   $��Center=�66   $	��Top=�6S   
 �� Orientation$
 
Horizontal=�6k   $��0,60,0,0q�6�   .+�5"      �� $	��100��6   $
��Edit��6!   ��60   �� *NetSettingEditCommand+�� 5#      $	��100��6   $
��Save��6!   $
��30,0q�6Z   ��60   �� *NetSettingSaveCommand+6Z   �� 5$      $	��100��6   $��Cancel��6!   ��62   �� *NetSettingCancelCommand+5%      5&      5'      