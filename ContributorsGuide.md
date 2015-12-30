# Guideline for codebase contributors #

([Ardupilot Mega's source](http://code.google.com/p/ardupilot-mega/wiki/Guide))

## How to become a contributor: ##
Please contact _Kinderkram_, owner of this group. Short version: we are always looking for new team members, but to avoid disrupting existing work or destabilizing code, we have processes that bring people in gradually. For starters, you need to be given commit access to the SVN repository, something that can be granted by one of the existing team members. In general, it is best to propose (or better yet, prototype) a specific feature addition or bug fix, and demonstrate that you are comfortable enough with code to be able to do it. If this fits in well with the project's direction, you will be added to the developers maillist and given SVN access.

## Once you've been given commit access to the SVN: ##
When you have SVN commit access, you can check code into the repository (anyone can check code out, but only approved developers can commit changed code back in). Here are the tools to use:

  * A SVN client: On Windows, we recommend Tortoise SVN, which integrates into the Windows desktop. All command are available by right-clicking on any desktop folder. For starters, you can go to the Repo Browser and explore the repository folders. When you find the one you want to work on, Check Out that folder to your local hard drive. When you have finished editing the code, Commit it back in, with an explantion of the changes you've made.

  * A text editor: Although the Arduino IDE has a built-in text editor, it does bad things to line endings. Our SVN settings require that contributors use a better text editor, which handles line endings correctly. On Windows, we recommend Notepad++

## Rules for Committing code: ##
Unless you are very sure of what you are doing, and your changes are very minor, you should probably create a branch within SVN for your work. That way, if you mess things up, you won't take down the main code that everyone else is working on. If you're unsure, Branch. You can always Merge your changes back in once they're tested.

All changes should be made in your own branch first, and then integrated in the Dev\_Trunk, for everyone to test, then if at least thee Pirate testers give he thumbs up, the modif can then be made in the Trunk.

Never make changes directly in the Trunk unless agreed by developers group.


---

_Before you commit any code, double check that it compiles in Arduino ! If it does not compile, don't commit !_

---


Finally, if the changes made imply setup or other modification in operations, such as setup, behaviour, etc. you MUST document them in the WIKI, otherwise, the wiki gets outdated and obsolete, and chenges can be lost.


## Languages: ##
We only support Arduino as our coding environment. As a rule of thumb, no code should be committed that the Arduino compiler can't understand.