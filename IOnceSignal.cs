using System;
using System.Collections;

namespace SignalFramework
{
    public interface IOnceSignal<T>
    {
        /**
		 * An optional array of classes defining the types of parameters sent to listeners.
		 */
        ArrayList ValueClasses();
        void ValueClasses(ArrayList value);

        /** The current number of listeners for the signal. */
        uint NumListeners
        {
            get;
        }

        /**
		 * Subscribes a one-time listener for this signal.
		 * The signal will remove the listener automatically the first time it is called,
		 * after the dispatch to all listeners is complete.
		 * @param	listener A function with arguments
		 * that matches the value classes dispatched by the signal.
		 * If value classes are not specified (e.g. via Signal constructor), dispatch() can be called without arguments.
		 * @return a ISlot, which contains the Function passed as the parameter
		 */
        ISlot<T> AddOnce( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener );

        /**
		 * Dispatches an object to listeners.
		 * @param	valueObjects	Any number of parameters to send to listeners. Will be type-checked against valueClasses.
		 * @throws	ArgumentError	<code>ArgumentError</code>:	valueObjects are not compatible with valueClasses.
		 */
        void Dispatch( T valueObjects);

        /**
		 * Unsubscribes a listener from the signal.
		 * @param	listener
		 * @return a ISlot, which contains the Function passed as the parameter
		 */
        ISlot<T> Remove(OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener);

        /**
		 * Unsubscribes all listeners from the signal.
		 */
        void RemoveAll();
    }
}
