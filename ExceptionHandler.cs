using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Handles all exceptions. 
    /// </summary>
    public class ExceptionHandler : System.Net.WebException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExceptionHandler()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error Message</param>
        public ExceptionHandler(String message)
            : base(message)
        {
        }
    }
}
