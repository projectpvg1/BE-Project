using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Create HTTP Delete Request
    /// </summary>
    public class DelHTTPRequest : HTTPRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Instance of client</param>
        internal DelHTTPRequest(Client client)
            : base(client)
        {
            method = "DELETE";
        }
    }
}
