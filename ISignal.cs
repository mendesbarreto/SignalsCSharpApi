using System;

namespace SignalFramework
{
    public interface ISignal<T>
    {
       /**
		 * Subscribes a listener for the signal.
		 * @param	listener A function with arguments
		 * that matches the value classes dispatched by the signal.
		 * If value classes are not specified (e.g. via Signal constructor), dispatch() can be called without arguments.
		 * @return a ISlot, which contains the Function passed as the parameter
		 */
        ISlot<T> Add(OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener);
    }
}
