using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingEditor
{
    [Serializable()]
    class SortedSetFilledException : System.Exception
    {
        public SortedSetFilledException() : base() { }
    public SortedSetFilledException(string message) : base(message) { }
    public SortedSetFilledException(string message, System.Exception inner) : base(message, inner) { }

    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client. 
    protected SortedSetFilledException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    {
    }

    }
}
