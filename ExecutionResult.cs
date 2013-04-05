using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Stores the response obtained from HTTP Requests
    /// </summary>
    public class ExecutionResult
    {
        Stream dataStream;
        String status;
        internal String responseFromServer;
        internal System.Net.WebResponse response;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">Response of type System.Net.WebResponse</param>
        internal ExecutionResult(System.Net.WebResponse response)
        {
            this.status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            this.response = response;
            response.Close();
        }

    }
}
