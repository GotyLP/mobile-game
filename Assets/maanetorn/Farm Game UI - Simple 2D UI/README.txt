CUSTOMBUTTON SCRIPT
Buttons have the same override Button script named "CustomButton.cs" This code only changes the position of button's text depending on the "thickness" of the button sprite. For "thickness" please check Image A below.
And please note that the thickness is different for 128x128 and 256x256 button prefabs.

Image A
+--------+
|        |
|        |
|        |
+--------+
|        |  <---- this is the thickness
+--------+
default button sprite

Image B
+--------+
|        | 
|        | 
|        | 
+--------+ 
pressed button sprite

When you click on the button, the default button sprite is swapped with the pressed button sprite and the text changes position accordingly thanks to the "CustomButton.cs"
In the code, the text position is changed depending on the "offset" variable which is calculated through the "height". I did this way because I have 128x128 and 256x256 button prefabs. But if you change the button size manually: the offset of the text might not make sense with respect to the thickness of the button or game view dimension----> use the code on line 42 instead of 40.
Code on line 42: float offset = height - (height * 0.86718f) - insertYourCustomOffset;
*Some examples to adjust the offset:
				float offset = height - (height * 0.86718f) - 10f;
				float offset = height - (height * 0.86718f) +5f;
*I included the explanation and an example in the code as well. Just erase the "//" in the beginning of the required line and add "//" in the beginning of the unwanted line.
*If you want, of course, don't calculate the offset and just put your custom offset directly.
			
FONT
This asset pack uses the Lilita One font by Juan Montoreano
📄Copyright (c) 2011 Juan Montoreano (juan@remolacha.biz), with Reserved Font Name Lilita One. This Font Software is licensed under the SIL Open Font License, Version 1.1 .
Font source: https://fonts.google.com/specimen/Lilita+One.

This package uses TextMeshPro ---> Be sure to import TMP Essentials in order for the Demo Scene to be uploaded correctly in the game view.

CONTACT INFO
If you have any questions or need help please contact me here:

*Email: contact-maanetorn.cultivate496@slmails.com

*Website: https://maanetorn.wixsite.com/contact