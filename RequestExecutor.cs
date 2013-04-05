using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Defines a PUT HTTP Request
    /// </summary>
    public class PutHTTPRequest : HTTPRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Instance of client</param>
        internal PutHTTPRequest(Client client)
            : base(client)
        {
            method = "PUT";
        }

        /// <summary>
        /// Add body to current HTTP Request
        /// </summary>
        /// <param name="buf">Body to be added as byte array</param>
        internal void addBody(byte[] buf)
        {
            request.GetRequestStream().Write(buf, 0, buf.Length);
        }
    }
}
