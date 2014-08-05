using System;
using System.Collections;

namespace Signals
{
    public interface ISlot<T>
    {
        Delegate Listener();
        void Listener(Delegate value);

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

        void Execute0();
        void Execute1(T value);

        void Remove();
    }
}
