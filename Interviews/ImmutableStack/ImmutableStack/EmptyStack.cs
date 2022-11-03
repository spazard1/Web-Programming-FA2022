using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableStack
{
    public class EmptyStack : Stack
    {
        public static readonly Stack Instance = new EmptyStack();

        private EmptyStack()
        {

        }

        public override Stack Push(object item)
        {
            return new Stack(null, item);
        }

        public override bool IsEmpty()
        {
            return true;
        }

    }
}
