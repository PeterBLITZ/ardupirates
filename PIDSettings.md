## Flight Tuning tips ##

([ArduCopter's source](http://code.google.com/p/arducopter/wiki/Quad_FlightTips))


### Acrobatic Mode ###
Acrobatic mode is a `PID` rate control.  The `I` term is not needed (usually 0).  `D` should be a low value (start with `D=0`).  The `P` term is the main parameter.  You should start by increasing `P` until the ArduCopter starts to oscillate and then reduce the `P` parameter a little bit.
<br>
<br>
<h3>Stable Mode</h3>
On the current version of stable mode, there are two controls.<br>
The first one is an outer control that is a PI absolute control which drives an inner control that is a P_rate control (<i>In the configurator, this is labeled as <code>D</code>. It will be changed to <code>P_rate</code></i>).<br>
<br>
<img src='http://arducopter.googlecode.com/svn/images/instructional/AC_Stable_control.jpg' />

This approach utilizing two controls instead of the normal <code>PID</code> has proved to be much better.  Because of this, the <code>D</code>, or <code>P_rate</code>, parameter is important and shouldn't be zero.<br>
<br>
<br>
<h3>Control Adjustment Guidelines</h3>
If you haven't adjusted your Acrobatic mode parameters, just use the default.  The <code>P_rate</code> value in Stable mode should be a little lower than the <code>P</code> value in Acrobatic mode.  <i>For example, a <code>P</code> rate in Acrobatic mode could be 1.9 and the <code>P_rate</code> in Stable mode could be 1.2.</i>  If the <code>P_rate</code> is too high, you will see quick oscillations on the ArduCopter. If the values are too low, it will be unstable.  The <code>I</code> term can be thought of like the trim on your radio; it is needed to account for differences in motors, ESCs, etc.  Its value will be low, likely between 0.1 and 0.4.  The <code>P</code> parameter is the main authority of the outer control.  If its value is too low, the quad will be lazy and slow to respond to stick movements.  If it is too high, instability will develop and the ArduCopter will oscillate.<br>
<br>
<br>
Here is an example video of oscillation:<br>
<br>
<a href='http://www.youtube.com/watch?feature=player_embedded&v=4bbpXBuPWSg' target='_blank'><img src='http://img.youtube.com/vi/4bbpXBuPWSg/0.jpg' width='425' height=344 /></a><br>
<br>
<br>

<b>Since control tuning is difficult, it is recommended that you start with the default values and only change one value at a time.</b>

Here are some other explanations made by Jose Julio, one of the DIYDrones core members:<br>
<br>
"I´d like to explain a bit more the actual Acrobatic and Stable modes of ArduCopter<br>
- Acrobatic mode, like you said is a PID rate control. I term is not needed (usually 0), D should be low (I personally prefer to flight with D=0) and P is the main param. As always you should start increasing P until the quad start to oscillate and then reduce a bit the P param.<br>
- Stable mode. Now the last version of stable mode are really two controls. One inner control that is a P rate control (that is the "D" label in the configurator, we need to change this label to "P_rate" ) and an outer control that is a PI absolute angle control.<br>
<br>
This new aproach with two controls instead of a simple PID control has demostrated to be much better.<br>
So, this is why the "D" param in the configurator (P_rate) is so important and could not be 0. And now some guidelines to adjust the control...<br>
First, if you have your Acrobatic mode adjusted, then the P_rate of stable mode is a bit lower than this value, for example I have P of acrobatic mode 1.9 and P_rate of Stable mode ("D" param in configurator) 1.2. If you don´t have your acrobatic mode adjusted I recommend to start with the default value. As in Acrobatic mode, if P_rate ("D" param in configurator) is too high you will see quick oscillations on your quad, too low and your quad will be unstable.<br>
Now the PI outer control params... I term is needed to "auto trim" for differences between motors, ESCs... (it´s easy to think in the I term as the trim of your radio) but we need only an small value (probably between 0.1 and 0.4, the default value is 0.15 and it´s a good start value). The P param is the main param of the outer control and defines the "authority" of the control, so if you have a low P value the quad will be "lazy" to the sticks. If you have a too high value you start again with oscillations and inestability.<br>
<br>
Control tunning is always a bit tricky so I recommend to start with the dafault values and only change one param on each test.<br>
<br>
Hope this helps,<br>
Jose."<br>
<br>
<img src='http://www.board-portal.de/ArduWiino/divider.gif' />

<b>Pirate Menno aka joebarteam offers a great tutorial on PID tuning</b>


Step-by-step instructions with videos!<br>
Not only applicable for MegaPirates but for every multirotor aircraft needing tuning of the PIDs.<br>
<br>
<a href='http://www.rcgroups.com/forums/showthread.php?t=1521520'>http://www.rcgroups.com/forums/showthread.php?t=1521520</a>

<img src='http://www.board-portal.de/ArduWiino/divider.gif' />