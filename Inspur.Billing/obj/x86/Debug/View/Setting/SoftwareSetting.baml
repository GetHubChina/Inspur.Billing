   M S B A M L   `   `   `  ����I  EInspur.Billing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Inspur.Billing.View.SettingInspur.Billing.View.Setting  5      1    +Inspur.Billing.View.Setting.SoftwareSetting   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:Inspur.Billing.View.Setting     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        � DataContext  5      ��  �� Path$ SoftwareSetting�   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     CommandParameter$ Loaded��6@      Command 6$   �� *	Command+6@   5      .��5      � $	��600��6   $	��430��6   $��Center=�6)   $��	0,100,0,0q�6F    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      	 � RowDefinitions	 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $	��100 �6   .+�5   
   �� $��Make:��6     H_R_M10_ContentTbkStyle#��[ 6!   �� 5   
   $��1��6     BaseContentTbkStyle#��[ 6I   ��6$   �� *PosInfo.CompanyName+6I   �� 5   
   $��1��6   $��Model:��6!   #��[ 6/   �� 5   
   $��1��6   $��1��6!   #��[ 6O   ��61   �� *PosInfo.Desc+6O   �� 5    
   $��2��6   $��Software Version:��6!   #��[ 6:   �� 5!   
   $��2��6   $��1��6!   #��[ 6R   ��61   �� *PosInfo.Version+6R   �� 5"   
   $��3��6   $��Release Time:��6!   #��[ 66   �� 5#   
   $��3��6   $��1��6!   #��[ 6r   ��61   �� *PosInfo.IssueDate+
 �� StringFormat$
 {0:yyyy-MM-dd}��6r   �� 5$   
   $��4��6   $��2��6   $	��100��62   $��30��6>   $��Cancel��6J   $��Center=�6[   $��SoftwareCancel��5%   :   $y�	Collapsed=�6\   ��6   �� *	Command+6\   5&      5'      