using System;

namespace Signals
{
    public interface ISignal<T>
    {
        /// <summary>
        /// Add a listener for the signal. 
        /// </summary>
        /// <param name="listener"> The listener parameter receive a function which has the same return and parameter value specified in the Signal declaration </param>
        ISlot<T> Add(OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener);
        ISlot<T> Add(OnceSignal<T>.SignalDelegateArgTemplateCallback0 listener);
    }
}
