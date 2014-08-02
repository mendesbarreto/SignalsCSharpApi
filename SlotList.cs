using System;
using System.Collections.Generic;

namespace SignalFramework
{
    /**
	 * The SlotList class represents an immutable list of Slot objects.
	 *
	 * @author Joa Ebert
	 * @author Robert Penner
	 */
    public sealed class SlotList<T>
    {
        /**
        * Represents an empty list. Used as the list terminator.
        */
        readonly public  static SlotList<T> NIL = SlotList<T>.Create(null, null);

        public ISlot<T> head = null;
        public SlotList<T> tail = null;
        public bool nonEmpty = false;

       /**
       * Creates and returns a new SlotList object.
       *
       * <p>A user never has to create a SlotList manually. 
       * Use the <code>NIL</code> element to represent an empty list. 
       * <code>NIL.prepend(value)</code> would create a list containing <code>value</code></p>.
       *
       * @param head The first slot in the list.
       * @param tail A list containing all slots except head.
       * 
       * @throws ArgumentError <code>ArgumentError</code>: Parameters head and tail are null. Use the NIL element instead.
       * @throws ArgumentError <code>ArgumentError</code>: Parameter head cannot be null.
       */
        public static SlotList<T> Create( ISlot<T> head, SlotList<T> tail = null )
        {

            SlotList<T> newList = new SlotList<T>();


            if( head == null && tail == null )
            {

                if( NIL != null )
                {
                    throw new ArgumentException("Parameters head and tail are null. Use the NIL element instead");
                }


                newList.nonEmpty = false;
            }
            else if ( head == null ) 
            {
                throw new ArgumentException("Parameter head cannot be null");

            } else 
            {
                newList.head = head;

                if( tail != null )
                    newList.tail = tail;

                else if (NIL != null)
                    newList.tail = NIL;

                newList.nonEmpty = true;
            }


            return newList;
        }


        /**
		 * The number of slots in the list.
		 */
        public uint Length()
        {
            if (!nonEmpty) return 0;
            if (tail == NIL) return 1;

            uint result = 0;
            SlotList<T> p = this;

            while(p.nonEmpty)
            {
                ++result;
                p = p.tail;
            }

            return result;
        }

        /**
		 * Prepends a slot to this list.
		 * @param	slot The item to be prepended.
		 * @return	A list consisting of slot followed by all elements of this list.
		 * 
		 * @throws ArgumentError <code>ArgumentError</code>: Parameter head cannot be null.
		 */
        public SlotList<T> Prepend(ISlot<T> slot)
		{
			return SlotList<T>.Create(slot, this);
		}


        /**
		 * Appends a slot to this list.
		 * Note: appending is O(n). Where possible, prepend which is O(1).
		 * In some cases, many list items must be cloned to 
		 * avoid changing existing lists.
		 * @param	slot The item to be appended.
		 * @return	A list consisting of all elements of this list followed by slot.
		 */
		public SlotList<T> Append(ISlot<T> slot)
		{
			if (slot == null) return this;
			if (!nonEmpty) return SlotList<T>.Create(slot);
			
            // Special case: just one slot currently in the list.
			if (tail == NIL) 
				return SlotList<T>.Create(slot).Prepend(head);
			
			// The list already has two or more slots.
			// We have to build a new list with cloned items because they are immutable.
			SlotList<T> wholeClone = SlotList<T>.Create(head);
			SlotList<T> subClone = wholeClone;
            SlotList<T> current = tail;

			while (current.nonEmpty)
			{
				subClone = subClone.tail = SlotList<T>.Create(current.head);
				current = current.tail;
			}
			// Append the new slot last.
			subClone.tail = SlotList<T>.Create(slot);
			return wholeClone;
		}

        /**
		 * Insert a slot into the list in a position according to its priority.
		 * The higher the priority, the closer the item will be inserted to the list head.
		 * @params slot The item to be inserted.
		 * 
		 * @throws ArgumentError <code>ArgumentError</code>: Parameters head and tail are null. Use the NIL element instead.
		 * @throws ArgumentError <code>ArgumentError</code>: Parameter head cannot be null.
		 */
		public SlotList<T> InsertWithPriority(ISlot<T> slot)
		{
			if (!nonEmpty) return SlotList<T>.Create(slot);

			int priority = slot.Priority;
			// Special case: new slot has the highest priority.
			if (priority > this.head.Priority) return Prepend(slot);

			SlotList<T> wholeClone = SlotList<T>.Create(head);
			SlotList<T> subClone = wholeClone;
			SlotList<T> current = tail;

			// Find a slot with lower priority and go in front of it.
			while (current.nonEmpty)
			{
				if (priority > current.head.Priority)
				{
					subClone.tail = current.Prepend(slot);
					return wholeClone; 
				}			
				subClone = subClone.tail = SlotList<T>.Create(current.head);
				current = current.tail;
			}

			// Slot has lowest priority.
			subClone.tail = SlotList<T>.Create(slot);
			return wholeClone;
		}

        /**
		 * Returns the slots in this list that do not contain the supplied listener.
		 * Note: assumes the listener is not repeated within the list.
		 * @param	listener The function to remove.
		 * @return A list consisting of all elements of this list that do not have listener.
		 */
        public SlotList<T> FilterNot( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener)
		{
			if (!nonEmpty || listener == null) return this;

			if ( listener.Equals( head.Listener() ) ) return tail;

			// The first item wasn't a match so the filtered list will contain it.
			SlotList<T> wholeClone = SlotList<T>.Create(head);
			SlotList<T> subClone = wholeClone;
            SlotList<T>current = tail;
			
			while (current.nonEmpty)
			{
				if (current.head.Listener().Equals(listener) )
				{
					// Splice out the current head.
					subClone.tail = current.tail;
					return wholeClone;
				}
				
				subClone = subClone.tail = SlotList<T>.Create(current.head);
				current = current.tail;
			}

			// The listener was not found so this list is unchanged.
			return this;
		}

		
        /**
		 * Determines whether the supplied listener Function is contained within this list
		 */
        public bool Contains( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener)
		{
			if (!nonEmpty) return false;

            SlotList<T> p = this;
			while (p.nonEmpty)
			{
				if (p.head.Listener().Equals(listener)) return true;
				p = p.tail;
			}

			return false;
		}

        	/**
		 * Retrieves the ISlot associated with a supplied listener within the SlotList.
		 * @param   listener The Function being searched for
		 * @return  The ISlot in this list associated with the listener parameter through the ISlot.listener property.
		 * Returns null if no such ISlot instance exists or the list is empty.  
		 */
		public ISlot<T> Find( OnceSignal<T>.SignalDelegateArgTemplateCallback1 listener)
		{
			if (!nonEmpty) return null;

			SlotList<T> p = this;
			while (p.nonEmpty)
			{
				if (p.head.Listener().Equals(listener)) return p.head;
				p = p.tail;
			}

			return null;
		}

		public string toString()
		{
			string buffer = "";
			SlotList<T> p = this;

			while (p.nonEmpty)
			{
				buffer += p.head + " -> ";
				p = p.tail;
			}

			buffer += "NIL";

			return "[List "+buffer+"]";
		}

    }
}
