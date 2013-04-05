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
    /// Defines an HTTP request
    /// <member>Stores type of webrequest</member>
    /// </summary>
    public class HTTPRequest : System.Net.WebRequest
    {
        Client client;
        internal WebRequest request;
        internal String method;

        /// <summary>
        /// Default Constructor
        /// </summary>
        internal HTTPRequest()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Instance of client</param>
        internal HTTPRequest(Client client)
        {
            this.client = client;
        }

        /// <summary>
        /// Add specific header to HTTP Request
        /// </summary>
        /// <param name="headerName">Name of header</param>
        /// <param name="headerValue">Value of header</param>
        private void addHeader(string headerName, string headerValue)
        {
            request.Headers.Add(headerName, headerValue);
        }

        /// <summary>
        /// Creates HTTP Request
        /// </summary>
        /// <param name="RequestType">Type of HTTP Request to be created
        /// Authentication or Account</param>
        internal void createHTTPRequest(String RequestType)
        {
            if (RequestType == Resource1.AuthRequest)
            {
                request = WebRequest.Create(RequestUtil.formAuthURL(client));
                addHeader(CommonConstants.X_STORAGE_USER_CONSTANT,
                    client.username + ":" + client.groupname);
                addHeader(CommonConstants.X_STORAGE_PASS_CONSTANT,
                    client.password);
            }

            if (RequestType == Resource1.AccountRequest)
            {
                request = WebRequest.Create(RequestUtil.formAccountURL(client));
                addHeader(CommonConstants.X_AUTH_TOKEN_CONSTANT,
                    client.token);
                addHeader(CommonConstants.X_CDMI_VERSION_CONSTANT,
                    CommonConstants.X_CDMI_VERSION_VALUE_CONSTANT);
            }
            request.Method = method;
        }

        /// <summary>
        /// Creates HTTP Request
        /// </summary>
        /// <param name="path">Path to container or object</param>
        /// <param name="RequestType">Type of HTTP Request
        /// Container or Object</param>
        internal void createHTTPRequest(String path, String RequestType)
        {
            if (RequestType == Resource1.ContainerRequest)
            {
                request = WebRequest.Create(RequestUtil.formContainerURL(client, path));
                addHeader(CommonConstants.X_AUTH_TOKEN_CONSTANT,
                    client.token);
                addHeader(CommonConstants.X_CDMI_VERSION_CONSTANT,
                    CommonConstants.X_CDMI_VERSION_VALUE_CONSTANT);
                request.ContentType = CommonConstants.CDMI_CONTAINER_CONSTANT;
            }
            if (RequestType == Resource1.ObjectRequest)
            {
                request = WebRequest.Create(RequestUtil.formObjectURL(client, path));
                addHeader(CommonConstants.X_AUTH_TOKEN_CONSTANT,
                    client.token);
                addHeader(CommonConstants.X_CDMI_VERSION_CONSTANT,
                    CommonConstants.X_CDMI_VERSION_VALUE_CONSTANT);
                request.ContentType = CommonConstants.CDMI_OBJECT_CONSTANT;
            }
            request.Method = method;
        }

        /// <summary>
        /// Executes HTTP Request
        /// </summary>
        /// <returns>ExecutionResult</returns>
        internal ExecutionResult executeRequest()
        {
            try
            {
                return RequestExecutor.executeRequest(this);
            }
            catch (ExceptionHandler)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Response to HTTP Request
        /// </summary>
        /// <returns>Response of type System.Net.WebResponse</returns>
        public override System.Net.WebResponse GetResponse()
        {
            try
            {
                WebResponse response = request.GetResponse();
                return response;
            }
            catch (System.Net.WebException webException)
            {
                throw new ExceptionHandler(webException.Message);
            }
        }

    }
}
