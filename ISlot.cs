using System;
using System.Collections;

namespace SignalFramework
{
    public interface ISlot<T>
    {
        OnceSignal<T>.SignalDelegateArgTemplateCallback1 Listener();
        void Listener(OnceSignal<T>.SignalDelegateArgTemplateCallback1 value);

        object[] Params();
        void Params(params object[] value);

        bool Once();

        int Priority
        {
            get;
            set;
        }

        bool Enabled
        {
            get;
            set;
        }

        void Execute(object value);
        
        void Remove();
    }
}
