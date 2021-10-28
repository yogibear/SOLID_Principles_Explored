# Open Closed Principle (OCP)
The OCP principle is one for SOLID principles.

The code in this project is there to demonstrate two ways the OCP principle could be applied.


## OCP

"In object-oriented programming, the open–closed principle states "software entities (classes, modules, functions, etc.) should be open for extension, but closed for modification";[1] that is, such an entity can allow its behaviour to be extended without modifying its source code." [en.wikipedia.org](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle)

These are examples and ideas for implementing OCP, they are not necessarily the best way to do it.


## OCP.CS

This source file shows how you can apply the OCP principle in relation to a GUI for example.
here we see a list of classes/objects can be placed in a list. Each class knows it's name and
how to do it's thing! A user in say WinForms for example when selecting the item, the event processing
this selection gets the object relating to the text described and naturally will do the right thing.
No need for if statements, its fairly cool.

## OCP Reflection
This uses the .net Activator class (so might not be reflection, need to check), to instantiate a class
from a string representation of the name. This could come from an environment variable, or argument passed in.

The beauty of this approach is that you can have one project, with all the logic in it. So for 
example say you were going to process 3 different kafka streams. One project with all the logic in it.

Then spin up that project 3 times, each time passing in the class name which deals with the topic your pocessing.

The examples provided are in C#, but the same thing is possible in JavaScript for NodeJS and Java, and very likely other languages too.


