using System;
using System.Collections.Generic;

namespace Signals
{
    /// <summary>
    /// 
    /// The SlotList class represents an immutable list of Slot objects.
    /// 
    /// Author: Joa Ebert
	/// Author: Robert Penner
    /// Adapted by  Douglas Mendes
    /// 
    /// </summary>
    public sealed class SlotList<T>
    {
        /// <summary>
        /// 
        /// Represents an empty list. Used as the list terminator.
        /// 
        /// </summary>
        readonly public  static SlotList<T> NIL = SlotList<T>.Create(null, null);

        public ISlot<T> head = null;
        public SlotList<T> tail = null;
        public bool nonEmpty = false;

        /// <summary>
        /// 
        /// Creates and returns a new SlotList object.
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// The number of slots in the list.
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// Prepends a slot to this list.
        /// 
        /// </summary>
        public SlotList<T> Prepend(ISlot<T> slot)
		{
			return SlotList<T>.Create(slot, this);
		}


        /// <summary>
        /// 
        /// Appends a slot to this list.
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// Insert a slot into the list in a position according to its priority.
        /// The higher the priority, the closer the item will be inserted to the list head.
        /// 
        /// </summary>
        /// 
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

        /// <summary>
        /// 
        /// Returns the slots in this list that do not contain the supplied listener.
        /// Note: assumes the listener is not repeated within the list.
        /// 
        /// </summary>
        public SlotList<T> FilterNot( Delegate listener)
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

		
        /// <summary>
        /// 
        /// Determines whether the supplied listener Function is contained within this list
        /// 
        /// </summary>
        public bool Contains( Delegate listener)
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

        /// <summary>
        /// 
        /// Retrieves the ISlot associated with a supplied listener within the SlotList.
        /// 
        /// </summary>
		public ISlot<T> Find( Delegate listener)
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
