   M S B A M L   `   `   `  ����E  AControlLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ControlLib.ControlsControlLib.Controls  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089+    %ControlLib.Controls.ImageToggleButton  �� 
TargetType �� Property �� Value �� 
TargetType'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� TargetProperty �� 
TargetName	 R� KeyTime
 R� Value  System.Windows.Visibility ��Name �� RelativeSource�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      +local!clr-namespace:ControlLib.Controls  5      %p  '         �� 5      "    6   .�5   
   ��  �@�Cursor$
 Hand��6#   �� 5   
    � �� 5      �� 5      "   6"   .R�5	      � 
������  5
        5      CommonStates��. 5   "     
Normal��. 5   &   �� .��5   *   T� $ (UIElement.Visibility)�6H   $ StartButtonHover��6{   ..�5   .   i� $	 0��6E   
 5   2    5   6   	Collapsed=�6L   5   /   5   +   T� 5   *   $ (UIElement.Visibility)�6H   $ PressedImage��6{   ..�5   .   i� $	 0��6E   
 5   2    5   6   	Collapsed=�6L   5   /   5   +   5   '   5   #     5   "   Disabled��6.     5   "   	MouseOver��. 5    &   �� .��5!   *   T� $ (UIElement.Visibility)�6H   $ StartButtonHover��6{   ..�5"   .   i� $	 0��6E   
 5#   2    5$   6   Visible=�6J   5&   /   5'   +   T� 5(   *   $ (UIElement.Visibility)�6H   $ StartButtonNormal��6{   ..�5)   .   i� $	 0��6E   
 5*   2    5+   6   	Collapsed=�6L   5-   /   5.   +   T� 5/   *   $ (UIElement.Visibility)�6H   $ PressedImage��6{   ..�50   .   i� $	 0��6E   
 51   2    52   6   	Collapsed=�6L   54   /   55   +   56   '   57   #     58   "   Checked��. 59   &   �� .��5:   *   T� $ (UIElement.Visibility)�6H   $ StartButtonNormal��6{   ..�5;   .   i� $	 0��6E   
 5<   2    5=   6   	Collapsed=�6L   5?   /   5@   +   T� 5A   *   $ (UIElement.Visibility)�6H   $ StartButtonHover��6{   ..�5B   .   i� $	 0��6E   
 5C   2    5D   6   	Collapsed=�6L   5F   /   5G   +   T� 5H   *   $ (UIElement.Visibility)�6H   $ PressedImage��6{   ..�5I   .   i� $	 0��6E   
 5J   2    5K   6   Visible=�6J   5M   /   5N   +   5O   '   5P   #     5Q   "   	UnChecked��. 5R   &   �� .��5S   *   T� $ (UIElement.Visibility)�6H   $ StartButtonNormal��6{   ..�5T   .   i� $	 0��6E   
 5U   2    5V   6   Visible=�6J   5X   /   5Y   +   T� 5Z   *   $ (UIElement.Visibility)�6H   $ StartButtonHover��6{   ..�5[   .   i� $	 0��6E   
 5\   2    5]   6   	Collapsed=�6L   5_   /   5`   +   T� 5a   *   $ (UIElement.Visibility)�6H   $ PressedImage��6{   ..�5b   .   i� $	 0��6E   
 5c   2    5d   6   	Collapsed=�6L   5f   /   5g   +   5h   '   5i   #   5j      .+�5l      �� $ StartButtonNormal��6    $
��Fill=�5n      ��5m      �� *NormalImage+ �� *TemplatedParent+5n      �� 5o      $ StartButtonHover��6    $
��Fill=�5q      $y�	Collapsed=�6'   ��5p      �� *MouseOverImage+ �� *TemplatedParent+5q   '   �� 5r      $ PressedImage��6    $
��Fill=�5t      $y�	Collapsed=�6'   ��5s      �� *PressedImage+ �� *TemplatedParent+5t   '   �� 5u      #��z��6+   $��0,5,0,0q�6O   #��z��5v   +   #��z��5w   +   5x      5y      5{      5|      5}      