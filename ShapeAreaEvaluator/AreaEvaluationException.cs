using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapeAreaEvaluator
{
    public class AreaEvaluationException : Exception
    {
        public AreaEvaluationException() : base() { }

        public AreaEvaluationException(string message) : base(message) { }

        public AreaEvaluationException(string message, Exception innerException) : base(message, innerException) { }

    }
}
