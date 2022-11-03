using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableStack
{
    /*
     * Who was here?
     * Steven
     * Ben Shull
     * Cecilia
     * Ethan
     * Jordan
     * Weston
     * Luke
     * Anna 
     * Collin
     * Anna 2
     * Zach
     */
    public class Stack
    {
        public object Value { get; }
        public Stack Next { get; }

        public Stack(Stack next, object value)
        {
            this.Next = next;
            this.Value = value;
        }

        protected Stack()
        {

        }

        public virtual Stack Push(object item)
        {
            return new Stack(this, item);
        }

        public Stack Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Tried to call pop on empty stack.");
            }

            if (this.Next == null)
            {
                return EmptyStack.Instance;
            }
            return Next;
        }

        public object Peek()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Tried to call peek on empty stack.");
            }
            return this.Value;
        }

        public virtual bool IsEmpty()
        {
            return false;
        }
    }
}
