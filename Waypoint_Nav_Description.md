# Introduction #

First, the code I will be referring to is located in my (Knuckles904) branch of the svn. It was last updated a while back (end of Nov). I plan to work with Hein to integrate a better form into the more current ardupirate code.

But for now this page discusses how it is currently implemented for anyones reference.


# Details #

The waypoint feature is wrapped with a #define UseWaypoint for easy disabling of the feature. The meat of the code added was added into the loop of the main tab:

#ifdef UseWaypoint
> if(Waypoint\_counter>(200\*loiter\_time)&&((waypoint\_i+1)<(num\_of\_waypoints\*2))){      //12000=200hz\*60s

> target\_lattitude+=waypoint\_array[waypoint\_i](waypoint_i.md);

> target\_longitude+=waypoint\_array[waypoint\_i+1];

> waypoint\_i+=2;

> Waypoint\_counter=0;

> //reset variables

> gps\_roll\_I = 0;

> gps\_pitch\_I = 0;

> gps\_err\_roll = 0;

> gps\_err\_pitch = 0;

> gps\_roll\_D = 0;

> gps\_pitch\_D = 0;

> gps\_err\_roll\_old = 0;

> gps\_err\_pitch\_old = 0;

> command\_gps\_roll = 0;

> command\_gps\_pitch = 0;

> }
#endif

The other chunk of code is found in the UserConfig.h:

////
//Waypoint declarations
int waypoint\_array[8](8.md)={0,1000,1000,0,0,-1000,-1000,0}

//lat0,long0,lat1,long1,lat2,long2,lat3,long3 (+ is N or E, - is S or W)

int num\_of\_waypoints=4;

int loiter\_time=20;//loiter time per waypoint in seconds

Also, two counters were added into the arducopter.h

unsigned int Waypoint\_counter=0;

int waypoint\_i=0;

# **Explanation** #
In a nutshell, the code starts a counter once gps hold mode has been triggered. A constant time is allowed between waypoints. This is defined as loiter time, in my code set to 20 seconds. The code will wait for the waypoint counter to reach the equivalent of 20 seconds and then will shift the target lat and long by the number of "units" in the waypoint\_array. The "units" correspond to the raw gps output from the gps lib. In reality, 1000 units equals around 20ft or so. The pattern I have programmed in will do as so after the gps hold mode has been activated:
1. Wait 20 sec at start/home position
2. move the target longitude value 1000 units to the East, making the copter move towards that new waypoint and continue doing so for 20 sec
3. move the new target lattitude 1000 units north, making the copter move towards that new waypoint and continue to do so for 20 sec
4. same, but moves the new target location west 1000 units, making it directly north of the home position
5. move the new target location back to home.

Basically, the waypoint array is an array of vectors which are then added to the current target location to make the new one. The already working position hold algorithm handles the rudimentary navigation. The results can be seen below:

![http://i52.tinypic.com/63u62u.jpg](http://i52.tinypic.com/63u62u.jpg)

In addition, to keep the rest of the code running smoothly, upon each change of target location, the necessary gps nav variables were reset. To keep the counter from changing the current waypoint\_i (used to determine which waypoint we are on) while not in gps hold mode,
> Waypoint\_counter=0;

> waypoint\_i=0;

were inserted into each mode besides gps hold mode.

# **Discussion** #

The code I have currently uses relative waypoints. There is a reason for that. When I first started, I found that my real location(taken from google maps) did not correspond well with the location my mtek gps module gave me. See what I mean from this test, taken while driving my car along the road in a loop.

![http://i51.tinypic.com/7330j7.jpg](http://i51.tinypic.com/7330j7.jpg)

To null this out, waypoints were taken to be relative to starting position. To make absolute waypoints would be relatively easy, but the offset of the gps would have to be calculated and nulled out when programming the absolute waypoints.

Also, because the navigation is handled by a gps hold algorithm and not a navigation algorithm, the navigation is pretty sloppy. The copter just goes directly to the current waypoint in a straight line. For a real autopilot navigation, cross track error should be considered and the copter should stay on the line between waypoints as well as possible.

**In conclusion, this is only a start for navigation, but it proves that multicopter navigation is very possible**