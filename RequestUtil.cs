using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Creates URL for CRUD Operations on container and data objects
    /// </summary>
    public class RequestUtil
    {
        /// <summary>
        /// Forms Authentication URL
        /// </summary>
        /// <param name="client">Instance of client</param>
        /// <returns>Authentication URL</returns>
        internal static String formAuthURL(Client client)
        {
            return CommonConstants.HTTP_PREFIX + client.serverIP + ":" +
                CommonConstants.PORT_NUMBER_CONSTANT + "/" +
                CommonConstants.AUTH_URL_CONSTANT;
        }

        /// <summary>
        /// Forms Account URL
        /// </summary>
        /// <param name="client">Instance of client</param>
        /// <returns>Account URL</returns>
        internal static String formAccountURL(Client client)
        {
            return CommonConstants.HTTP_PREFIX + client.serverIP + ":" +
                CommonConstants.PORT_NUMBER_CONSTANT +
                CommonConstants.CDMI_PATH_CONSTANT +
                client.username + "/";
        }

        /// <summary>
        /// Forms Container URL
        /// </summary>
        /// <param name="client">Instance of client</param>
        /// <param name="path">Path to container</param>
        /// <returns>Container URL</returns>
        internal static String formContainerURL(Client client, String path)
        {
            return CommonConstants.HTTP_PREFIX + client.serverIP + ":" +
                CommonConstants.PORT_NUMBER_CONSTANT +
                path + "/";
        }

        /// <summary>
        /// Forms Object URL
        /// </summary>
        /// <param name="client">Instance of client</param>
        /// <param name="path">Path to object</param>
        /// <returns>Object URL</returns>
        internal static String formObjectURL(Client client, String path)
        {
            return CommonConstants.HTTP_PREFIX + client.serverIP + ":" +
                CommonConstants.PORT_NUMBER_CONSTANT +
                path;
        }

    }
}
