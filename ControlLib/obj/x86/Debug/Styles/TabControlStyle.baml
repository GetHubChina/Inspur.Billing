   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TabItem.Selected.Background  �� Color  TabItem.Selected.Border �� 
TargetType �� Property �� Value  Button.Background.Normal �� 
TargetType �Name J� SnapsToDevicePixels ��Name ��Name	 �� RelativeSource
 �� AncestorType �� Triggers ^� 
Conditions �� Binding �� Value �� 
TargetName  Button.Background.Mousover  Button.Background.Pressed-    'System.Windows.Input.KeyboardNavigation    TabNavigation � ColumnDefinitions ��Name � RowDefinitions ��Name ��Name K� IsItemsHost    TabIndex K� ZIndex    DirectionalNavigation P� Property P� Value�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      MoptionsAhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/options  5      %  &	        &	 !     '�� B     '�� /    7  �� 5      $  #FFFFFF��6:   �� 5      $  #ACACAC��66   �� 5      " ��6   .�5   
   ��  � ��
 �   �6'   �� 5	   
    � ��# �  6'   �� 5
   
    � ��
 ���� 6(   �� 5   
    � ��$ 0q�6#   �� 5   
    � ��$	 6,5q�6$   �� 5   
    � ��$ Stretch=�67   �� 5   
    � ��$ Stretch=�65   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      � $ templateRoot��6    . .+�5      �� $ 
mainBorder��6!   #��z��65   $��0q�6a   #��z��6u   $��0q�6�   5      �� 5      $ contentPresenter��6+   $��Header��6E   |�.  #��z��5   +   ��. # z 6i   ��5   +   �� *HorizontalContentAlignment+	 �� "
 ����5   +   �� *VerticalContentAlignment+	 �� "
 ��5       5      ^� 5       5      �� 5   "    6,   �� *IsMouseOver+	 �� *Self+ true6r   �� 5   "    6,   �� *TabStripPlacement+	 �� "
 �� Left6�   .0�5       �� $ 
mainBorder��6;   # �  6S    � ��5!      ^� 5"       5#      �� 5$   "    6,   �� *
IsSelected+	 �� *Self+	 false6q   �� 5%   "    6,   �� *TabStripPlacement+	 �� "
 �� Left6�   .0�5'      �� $ 
mainBorder��6;   # �  6S    � ��5(      ^� 5)       5*      �� 5+   "    6,   �� *
IsSelected+	 �� *Self+ true6q   �� 5,   "    6,   �� *TabStripPlacement+	 �� "
 �� Left6�   .0�5.      �� $ 
mainBorder��6;   # �  6S    � ��5/      51      53      54      �� 56      " ��6   .�57   
   ��  � ��$ 2q�6$   �� 58   
    � ��$ Center=�67   �� 59   
    � ��$ Center=�65   �� 5:   
    � ��
 �����6'   �� 5;   
    � ��8   6(   �� 5<   
    � ��$ 1q�6,   �� 5=   
    � ��# � ��6'   �� 5>   
    � �� 5?      �� 5@      " ��6"   .R�5A      � $ templateRoot��6   }�.  . $ Local=�6`    5B      �� 5C      $ ColumnDefinition0��6/   �� 5D      $ ColumnDefinition1��6/   $��0 �6J    5F      �� 5G      $ RowDefinition0��6,   $
��Auto �6D   �� 5H      $ RowDefinition1��6,   $��* �6D   .+�5J      �� $ headerPanel��6#   
������ $��0��6Q    . $��2,0,2,0q�6t   $��0��6�   $ 1��6�   $ 1��6�   �� 5K      $ contentPanel��6!   #��z��67   #��z��6c   #��z��6�   $��0��6�   $ 	Contained=�6�   $��1��6  $ 2��6  $ Local=�63  ._�5L      �� $ PART_SelectedContentHost��6/   $��SelectedContent��6Q   #��z��6q   # z 6�   5M      5N       5O      P� 5P       �@��TabStripPlacement$
 Left=�6?   . �5Q      �� $ headerPanel��69    � ��$ 0��6R   �� 5R      $ contentPanel��69    � ��$ 0��6S   �� 5S      $ headerPanel��6<    � ��$ 0��6U   �� 5T      $ contentPanel��6<    � ��$ 1��6V   �� 5U      $ ColumnDefinition0��66    � ��$
 Auto �6U   �� 5V      $ ColumnDefinition1��66    � ��$ * �6U   �� 5W      $ RowDefinition0��67    � ��$ * �6S   �� 5X      $ RowDefinition1��67    � ��$ 0 �6S   �� 5Y      $ headerPanel��67    � ��$ 2,0,0,2q�6P   5Z      5\      5^      5_      5`      