using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// This class defines the constants used in SDK.
    /// </summary>
    public class CommonConstants
    {
        internal static String CDMI_CONTAINER_CONSTANT = "application/cdmi-container";
        internal static String CONTENT_LENGTH = "Content-Length";
        internal static String CONTENT_TYPE = "Content-Type";
        internal static String HTTP_PREFIX = "http://";
        internal static String X_CDMI_VERSION_CONSTANT = "X-CDMI-Specification-Version";
        internal static String X_CDMI_VERSION_VALUE_CONSTANT = "1.0.1";
        internal static String CDMI_OBJECT_CONSTANT = "application/cdmi-object";
        internal static String X_STORAGE_USER_CONSTANT = "X-Storage-User";
        internal static String X_STORAGE_PASS_CONSTANT = "X-Storage-Pass";
        internal static String X_AUTH_TOKEN_CONSTANT = "X-Auth-Token";
        internal static String PORT_NUMBER_CONSTANT = "8080";
        internal static String AUTH_URL_CONSTANT = "auth/v1.0/";
        internal static String CDMI_PATH_CONSTANT = "/cdmi/AUTH_";
    }
}
