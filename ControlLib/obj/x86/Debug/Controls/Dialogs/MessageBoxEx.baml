   M S B A M L   `   `   `  ����E  AControlLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ControlLib.Controls.DialogsControlLib.Controls.Dialogs  5      .    (ControlLib.Controls.Dialogs.MessageBoxEx   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:ControlLib.Controls.Dialogs     5        � WindowStartupLocation$  CenterScreen=�5      $	��350��6D   $	��200��6P    � AllowsTransparency .  � WindowStyle$
 None=�5   (   .��5
      �� $��1,0,1,1q�6   ���	#FF45B9F1._�5   
   � 
������� � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� -   5           HighlightBrush#���   5   F   ._�5      � .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5      ��  �� BitmapScalingMode$
 Fant=�6    $��Uniform=�6�   $��0,8,0,8q�6�   ��6G   �� *Icon+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ WindowL�6�   �� 5        ContentFontSize#���  6�   
�������$��10,3,0,3q�6�   ��6$   �� *Title+ �� $ FindAncestor=�$ WindowL�6�   5      �� 5      -   5          CloseButtonStyle#���  5   2   5      5      �� 5      -   5        $��
tbkContent��5      $��1��6*   $��Center=�67   $��Center=�6T   #���  6o   $
��Wrap=�6�   �� 5      -   5        $��btnOk��5      $��2��6"   $��Ok��6/     MetroFlatButton#���  6P   $��40��6z   � 5      -   5        $��
gdOkCancel��5      $��3��6%   $��40��62   	 � ColumnDefinitions	 5      �� 5      �� 5       $��2 �6'   �� 5!      .+�5#      �� -   5        $��Ok��5#      #���  6:   �� 5$      -   5        $��2��5$      $��Cancel��6)   #���  6O   5%      5&      5'      5(      