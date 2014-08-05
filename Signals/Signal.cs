using System;
using System.Collections.Generic;

namespace Signals
{
    public class Signal<T> : OnceSignal<T> , ISignal<T>
    {

        public ISlot<T> Add( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener)
        {
            return RegisterListener(listener, false);
        }

        public ISlot<T> Add(OnceSignal<T>.SignalDelegateArgTemplateCallback0 listener)
        {
            return RegisterListener(listener, false);
        }
    }
}
