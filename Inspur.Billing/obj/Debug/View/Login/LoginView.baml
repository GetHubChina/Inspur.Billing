   M S B A M L   `   `   `  ����I  EInspur.Billing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Inspur.Billing.View.LoginInspur.Billing.View.Login  5   	   D @CommonLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullF1clr-namespace:CommonLib.Helper;assembly=CommonLibCommonLib.Helper 5   	   )    #Inspur.Billing.View.Login.LoginView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   3local'clr-namespace:Inspur.Billing.View.Login  5   	   >helper1clr-namespace:CommonLib.Helper;assembly=CommonLib  5   	   -   5        $��login��5	        � Title$  Login��5
   	   $	��400��6   $	��602��6$    � WindowStartupLocation$ CenterScreen=�60    � WindowStyle$
 None=�6U    � AllowsTransparency .  � 
ResizeMode$ CanMinimize=�5   M    � DataContext 6	   ��  �� Path$ Login�   Locator �� Source# [   � 	Resources 5        LogInImageStyle	 �� 
TargetType
 �� Property �� Value  LogInTextBoxStyle  ContentFontSize�� 5   
   %�  &	       &	 �     7  �� 5      "	 ��6,   .�5      �� 
 � ��$	 195��6*   �� 5      
 � ��$ 31��6+   �� 5      
 � ��$	 0,5q�6+   �� 5      
 � ��$
 Left=�68   5      �� 5      "	 ��6.   .�5      �� 
 � ��$
 Left=�68   �� 5      
 � ��$ Center=�66   �� 5      
 � ��$	 145��6*   �� 5      
 � ��$ 0q�64   �� 5      
 � ��$ 45,0,0,0q�6+   �� 5      
 � ��8   6-   5      5      .  (System.Windows.Interactivity.Interaction   Triggers 5       /  )System.Windows.Interactivity.EventTrigger  5!   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5"      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+   CommandParameter 5#   $   ��  �� ElementName$ login��5$      .��5&      �� ���	#FF45B9F1$��1q�6%   ._�5'   
   � -   5        $��1��5'       � ColumnDefinitions 5(      �� 5)      �� 5*      $
��Auto �6#   �� 5+       � RowDefinitions 5-      �� 5.      $
��Auto �6    �� 5/      $��2* �6    �� 50      $��* �6    �� 51      .+�54      �� $��1��6   $��3��6%   $
��Fill=�69   $4��./Inspur.Billing;component/Images/login_bg1.png��6H   �� 55      -   5        $��2��55      $��30��6%   $��30��60     CloseButtonStyle#���  6<   6g   � 57      $��1��6   $��1��6    $��0,10,0,0q�60    58      �� 59      �� 5:       5<      �� 5=      $	��100 �6'   �� 5>      .+�5@      �� $��
User Name:��6     BaseContentTbkStyle#��[ 6.   �� 5A      $��0��6   $��1��6%   $6��0/Inspur.Billing;component/Images/login_edit2.png��65   #��[ 6o   �� 5B      $��1��6   #��[ 6D   $��45,0,0,0q�6o   $��Center=�6�   $	��0,3q�6�   ��6*   �� *
UserName+6�   �� 5C      $��1��6   $��	Password:��6)   #��[ 6:   �� 5D      $��2��6   $��1��6%   $6��0/Inspur.Billing;component/Images/login_edit3.png��65   #��[ 6o   G� 5E      $��1��6   $��1��6+   (  "CommonLib.Helper.PasswordBoxAttach   IsPasswordBindingEnabled . $��Center=�6u   $	��0,3q�6�   $��45,0,0,0q�5G      $��0q�60   $	��145��6D      BindedPassword 5F      �� *
Password+ �� Mode$ TwoWay=�5G   D   5H      �� 5I      $��Login��6   $��2��6%   $��1��62   $��Center=�6B     MetroFlatButton#��[ 5J      $��40��6>   ��5I   ]   �� *LoginCommand+5J   >   �� 5K      $��Right=�6   $��Center=�64   $��3��6O   $��3��6\   $
��V1.1��6p   $��0,0,80,0q�6|   5L      5M      5N      