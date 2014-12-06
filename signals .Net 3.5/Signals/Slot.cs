using System;
using System.Collections;

namespace Signals
{
    public class Slot<T> : ISlot<T>
    {
        protected bool m_enabled = true;
        protected bool m_once = false;
        protected int m_priority = 0;

        protected IOnceSignal<T> m_signal;
        protected Delegate m_listener;
        protected object[] m_params;


        /// <summary>
        /// Creates and returns a new Slot object.
        /// </summary>
        /// 
        /// <param name="listener"> The listener associated with the slot </param>
        /// <param name="signal"> The signal associated with the slot </param> 
        /// <param name="once"> Whether or not the listener should be executed only once </param>
        /// <param name="priority"> The priority of the slot </param>
        /// 
        /// <exception cref="ArgumentError"> Given listener is NULL </exception>
        /// <exception cref="Error"> Internal signal reference has not been set yet </exception>

        public static Slot<T> Create(
            Delegate listener,
            IOnceSignal<T> signal, 
            bool once = false, 
            int priority = 0)
        {
            Slot<T> newSlot = new Slot<T>();

            newSlot.m_listener = listener;
            newSlot.m_once = once;
            newSlot.m_signal = signal;
            newSlot.m_priority = priority;

            newSlot.VerifyListener(listener);

            return newSlot;
        }



        public Delegate Listener()
        {
            return m_listener;
        }

        public void Listener( Delegate value)
        {
            if (value == null)
                throw new ArgumentException("Given listener is null.\nDid you want to set enabled to false instead?");

            VerifyListener(value);
            
            m_listener = value;
        }


        protected void VerifyListener( Delegate listener)
		{
			if (null == listener)
			{
				throw new ArgumentException("Given listener is null.");
			}

			if (null == m_signal)
			{
                throw new Exception("Internal signal reference has not been set yet.");
			}
		}

        public void Execute0()
        {
            if (!m_enabled) return;
            if (m_once) Remove();

            (m_listener as OnceSignal<T>.SignalDelegateArgTemplateCallback0).Invoke();
        }

        public void Execute1(T value)
        {
            if (!m_enabled) return;
            if (m_once) Remove();

            (m_listener as OnceSignal<T>.SignalDelegateArgTemplateCallback1).Invoke(value);
        }

        public object[] Params()
        {
            return m_params;
        }
        public void Params(params object[] value)
        {
            m_params = value;
        }

        public bool Once()
        {
            return m_once;
        }

        public int Priority
        {
            get { return m_priority; }
            set { m_priority = value;  }
        }

        /// <summary>
        /// Whether the listener is called on execution. Defaults to true.
        /// </summary>
        public bool Enabled
        {
            get { return m_enabled; }
            set { m_enabled = value; }
        }

        public void Remove()
		{
            if (m_listener is OnceSignal<T>.SignalDelegateArgTemplateCallback0)
                m_signal.Remove(m_listener as OnceSignal<T>.SignalDelegateArgTemplateCallback0);
            else
                m_signal.Remove(m_listener as OnceSignal<T>.SignalDelegateArgTemplateCallback1);
		}


    }
}
