   M S B A M L   `   `   `  ����D  @ClientWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientWpf	ClientWpf  5   	       ClientWpf.ChatWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientWpf     5   	   -   5          � Title$  
ChatWindow��5   	   $��362.741��6   $��287.905��6-    � 
ResizeMode$ NoResize=�6=   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��34* �6   �� 5      $
��301* �6    � ColumnDefinitions 5   
   �� 5      $	��79* �6   �� 5      $
��215* �6   .+�5   
   �� -   5         ��Name$ 
historyBox��5      $
��Left=�6&   $	��256��6A    �� TextWrapping$
 Wrap=�6N   $	��Top=�6b   $	��282��6z   $��0,0.402,-0.047,0q�6�   $��2��6�   $��1��6�   �� 5   
   -   5        $ 
MessageBox��5      $
��Left=�6&   $��40��6A   $��0,261.402,0,-0.134q�6M   $
 Wrap=�6i   $��'your message here'��6}   $	��Top=�6�   $	��201��6�   $��2��6�   $��1��6�   �� 5   
   -   5         ��Name$ 
SendButton��5      $
��Send��6%   $
��Left=�64   $��40��6O   $��130.409,256.402,0,0q�6[   $	��Top=�6x   $��66��6�   $��1��6�   $��1��6�   �� 5   
   -   5        $ UserNameBox��5      $
��Left=�6'   $��34��6B   $
 Wrap=�6N   $��UserName��6b   $	��Top=�6r   $	��102��6�   $��36.409,0,0,0q�6�    �� 
IsReadOnly . $��1��6�   �� 5   
   -   5        $ UnLoggin��5      $��UnLoggin��6#   $
��Left=�66   $��34��6Q   $��138.409,0,-0.047,0q�6]   $	��Top=�6y   $��68��6�   $��1��6�   �� 5   
   -   5        $ getHelpButton��5      $��Get Help��6(   $
��Left=�6;   $��34��6V   $	��Top=�6b   $��66��6z   6�   5      5      