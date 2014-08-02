using System;
using System.Collections;

namespace SignalFramework
{
    public interface ISlot<T>
    {
        /**
		 * The listener associated with this slot.
		 */
        OnceSignal<T>.SignalDelegateArgTemplateCallback1 Listener();
        void Listener(OnceSignal<T>.SignalDelegateArgTemplateCallback1 value);

        /**
		 * Allows the ISlot to inject parameters when dispatching. The params will be at 
		 * the tail of the arguments and the ISignal arguments will be at the head.
		 * 
		 * var signal:ISignal = new Signal(String);
		 * signal.add(handler).params = [42];
		 * signal.dispatch('The Answer');
		 * function handler(name:String, num:int):void{}
		 */
        object[] Params();
        void Params(params object[] value);


        /**
		 * Whether this slot is automatically removed after it has been used once.
		 */
        bool Once();

        /**
		 * The priority of this slot should be given in the execution order.
		 * An IPrioritySignal will call higher numbers before lower ones.
		 * Defaults to 0.
		 */
        int Priority
        {
            get;
            set;
        }

        /**
		 * Whether the listener is called on execution. Defaults to true.
		 */
        bool Enabled
        {
            get;
            set;
        }


       
        /**
		 * Executes a listener without arguments.
		 * Existing <code>params</code> are appended before the listener is called.
		 */
        //void Execute();
       
        /**
		 * Dispatches one argument to a listener.
		 * Existing <code>params</code> are appended before the listener is called.
		 * @param value The argument for the listener.
		 */
        void Execute(object value);
        
        /**
		 * Executes a listener of arity <code>n</code> where <code>n</code> is
		 * <code>valueObjects.length</code>.
		 * Existing <code>params</code> are appended before the listener is called.
		 * @param valueObjects The array of arguments to be applied to the listener.
		 */
        //void Execute(object[] valueObjects);


        /**
         * Removes the slot from its signal.
         */
        void Remove();
    }
}
