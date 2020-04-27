using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Triangle
{
    public class ParseException : Exception
    {
        public ParseException() { }

        public ParseException(string message) : base(message) { }

        public ParseException(string message, Exception innerException) : base(message, innerException) { }

        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
