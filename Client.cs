using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// This class is used as wrapper for underlying structure 
    /// and does basic authentication, handler functions
    /// </summary>
    public class Client
    {
        internal String username, groupname, password, serverIP, token;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serverIP">IP address of Server</param>
        /// <param name="username">Username</param>
        /// <param name="groupname">Group Name</param>
        /// <param name="password">Password</param>
        public Client(String serverIP, String username, String groupname,
            String password)
        {
            this.serverIP = serverIP;
            this.username = username;
            this.groupname = groupname;
            this.password = password;
        }

        /// <summary>
        /// get the Auth Token from Server which can be used for any operation later.
        /// </summary>
        /// <returns>True if token generated
        /// False otherwise
        /// </returns>
        public Boolean getAuthorizationToken()
        {
            try
            {
                GetHTTPRequest request = new GetHTTPRequest(this);
                request.createHTTPRequest(Resource1.AuthRequest);
                ExecutionResult executionResult = request.executeRequest();
                token = executionResult.response.Headers
                    [CommonConstants.X_AUTH_TOKEN_CONSTANT];
                if (token != null)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to obtain the Handler
        /// </summary>
        /// <returns>Returns object of Handler class</returns>
        public Handler getHandler()
        {
            return new Handler(this);
        }

    }
}

