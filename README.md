SignalsCSharpAPI
================

BUILDING PAGE NOT READ!!!

Welcome to SignalsCSharpApi page
=========

What is it?
------

SignalsCSharpApi is an open-source C# library witch simplify the messaging between objects and makes it easy to implement the Observer pattern while avoiding boilerplate your code. This library could be used in place of delegate and C# events.

The main idea behind this library is make you code clear and easy to add and remove "listeners" of a object event.

Before we start, I need you have some concepts in your mind such as:

Signal is a dispatcher of a single event in a specific object and hods a list of your own listeners.
Signal works like a delegate and Events, the difference in use signals are: You have less code to bore you. =)
The most common way to use this library is with user interface, but can be used to many ways especially in gaming applications.
Ok, Let's see some code!

TODO: INSTALL TUTORIAL

How to Start
------

//Here the declaration
public Signal<int> OnTakeDamage = new Signal<int>();
Above is the declaration, very simple. The "Signal" is the class which we will use to bring the signal to life and the type between "<>" is what the signal will push to the listener. "Obs: If you want nothing use Signal.Void class".

Adding Listeners
------

//Add a listener to our signal. 
//The OnHeroTakeDamageHandler is the listener, the function that will be called when the hero take some damage. 
OnTakeDamage.Add(OnHeroTakeDamageHandler);

//OR

// that handler only be called once.
OnTakeDamage.AddOnce(OnHeroTakeDamageHandler);
For add listeners we have two forms, the first one is the method "Add()", when you use this method the system will register the listener in the Signal and will be called every time the Signal dispatch. The second one is the method "AddOnce()" it work in the same way that the "Add()" but the system call once, ie, will be removed automatically.

Remove Listeners
------

//Remove a specific listener 
OnTakeDamage.Remove(OnHeroTakeDamageHandler);

//OR

//Remove All listeners
OnTakeDamage.RemoveAll()
Dispatching
OnTakeDamage.Dispatch(10);
To dispatch the signal to the listeners is simple, use the "Dispatch()" method, and pass between "()" the value of type specified in the signal declaration, in this case "int".

Support or Contact
------
Having trouble with Pages? Check out the documentation at https://help.github.com/pages or contact mendes-barreto@live.com and we’ll help you sort it out.
