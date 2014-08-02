using System;
using System.Collections.Generic;

namespace SignalFramework
{
    public class Signal<T> : OnceSignal<T> , ISignal<T>
    {

        public ISlot<T> Add( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener)
        {
            return RegisterListener(listener, false);
        }

    }
}
