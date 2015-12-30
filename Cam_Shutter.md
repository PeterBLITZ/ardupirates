# Camera Shutter #

Small manual for UseCamShutter in firmware.

We actually have 11 channel on the APM. I also only found out recently.

So we use channel 9 (PL3) on APM. Just look on APM (underneath) you will see its written there (PL3) somewhere. Solder this wire to your signal wire on Servo (white or orange). Then just solder your servo (red) to +5V and (black) to gnd. You can now use a servo to trigger (shutter) your camera. To take pictures.

All you need now is to connect that servo somehow to your camera shutter button.....

Finaly you will need to fine tune focus, trigger (shutter) and release position of servo. When you switch to Altitude hold or GPS hold then the camera shutter automatically start taking pictures. First it moves to focus position....wait 2 seconds then move to shutter (trigger) position...(take picture)....wait 2 seconds and finally moves to release position....(take servo arm off trigger button)...wait 3 seconds and then repeat......