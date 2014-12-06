using System;
using System.Collections;
namespace Signals
{
    
    /// <summary>
    /// Signal dispatches events to multiple listeners.
    /// </summary>
    public class OnceSignal<T> : IOnceSignal<T>
    {
        public delegate void SignalDelegateArgTemplateCallback0();
        public delegate void SignalDelegateArgTemplateCallback1(T value);

        protected SlotList<T> m_slots = SlotList<T>.NIL;
        
        /// <summary>
        /// Creates a Signal instance to dispatch value objects.
        /// </summary>
        public OnceSignal()
        {

        }

        public void Dispatch( T valueObjects = default(T) )
        {
			// Broadcast to listeners.
            SlotList<T> slotsToProcess = m_slots;
			
            if(slotsToProcess.nonEmpty)
			{
				while (slotsToProcess.nonEmpty)
				{
                    if (valueObjects != null)
					    slotsToProcess.head.Execute1(valueObjects);
                    else
                    {
                        if (typeof(T) == typeof(empty))
                            slotsToProcess.head.Execute0();
                        else
                            throw new Exception("The function is wainting some object as parameter of type: " + typeof(T).ToString() );
                    }

					slotsToProcess = slotsToProcess.tail;
				}
			}
        }

        public uint NumListeners
        {
            get { return m_slots.Length();  }
        }

        public ISlot<T> AddOnce(OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener)
        {
            return RegisterListener(listener, true);
        }

        public ISlot<T> AddOnce(OnceSignal<T>.SignalDelegateArgTemplateCallback0 listener )
        {
            return RegisterListener(listener, true);
        }

        protected ISlot<T> RegisterListener( Delegate listener, bool once = false)
        {
            if (RegistrationPossible(listener, once))
            {
                ISlot<T> newSlot = Slot<T>.Create(listener, this, once);
                m_slots = m_slots.Prepend(newSlot);
                return newSlot;
            }

            return m_slots.Find(listener);
        }


        protected bool RegistrationPossible( Delegate listener, bool once)
		{
			if (!m_slots.nonEmpty) return true;
			
			ISlot<T> existingSlot = m_slots.Find(listener);
			
            if ( existingSlot == null ) return true;

			if ( existingSlot.Once() != once )
			{
				throw new InvalidOperationException("You cannot addOnce() then add() the same listener without removing the relationship first.");
			}
			
			return false;
		}

        public ISlot<T> Remove( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener)
        {
            ISlot<T> slot = m_slots.Find(listener);

            if (slot == null) return null;

            m_slots = m_slots.FilterNot(listener);

            return slot;
        }

        public ISlot<T> Remove( OnceSignal<T>.SignalDelegateArgTemplateCallback0 listener)
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
