using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Defines data object
    /// </summary>
    public class Object
    {
        Metadata metadata;
        internal Client client;
        internal String path;
        String mimetype;
        String objectName;
        String capabilitiesURI;
        String parentURI;
        String objectType;
        String valueRange;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Instance of Client</param>
        /// <param name="path">Path to object</param>
        public Object(Client client, String path)
        {
            try
            {
                this.client = client;
                this.path = path;
                toString();
            }
            catch (ExceptionHandler)
            {

            }
        }

        /// <summary>
        /// Check if the Get Child Object has been successful or not
        /// </summary>
        /// <returns>
        /// True if successful
        /// False otherwise
        /// </returns>
        public bool isGetSuccessful()
        {
            if (objectType.Equals(CommonConstants.CDMI_OBJECT_CONSTANT))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Get parent container of object
        /// </summary>
        /// <returns>Parent Container</returns>
        public Container getParentContainer()
        {
            String parentPath = path;
            int pathLength = parentPath.Length;
            for (int counter = pathLength - 1; counter >= 0; counter--)
            {

                if (parentPath[counter] == '/')
                {
                    parentPath = parentPath.TrimEnd(parentPath[counter]);
                    break;
                }
                parentPath = parentPath.TrimEnd(parentPath[counter]);
            }
            Container container = new Container(client, parentPath);
            return container;
        }

        /// <summary>
        /// Write object to local file
        /// </summary>
        /// <param name="filePath">Path to local file</param>
        /// <returns>True if object is written in a file successfully
        /// False otherwise</returns>
        public Boolean writeObjectToFile(String filePath)
        {
            try
            {
                String value = getValue();
                File.WriteAllText(filePath, value);
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }

        /// <summary>
        /// Get detais of object from JSON Parser
        /// </summary>
        private void toString()
        {
            try
            {

                GetHTTPRequest request = new GetHTTPRequest(this.client);
                request.createHTTPRequest(path, Resource1.ObjectRequest);
                ExecutionResult executionResult = request.executeRequest();
                mimetype = JSONParser.getFieldValue("mimetype",
                    executionResult.responseFromServer);
                objectName = JSONParser.getFieldValue("objectName",
                    executionResult.responseFromServer);
                capabilitiesURI = JSONParser.getFieldValue("capabilitiesURI",
                    executionResult.responseFromServer);
                parentURI = JSONParser.getFieldValue("parentURI",
                    executionResult.responseFromServer);
                parentURI = parentURI.Remove(parentURI.Length - 1);
                valueRange = JSONParser.getFieldValue("valuerange",
                    executionResult.responseFromServer);
                metadata = new Metadata(JSONParser.getFieldValue("metadata",
                    executionResult.responseFromServer));
                objectType = JSONParser.getFieldValue("objectType",
                    executionResult.responseFromServer);
            }
            catch (ExceptionHandler)
            { }

        }

        /// <summary>
        /// Returns Object Name
        /// </summary>
        /// <returns>String containing Object Name</returns>
        public String getObjectName()
        {
            return objectName;
        }

        /// <summary>
        /// Returns Value Range
        /// </summary>
        /// <returns>String containing Value Range</returns>
        public String getValueRange()
        {
            return valueRange;
        }

        /// <summary>
        /// Returns parentURI  
        /// </summary>
        /// <returns>String containing parentURI</returns>
        public String getParentURI()
        {
            return parentURI;
        }

        /// <summary>
        /// Returns Value of the Object
        /// </summary>
        /// <returns>String containing Value of the Object</returns>
        public String getValue()
        {
            GetHTTPRequest request = new GetHTTPRequest(this.client);
            request.createHTTPRequest(path, Resource1.ObjectRequest);
            ExecutionResult executionResult = request.executeRequest();
            String value = JSONParser.getFieldValue("value", executionResult.responseFromServer);
            byte[] objectValue = Convert.FromBase64String(value);
            value = System.Text.Encoding.UTF8.GetString(objectValue);
            return value;
        }

        /// <summary>
        /// Return Metadata of current object
        /// </summary>
        /// <returns>Object of Metadata</returns>
        public Metadata getMetadata()
        {
            return metadata;
        }

    }
}
