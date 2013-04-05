using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Creates GET HTTP Request
    /// </summary>
    public class GetHTTPRequest : HTTPRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Instance of client</param>
        internal GetHTTPRequest(Client client)
            : base(client)
        {
            method = "GET";
        }
    }
}
