using System;
using System.Collections;
namespace SignalFramework
{
    /**
	 * Signal dispatches events to multiple listeners.
	 * It is inspired by C# events and delegates, and by
	 * <a target="_top" href="http://en.wikipedia.org/wiki/Signals_and_slots">signals and slots</a>
	 * in Qt.
	 * A Signal adds event dispatching functionality through composition and interfaces,
	 * rather than inheriting from a dispatcher.
	 * <br/><br/>
	 * Project home: <a target="_top" href="http://github.com/robertpenner/as3-signals/">http://github.com/robertpenner/as3-signals/</a>
	 */
    public class OnceSignal<T> : IOnceSignal<T>
    {
        /*
        public delegate void SignalDelegateArgs( params object[] arguments);
        public delegate void SignalDelegateArgBool( bool value );
        public delegate void SignalDelegateArgInt( int value);
        public delegate void SignalDelegateArgFloat( float value);
        public delegate void SignalDelegateArgDouble( double value);
        public delegate void SignalDelegateArgStr( string value );
        public delegate void SignalDelegateArgumentSig( Signalling signal );
        public delegate void SignalDelegate();
        */

        public delegate void SignalDelegateArgTemplateCallback0();
        public delegate void SignalDelegateArgTemplateCallback1(T value);

        //public delegate void SignalDelegateArgTemplateCallback2<T1, T2>(T1 value, T2 value2);
        //public delegate void SignalDelegateArgTemplateCallback3<T1, T2, T3>(T1 value, T2 value2, T3 value3);
        
        //protected ArrayList m_valueClasses = null;
        protected SlotList<T> m_slots = SlotList<T>.NIL;

        /**
        * Creates a Signal instance to dispatch value objects.
        * @param	valueClasses Any number of class references that enable type checks in dispatch().
        * For example, new Signal(String, uint)
        * would allow: signal.dispatch("the Answer", 42)
        * but not: signal.dispatch(true, 42.5)
        * nor: signal.dispatch()
        *
        * NOTE: In AS3, subclasses cannot call super.apply(null, valueClasses),
        * but this constructor has logic to support super(valueClasses).
        * 
        */

        public OnceSignal()
        {

        }

        public void Dispatch(T valueObjects)
        {
			// Broadcast to listeners.
            SlotList<T> slotsToProcess = m_slots;
			
            if(slotsToProcess.nonEmpty)
			{
				while (slotsToProcess.nonEmpty)
				{
					slotsToProcess.head.Execute(valueObjects);
					slotsToProcess = slotsToProcess.tail;
				}
			}
        }


        public ArrayList ValueClasses() { return null; }
        public void ValueClasses(ArrayList value)
        {
            //TODO: Make the signal call a determined function with a determined arguments
        }

        public uint NumListeners
        {
            get { return m_slots.Length();  }
        }

        public ISlot<T> AddOnce( SignalDelegateArgTemplateCallback1 listener)
        {
            return RegisterListener(listener, true);
        }


        protected ISlot<T> RegisterListener( SignalDelegateArgTemplateCallback1 listener, bool once = false)
		{
			if ( RegistrationPossible(listener, once) )
			{
                ISlot<T> newSlot = Slot<T>.Create(listener, this, once);
                m_slots = m_slots.Prepend(newSlot);
				return newSlot;
			}

            return m_slots.Find(listener);
		}
        protected bool RegistrationPossible(SignalDelegateArgTemplateCallback1 listener, bool once)
		{
			if (!m_slots.nonEmpty) return true;
			
			ISlot<T> existingSlot = m_slots.Find(listener);
			
            if ( existingSlot == null ) return true;

			if ( existingSlot.Once() != once )
			{
				// If the listener was previously added, definitely don't add it again.
				// But throw an exception if their once values differ.
				throw new InvalidOperationException("You cannot addOnce() then add() the same listener without removing the relationship first.");
			}
			
			return false; // Listener was already registered.
		}


        public ISlot<T> Remove(SignalDelegateArgTemplateCallback1 listener)
        {
            ISlot<T> slot = m_slots.Find(listener);

            if (slot == null) return null;

            m_slots = m_slots.FilterNot(listener);

            return slot;
        }

        public void RemoveAll()
        {
            m_slots = SlotList<T>.NIL;
        }

    }
}
