# FAQ #





---

### What means ArduPirates _NG_ ? ###

NG means New Generation. A lot of changes have been done in ArduPiratesNG code, and it's really a new step.

### What are the differences between ArduPirates' SuperStable Code v1.X and ArduPiratesNG Code? ###

SuperStable code is an exit of the ArduCopter's first release, also known as ArduCopter RC1. All developpments, as GPS hold, Altitude hold, SuperStable mode, Camera stabilisation etc. were started from there.

ArduPiratesNG is the latest version and will be the Official Release for both ArduPirates Team and ArduCopter Team.

### I can't compile the code, I get errors, what I can do? ###

here's a little "how to..." to compile without errors:

First clean up everything (make copies in some other directories if you wish)

  1. copy "libraries" directory from "ardupirates\trunk\ArduPiratesNG" to "arduino" directory
  1. copy libraries directory from "ardupirates\trunk\QUAD\ArduCopter\_SuperStable " to arduino directory DO NOT overwrite when asked
  1. create "ArduPiratesNG" directory in the "sketches" diretory
  1. copy the files from "ardupirates\trunk\ArduPiratesNG" (not the libraries dir) to the "ArduPiratesNG" directory in "sketches" diretory
  1. in arduino open sketch "ArduPiratesNG"
  1. compile (should do without errors)
  1. go to http://ardupirates.net/config/ select NG Platform, select your options and generate new config.h
  1. it's quicker if you copy&paste it into the opened config.h
  1. save an recompile

### I can't save my Tx calibration, I get errors ###

To be able to save the tx calibration, your pc must have English-US regional settings.

### Are all Xbee's Series working with APM? ###

Yes, all Xbee's are working with APM, but Pro Serie 2 does not work properly on telemetry. It is not because of APM due APM does not know anything about radio modems. It's the design on Pro 2 that is against us. Their firmware does not work properly with constant data stream.

### One (or more) motors stop spinning suddenly during flight, what's wrong? ###

This can come from several sources, but you should check your bullet connectors between motors and ESC's, soldering the cables together is a good way.