using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Exceptions
{
    /// <summary>
    /// Thrown when attempting to add a dictionary with the same name.
    /// </summary>
    public class DictionaryExistsException : RsgException
    {
        private const string DefaultMessage = "The dictionary you are attempting to add already exists!";

        public DictionaryExistsException()
            : base(DefaultMessage)
        {
        }

        public DictionaryExistsException(string message)
            : base(message)
        {
        }

        public DictionaryExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
