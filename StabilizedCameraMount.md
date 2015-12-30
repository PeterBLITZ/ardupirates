# Ardu Pirate's Gyro Stabilyzed Camera Mount #

Welcome to the "**How To Build Your Own Gyro Stabilyzed Camera Mount**" (for GoPro HD Hero with housing)

Ok, here it is, a camera mount for the Ardu Pirate's Quad, I'll be using a GoPro HD Hero camera, for videos and pictures.

I could have designed a smaller mount if using the camera naked, but for safety reasons, I'll use the camera with the protective housing.

![http://www.danisaez.com/ardupirates/cameramount/gopro.jpg](http://www.danisaez.com/ardupirates/cameramount/gopro.jpg)

Here's the idea, the camera will be positioned between the two front arms

![http://www.danisaez.com/ardupirates/cameramount/cm00.jpg](http://www.danisaez.com/ardupirates/cameramount/cm00.jpg)

The GoPro has two different shooting angles, depending on the recording mode selected, 170º or 127º(in 1080). Here you can see them.

![http://www.danisaez.com/ardupirates/cameramount/cm000.jpg](http://www.danisaez.com/ardupirates/cameramount/cm000.jpg)

Here you have the pdf with the design [Camera Mount](http://www.danisaez.com/ardupirates/cameramount/camerasupport.pdf).

![http://www.danisaez.com/ardupirates/cameramount/cm01.jpg](http://www.danisaez.com/ardupirates/cameramount/cm01.jpg)

## List of Materials ##

  * 1 2mm thick 500x20 aluminium strip
  * 1 3mm thick 400x20 aluminium strip
  * 2 12gr. servos
  * 10 2mm screws
  * 1 4mm screw 40mm long
  * 1 3mm screw 10mm long

## Building ##
Start cutting and bending the supporting bar, the camera will hang from it:

![http://www.danisaez.com/ardupirates/cameramount/cm02.jpg](http://www.danisaez.com/ardupirates/cameramount/cm02.jpg)

Next cut and bend the upper part:

![http://www.danisaez.com/ardupirates/cameramount/cm03.jpg](http://www.danisaez.com/ardupirates/cameramount/cm03.jpg)

Next make the two U angles, bear in mind that one has to fit inside the other, so the smaller is 20mm width, and the other is 24mm.

![http://www.danisaez.com/ardupirates/cameramount/cm04.jpg](http://www.danisaez.com/ardupirates/cameramount/cm04.jpg)

Next cut and bend the lower part:

![http://www.danisaez.com/ardupirates/cameramount/cm05.jpg](http://www.danisaez.com/ardupirates/cameramount/cm05.jpg)

On the upper part, we have to make a hole to fit the tilt servo, and the holding screw on the other side:

![http://www.danisaez.com/ardupirates/cameramount/cm06.jpg](http://www.danisaez.com/ardupirates/cameramount/cm06.jpg)

On the lower part, we have to make a hole to fit the servo arm, and the holding screw on the other side:

![http://www.danisaez.com/ardupirates/cameramount/cm07.jpg](http://www.danisaez.com/ardupirates/cameramount/cm07.jpg)

Servo fitted with two 2mm screws:

![http://www.danisaez.com/ardupirates/cameramount/cm08.jpg](http://www.danisaez.com/ardupirates/cameramount/cm08.jpg)

Holding 3mm screw is fixed on the other side:

![http://www.danisaez.com/ardupirates/cameramount/cm09.jpg](http://www.danisaez.com/ardupirates/cameramount/cm09.jpg)

Pre-Fit of upper and lower parts:

![http://www.danisaez.com/ardupirates/cameramount/cm10.jpg](http://www.danisaez.com/ardupirates/cameramount/cm10.jpg)

Now we fix the upper holding angle to the supporting bar:

![http://www.danisaez.com/ardupirates/cameramount/cm11.jpg](http://www.danisaez.com/ardupirates/cameramount/cm11.jpg)

Then the lower holding angle to the lower frame:

![http://www.danisaez.com/ardupirates/cameramount/cm12.jpg](http://www.danisaez.com/ardupirates/cameramount/cm12.jpg)

I'll be using a heli ball link for the Roll servo:

![http://www.danisaez.com/ardupirates/cameramount/cm13.jpg](http://www.danisaez.com/ardupirates/cameramount/cm13.jpg)

Also reinforced the servo arm with 1mm aluminium

![http://www.danisaez.com/ardupirates/cameramount/cm13.jpg](http://www.danisaez.com/ardupirates/cameramount/cm13.jpg)

Fit the 4mm screw to hold the two U angles together:

![http://www.danisaez.com/ardupirates/cameramount/cm14.jpg](http://www.danisaez.com/ardupirates/cameramount/cm14.jpg)

Almost there:

![http://www.danisaez.com/ardupirates/cameramount/cm15.jpg](http://www.danisaez.com/ardupirates/cameramount/cm15.jpg)

Camera Pre-Fit:

![http://www.danisaez.com/ardupirates/cameramount/cm16.jpg](http://www.danisaez.com/ardupirates/cameramount/cm16.jpg)

Extended the servo wires, and pre-set the camera mount in place, just wo bolts will be needed to fix it:

![http://www.danisaez.com/ardupirates/cameramount/cm17.jpg](http://www.danisaez.com/ardupirates/cameramount/cm17.jpg)

![http://www.danisaez.com/ardupirates/cameramount/cm18.jpg](http://www.danisaez.com/ardupirates/cameramount/cm18.jpg)

The camera mount will move the CG forward, so to compensate this a bit, when using the camera mount I'll move the battery back, in a new horizontal support between the rear arms:

![http://www.danisaez.com/ardupirates/cameramount/cm19.jpg](http://www.danisaez.com/ardupirates/cameramount/cm19.jpg)

![http://www.danisaez.com/ardupirates/cameramount/cm20.jpg](http://www.danisaez.com/ardupirates/cameramount/cm20.jpg)

![http://www.danisaez.com/ardupirates/cameramount/cm21.jpg](http://www.danisaez.com/ardupirates/cameramount/cm21.jpg)

Top view of the whole system:

![http://www.danisaez.com/ardupirates/cameramount/cm22.jpg](http://www.danisaez.com/ardupirates/cameramount/cm22.jpg)


## Setup ##

I'll be using ch6 for tilt control.

Setup taken from Post #1:

> Place the following lines into Arducopter.pde ~Line 778 after this line:
> Code:

> |APM\_RC.OutputCh(3, backMotor); // Back motor |
|:---------------------------------------------|

> For the CURRENT code from the trunk (release #733 SVN)

> http://static.rcgroups.net/forums/attachments/3/2/4/1/6/5/a3556748-28-SVN%20code%20frame%20setup.jpg?d=1288025577

> Code:
> ||// Camera Stabilisation with transmitter controlled tilt.
> APM\_RC.OutputCh(4, APM\_RC.InputCh(6)+(pitch)`*`1000); // Tilt correction
> APM\_RC.OutputCh(5, 1510+(roll)`*`-400);               // Roll correction

> Code:
> ||// Camera Stabilisation NO transmitter controlled tilt.
> APM\_RC.OutputCh(4, 1500+(pitch)`*`1000); // Tilt correction
> APM\_RC.OutputCh(5, 1500+(roll)`*`-400);   // Roll correction

> The ...`*`1000);... can be tuned for smoother moves of the camera.

> For the PUBLIC ALPHA v1.0

> http://static.rcgroups.net/forums/attachments/3/2/4/1/6/5/a3556749-122-alpha%20code%20frame%20setup.jpg?d=1288025577

> Code alternative 1:
> Code:
> ||//Camera Stabilisation with transmitter controlled tilt.
> APM\_RC.OutputCh(4, APM\_RC.InputCh(6)+(roll-pitch)`*`1000); //Tilt correction
> APM\_RC.OutputCh(5, 1500+(roll+pitch)`*`1000); //Roll correction

> Code alternative 2:
> Code:
> ||// Camera Stabilisation NO transmitter controlled tilt.
> APM\_RC.OutputCh(4, 1500+(roll-pitch)`*`1000); //Tilt correction
> APM\_RC.OutputCh(5, 1500+(roll+pitch)`*`1000); //Roll correction

> If you don't have an extra channel:
> "1500" is the center position of servos, it go from 1000 to 2000.
> You can set desired Tilt angle: .... (4, 1800+(roll-pitch)`*`1000); ....


**Let's try to explain those values a bit more:**

In my case I had the roll inverted, and the tilt was overcorrecting, so here's what to do:

For the Roll:
> Original Line:
> > APM\_RC.OutputCh(5, 1500+(roll)`*`-400);   // Roll correction

  * The 1500, is the initial value for the roll servo, that is like the trim on the radio, so to get my camera horizontal, had to go for 1650, this is like setting starting position
  * The -400, is the multiplier factor for the roll servo, as I had mine inverted, I just removed the minus sign, then to adjust the movementto be correct, I ended up with 800


> New Line:
> > APM\_RC.OutputCh(5, 1650+(roll)`*`800);               // Roll correction

For the Tilt:

> Original Line:
> > APM\_RC.OutputCh(4, APM\_RC.InputCh(6)+(pitch)`*`1000); //Tilt correction

  * The only value we can change here, it's the 1000, this is the multiplier for the tilt servo, that is higher the value, the more it will correct the position, as mine was overcorrecting, I had to lower this value from 1000 to 600.


> New Line:
> > APM\_RC.OutputCh(4, APM\_RC.InputCh(6)+(pitch)`*`600); // Tilt correction

Right now, as those values are not yet in the configurator, the easiest way to tune them, is:
  1. open the arduino
  1. connect your quad in the USB port (no need for battery)
  1. change the values in the two lines of code
  1. upload the code
  1. repeat the two previous steps until the camera corrections are ok.

## GoPro Connection for video feedback ##

For the moment, until I get the FPV kit set, I'll be using the GoPro HD Hero to record video, the good thing is that the latest firmware of the camera enables Live Video Output, so why not see what you're recording?

Just connect the Live Video Output of the GoPro to the 1.3Ghz video transmitter, that is Video, Audio and negative. then connect the 11.1v to the transmitter

That's it, you got yhe video feedback.

## Video Proof ##

<a href='http://www.youtube.com/watch?feature=player_embedded&v=JTc8ngXgVBk' target='_blank'><img src='http://img.youtube.com/vi/JTc8ngXgVBk/0.jpg' width='425' height=344 /></a>

Did a couple of videos, but night came in and it was too dark to see a thing, tomorow I'll take some videos.

That's it!!

Enjoy!!

Happy Landings,

Dani