# Introduction #

If you use the DIYDrones MediaTek GPS, you're probably aware of the fact that, on each activation, it make take a very long time to get proper satellite lock.  The reason is that it always starts from scratch; it is not aware of it approximate geographic location, not of the time.

The reason for this is lack of backup battery.  Data is completely erased once you disconnect the main battery.

The GPS module itself has a backup battery input.  Here we show how to connect an external battery to that input.

# What You Need #

You will need a a battery holder and a small, 3V, coin-shaped lithium battery.  These can be purchased at any DIY electronics store.

I also used a piece of perforated prototyping board, but this is for convenience and not a must.

# How It's Done #

Below, you can see the GPS module board layout.  Cut one trace as shown, and connect the two leads from the battery holder to the marked places.

![http://ardupirates.googlecode.com/svn/Images/2011_0415%2005%20GPS%20backup%20battery.png](http://ardupirates.googlecode.com/svn/Images/2011_0415%2005%20GPS%20backup%20battery.png)

Here is how the setup looks:

http://ardupirates.googlecode.com/svn/Images/2011_0415%2002%20%20GPS%20backup%20battery.JPG

http://ardupirates.googlecode.com/svn/Images/2011_0415%2003%20%20GPS%20backup%20battery.JPG

This is how it looks from below. Note the place where the trace is cut:

http://ardupirates.googlecode.com/svn/Images/2011_0415%2004%20%20GPS%20backup%20battery.JPG