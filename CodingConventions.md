# The ArduPirates Coder's Bible #

## Coding Conventions ##

([Derived from Ardupilot Mega](http://code.google.com/p/ardupilot-mega/wiki/Naming))

Since multiple individuals are simultaneously contributing to the ArduPirates codebase, we need to observe a few coding conventions. These coding conventions should be considered the ArduPirates Coder's Bible from this point onward.

These conventions help us all maintain readable and understandable code, in spite of our different native languages and cultures. These conventions set rules for formatting and content, and define how we approach the whole coding concept.

Feedback and suggestions to these conventions are always welcome.


### General Principles ###
Where possible, **keep the code simple and straightforward**.  As this is an Arduino-based project, look to the level and style of code that the Arduino community generally uses and understands to establish a baseline.

When considering the use of a more sophisticated technique or language feature, consider whether it will improve the overall comprehensibility of the code, or whether a slightly more verbose but simpler implementation will achieve the same end with reduced confusion.

Remember, doing the same operation in **two easy steps is** generally **better than** doing it in **one complicated step**. It will probably not save you bytes in memory and it will not be processed any quicker, but complex code will be harder to read and certainly harder to debug.

**Use libraries and classes** as ways to hide complexity; it is generally more acceptable to provide a class with a simple and easy to understand interface that internally uses more complex techniques than it is to implement those techniques directly.

**Document your code !**  Everything that the code does should be explained in a comment; for any operation the code performs, a reader should be able to answer "what is this doing?" and "why is it doing it?" by reading the comments. We should prefer to have one line of code for two lines of comments, making sure _everyone_ understands what the code does, over having too little comments and others not understanding what we are doing.

Take advantage of the naming rules detailed below to help explain what the code does.

### Names ###
In general, we prefer expressive identifier names using whole words separated by underscores.  Try to keep names to a reasonable length (less than 30 characters or so).  When possible, use standard terms and acronyms.  Acronyms should be capitalised correctly, even in identifiers that are described as being lowercase.

When constructing an identifier name, order words starting with the more general and progressing to the more specific.

```
roll_command
GPS_update_rate
```

<dl>
<dt><b>File names</b></dt>
<dd>
<p>Source code filenames should begin with a capital letter, other filenames should not unless required by another convention.  The main file in a sketch should be named using CamelCase in order to distinguish it (and in accordance with convention).</p>
<pre><code>ArduPirates.pde<br>
Math.cpp<br>
eeprom.txt<br>
</code></pre>
</dd>
<dt><b>Type names</b></dt>
<dd>
<p>Structure, class, enumeration and typedef'ed names should begin with a capital letter.  When these types are provided by a library, they may be prefixed by the library name.</p>
<p>Library class names should begin with <code>AP_</code> if they are specific to the ArduPilot project, otherwise they should try to conform to Arduino norms.</p>
<pre><code>class Vector;<br>
class AP_GPS_Auto;<br>
typedef unsigned long Latitude;<br>
</code></pre>
</dd>
<dt><b>Function names</b></dt>
<dd>
<p>Function names should be all lowercase.  Class member function names do not need to begin with or include the class name, as it is implied.  Functions marked static in a file may omit mention of the file's purpose if their use is otherwise obvious.  Private member function names should begin with an undercore, protected member function names should not.</p>
<pre><code>int radio_get_switch_setting(void);<br>
static bool is_complete(void);<br>
class GPS {<br>
public:<br>
    void init(void);<br>
protected:<br>
    int status(void);<br>
private:<br>
    bool _read(void);<br>
};<br>
</code></pre>
</dd>
<dt><b>Global variable names</b></dt>
<dd>
<p>Global variable names should be all lowercase, and prefixed with <code>g_</code>.</p>
<pre><code>unsigned long g_startup_time;<br>
</code></pre>
</dd>
<dt><b>Global constant names</b></dt>
<dd>
<p>Global constant names should be all lowercase, and prefixed with <code>k_</code>.  All constants should be marked with the <code>const</code> qualifier.</p>
<p>Enumeration members should be named like constants.</p>
<pre><code>const float k_pi = 3.1415;<br>
enum Status {<br>
        k_operational,<br>
        k_defunct<br>
};<br>
</code></pre>
</dd>
<dt><b>Local variable names</b></dt>
<dd>
<p>Local variable names should be all lowercase.  Single-letter identifier names are acceptable for common uses; e.g. <code>i, j, k</code> for nested loop counters, <code>c</code> for a character, <code>s, p</code> for pointers to character strings.</p>
</dd>
<dt><b>Member variable names</b></dt>
<dd>
<p>Private member variable names should begin with an underscore.  Protected and public member variable names should not.</p>
<pre><code>class Foo {<br>
public:<br>
    int x;<br>
protected:<br>
    int y;<br>
private:<br>
    int _z;<br>
};<br>
</code></pre>
</dd>
</dl>

### Functions ###
Where possible, group functions into classes.  When functions are exported by a library, if the library does not implement a class its functions should be grouped into a namespace.

In general, avoid global functions when a better alternative is available.  If a global function is only used and appropriate in a single file, it should be marked static.  Class member functions should be private or protected unless they form a part of the class' contract with its clients.

### Classes ###
Avoid the use of public member variables for class properties; implement get/set methods instead.  Where a class has a property `int some_property`, implement

```
int     get_some_property(void);
void    set_some_property(int new_value);
```

If a class has only one property and exists primarily for the purpose of that property, it is acceptable to have methods `get` and `set` omitting the property name.  Consider overloading `operator <type> &` in the case where the class masquerades as a variable.

### Constants ###
Where possible use global constants rather than preprocessor definitions.  i.e. instead of

```
#define FROG_SCALING_FACTOR     4.3
```

use

```
const float k_frog_scaling_factor = 4.3
```

For class-specific constants, use `static const` members or enumerations as appropriate.

### Formatting ###
  * Code should be formatted according to the style known as [1TBS](http://en.wikipedia.org/wiki/Indent_style#Variant:_1TBS).  Experience has shown that it produces code that is easy to read and harder to get "wrong".  It's also easy to configure most text editors to help you keep code in this style.
  * Source line lengths should be no more than 120 characters (barring explicit technical constraints).
  * Line endings should be native to the platform on which the file was checked out, and all files containing text should have the appropriate SVN attributes to ensure that line endings are translated correctly.
  * The default indentation will be four spaces.
  * No tab characters in our source files.  This means the code will always look 'right' in any editor.
  * Dates should use the international format of YYYY-MM-DD. For example, "3rd of April 2002", in international format is written as 2002-04-03.

_editor configuration examples TBD_

See the Tools section below for recommended tools to aid in formatting your code.

### Other Code Conventions ###

**_We should prefer the use of a .h/.cpp file pair_** when adding new functionality to a sketch, rather than adding another .pde file.  The compiler considers each .cpp file separately, but the .pde files are smushed together before the compiler gets to see them.  Thus, things that the programmer believes are separate because they're in separate files are in fact not separate.

This also requires the programmer to create a corresponding .h file which contains explicit declarations of everything inside the file that is intended for use by other files.  This provides a place for documentation for those interfaces, as well as making it explicitly clear to anyone else coming along later what the expected interface to the contents of the file are.

Avoid the use of C source files, as the compiler treats them in subtly different ways that will not be obvious to many developers.

### Useful Tools ###
The [Artistic Style](http://astyle.sourceforge.net/) source code formatter can be used to help keep code in a consistent style.  The following configuration file can be used with the command-line version of Artistic Style.

There is a [Windows GUI](http://jimp03.zxq.net/) available as well.  The [Universal Indent GUI](http://universalindent.sourceforge.net/) also supports Artistic Style and can read the configuration file, but on at least some platforms Artistic Style support is broken in the most recent version.

```

#
# Artistic Style configuration file for the ArduPirates Coding Conventions
#       http://code.google.com/p/ardupirates/wiki/CodingConventions
#
style=1tbs                 # http://en.wikipedia.org/wiki/Indent_style#Variant:_1TBS
indent-preprocessor        # multi-line preprocessor macros will be indented
min-conditional-indent=0   # multi-line conditional expressions are indented to follow their parentheses.
pad-oper                   # spaces are added around arithmetic operators
pad-header                 # spaces are added between if/while/for/switch and their arguments
unpad-paren                # spaces are removed from around parentheses in expressions
keep-one-line-statements   # don't wrap complex or multi-line statements
align-pointer=name         # keep the * in pointer declarations with the pointer name
```