# Kinderkram's Installer and Updater #

Always in mind to make the whole software stuff easier and faster, here comes the latest release of Kinderkram's ArduCopter Installer & Updater Software.

He decided to split the installation and the update process.

You'll need to download these 2 files:

  * [The Kinderkram Quad Installer](http://www.board-portal.de/ArduCopter/KinderKram-ArduCopter-Installer.exe)
![http://www.board-portal.de/ArduCopter/Kinderkram-Arducopter-installer.jpg](http://www.board-portal.de/ArduCopter/Kinderkram-Arducopter-installer.jpg)
  * [The Arducopter Quad Updater](http://www.board-portal.de/ArduCopter/Arducopter-Updater.exe)
![http://www.board-portal.de/ArduCopter/arducopter%20updater.jpg](http://www.board-portal.de/ArduCopter/arducopter%20updater.jpg)

The recent version 1.0 contains
  * Arducopter sketches & libraries from the public alpha 2.0
  * Arduino 0021
  * Configurator 1.1 (Important: Install it to C:\Arducopter\ArducopterConfigurator)
![http://www.board-portal.de/ArduCopter/media/images/configurator-directory.jpg](http://www.board-portal.de/ArduCopter/media/images/configurator-directory.jpg)

After the installation in C:\Arducopter you can easily upgrade the Configurator to version 1.22 manually. Just press the "Update" button.
If you bump into any problems, have a look into C:\Arducopter\sketches\Arducopter. If there's a Arducopter\_alpha\_RC1.pde - delete it!
And please check if all settings apply to your setup, i.e. COM-Ports , Baudrates etc.

Always download new updates from server to C:\Arducopter\Updates

You can download installation instructions as PDF [here](http://www.board-portal.de/ArduCopter/installation-instructions.pdf) (65KB):



**IMPORTANT** This will install the ArduCopter RC2 code, which is different of the ArduPiratesNG. ArduCopter RC2 is the latest ArduCopter Team code. ArduPiratesNG is the lastest code of both ArduPirates Team and ArduCopter Team. See [here](http://diydrones.ning.com/profiles/blogs/arducopter-ng-taken-over-by) for more informations. To use ArduPiratesNG code and Libraries, follow the next step "[Turtoise SVN](Tortoise.md)".