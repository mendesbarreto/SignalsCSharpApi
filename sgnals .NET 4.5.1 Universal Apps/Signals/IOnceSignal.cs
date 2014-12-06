using System;
using System.Collections;

namespace Signals
{
    public interface IOnceSignal<T>
    {

        /// <summary>
        /// The current number of listeners for the signal.
        /// </summary>
        uint NumListeners
        {
            get;
        }

        /// <summary>
        /// Subscribes a one-time listener for this signal.
		/// The signal will remove the listener automatically the first time it is called,
        /// after the dispatch to all listeners is complete.
        /// </summary>
        /// 
        /// <returns>ISlot which contains the Function passed as the parameter </returns>
        ISlot<T> AddOnce(OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener);


        /// <summary>
        /// Subscribes a one-time listener for this signal.
        /// The signal will remove the listener automatically the first time it is called,
        /// after the dispatch to all listeners is complete.
        /// </summary>
        /// 
        /// <returns>ISlot which contains the Function passed as the parameter </returns>
        ISlot<T> AddOnce(OnceSignal<T>.SignalDelegateArgTemplateCallback0 listener);

        /// <summary>
        /// Dispatches some object to listeners.
        /// The signal will remove the listener automatically the first time it is called,
        /// after the dispatch to all listeners is complete.
        /// </summary>
        void Dispatch(T valueObjects = default(T));


        /// <summary>
        /// Unsubscribes a listener from the signal.
        /// </summary>
        /// 
        /// <param name="listener"> The listener associated with the slot </param>
        /// <returns>ISlot which contains the Function passed as the parameter </returns>
        ISlot<T> Remove( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener);


        ISlot<T> Remove(OnceSignal<T>.SignalDelegateArgTemplateCallback0 listener);

        /// </summary>
        /// Unsubscribes all listeners from the signal.
        void RemoveAll();
    }
}
