SignalsCSharpAPI
================


What is it?
------

Signals Api is an open-source C# library which simplifies the messaging between objects and makes easy the implementation of Observer pattern, while avoiding boilerplate your code.
This library could be used in place of delegate and C# events.
The main idea behind this library is make you code clear and easy to add and remove "listeners" of a specific object event.
Before we start, you need to know some concepts such as:

* Listener is a method which will be called after the signal was dispatched.

* Signal is a dispatcher of a single event in a specific object and has and holds a list of your own listeners which can be removed or added as needed by the programmer.

* Signal works like a delegate and Events, the difference in use signals are: You have less code to bore you and The workflow is simpler than "delegates". =)

* The most common way to use this library is with user interface, but can be used to many ways especially in gaming applications. 

Ok, Let's see some code!

TODO: INSTALL TUTORIAL

How to Start
------
```csharp
//Here the declaration
public Signal<int> OnTakeDamage = new Signal<int>();
```
Above is the declaration. The "Signal" is the class which we will use to bring the signal to life and the type between "<>" is what the signal will push to the listener. "Obs: If you want nothing use Signal.empty class".

Adding Listeners
------
```csharp
//Add a listener to our signal. 
//The OnHeroTakeDamageHandler is the listener, this method will be called when the hero take some damage.
OnTakeDamage.Add(OnHeroTakeDamageHandler);

//OR

// that handler will be called once.
OnTakeDamage.AddOnce(OnHeroTakeDamageHandler);
```
For add listeners we have two forms, the first one is the method "Add()", when you use this method the system will register the listener in the Signal and each time that signal was dispatched the method will be called. The second one is the method "AddOnce()" it work in the same way that the "Add()" but the signal call the method only once, ie, after the first call the listener will be removed automatically.

Remove Listeners
------
```csharp
//Remove a specific listener 
OnTakeDamage.Remove(OnHeroTakeDamageHandler);

//OR

//Remove All listeners
OnTakeDamage.RemoveAll()
```

Dispatching
------

```csharp
OnTakeDamage.Dispatch(10);
```

To dispatch the signal to the listeners is simple, use the "Dispatch()" method, and pass between "()" the value of type specified in the signal declaration, in this case "int".

Support or Contact
------
Having trouble with Pages? Check out the documentation at https://Come_soon.com or contact mendes-barreto@live.com and we’ll help you sort it out.

References and Information
------

* [Pattern Observer](http://en.wikipedia.org/wiki/Observer_pattern "Pattern Observer")
* [Signals with Boost C++](http://www.boost.org/doc/libs/1_51_0/doc/html/signals.html "Boost Signals")
* [Signals & Slots QT](http://qt-project.org/doc/qt-4.8/signalsandslots.html "QT")

