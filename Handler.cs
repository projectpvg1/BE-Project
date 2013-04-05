using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Handles basic CRUD operations
    /// </summary>
    public class Handler
    {
        internal Client client;
        internal String path;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Instance of Client</param>
        public Handler(Client client)
        {
            this.client = client;
        }

        /// <summary>
        /// Creates a new container in account
        /// </summary>
        /// <param name="containerName">Name of Container to be created</param>
        /// <returns>True if created successfully
        /// False otherwise</returns>
        public Boolean createContainer(String containerName)
        // 
        {
            try
            {
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                path = CommonConstants.CDMI_PATH_CONSTANT +
                    client.username + "/" + containerName;
                byte[] buf = Encoding.UTF8.GetBytes(
                    "{ \"metadata\" : {\"cTime\" : \"" +
                    DateTime.Now.ToString() + "\"} }");
                request.createHTTPRequest(path, Resource1.ContainerRequest);
                request.addBody(buf);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new container in account
        /// </summary>
        /// <param name="containerName">Name of Container to be created</param>
        /// <param name="metadataFilePath">Path of metadata file</param>
        /// <returns>True if created successfully
        /// False otherwise</returns>
        public Boolean createContainer(String containerName,String metadataFilePath)
        // 
        {
            try
            {
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                path = CommonConstants.CDMI_PATH_CONSTANT +
                    client.username + "/" + containerName;

                String[] metadataFile = File.ReadAllLines(metadataFilePath);
                StringBuilder metadataContents = new StringBuilder();
                foreach (String entry in metadataFile)
                {
                    metadataContents.Append(entry);
                }

                byte[] buf = Encoding.UTF8.GetBytes(
                    "{ \"metadata\" : {\"cTime\" : \"" +
                    DateTime.Now.ToString() + "\"" + ","+metadataContents.ToString()+"} }");
                request.createHTTPRequest(path, Resource1.ContainerRequest);
                request.addBody(buf);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }


        /// <summary>
        /// Display contents of the account
        /// </summary>
        public List<String> displayAccountContents()
        {
            GetHTTPRequest request = new GetHTTPRequest(this.client);
            request.createHTTPRequest(Resource1.AccountRequest);
            ExecutionResult executionResult = request.executeRequest();
            List<String> contents = JSONParser.getChildren(JSONParser.getFieldValue("children",executionResult.responseFromServer));
            return contents;
        }

        /// <summary>
        /// Creates a new object in specified container
        /// </summary>
        /// <param name="containerName">Name of container in which to create object</param>
        /// <param name="objectName">Name of object to be created</param>
        /// <param name="filePath">Path of file to be uplaoded</param>
        /// <returns>True if created successfully
        /// False otherwise</returns>
        public Boolean createObject(String containerName, String objectName, String filePath)
        {
            try
            {
                path = CommonConstants.CDMI_PATH_CONSTANT + client.username +
                    "/" + containerName + "/" + objectName;
                String objectValue = File.ReadAllText(filePath);
                byte[] value = Encoding.UTF8.GetBytes(objectValue);
                objectValue = Convert.ToBase64String(value);
                byte[] buf = Encoding.UTF8.GetBytes(
                    "{ \"mimetype\" : \"text/plain\"," + "\"value\" : \"" +
                    objectValue + "\" ,\"metadata\" : { \"cTime\" : \"" +
                    DateTime.Now.ToString() + "\" } }");
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                request.createHTTPRequest(path, Resource1.ObjectRequest);
                request.addBody(buf);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }

        /// <summary>
        /// Create a new object with user defined metadata attached
        /// </summary>
        /// <param name="containerName">Name of container in which to create object</param>
        /// <param name="objectName">Name of object to be created</param>
        /// <param name="filePath">Path of file to be uploaded</param>
        /// <param name="metadataFilePath">Path of file containing user defined metadata</param>
        /// <returns>True if created successfully
        /// False otherwise</returns>
        public Boolean createObject(String containerName, String objectName,
            String filePath, String metadataFilePath)
        {
            try
            {
                path = CommonConstants.CDMI_PATH_CONSTANT + client.username +
                    "/" + containerName + "/" + objectName;
                String objectValue = File.ReadAllText(filePath);
                byte[] value = Encoding.UTF8.GetBytes(objectValue);
                objectValue = Convert.ToBase64String(value);

                String[] metadataFile = File.ReadAllLines(metadataFilePath);
                StringBuilder metadataContents = new StringBuilder();
                foreach (String entry in metadataFile)
                {
                    metadataContents.Append(entry);
                }

                byte[] buf = Encoding.UTF8.GetBytes(
                    "{ \"mimetype\" : \"text/plain\"," + "\"value\" : \"" +
                    objectValue + "\" ,\"metadata\" : { \"cTime\" : \"" +
                    DateTime.Now.ToString() + "\"" + "," + metadataContents.ToString() + " } }");
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                request.createHTTPRequest(path, Resource1.ObjectRequest);
                request.addBody(buf);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete an existing container
        /// </summary>
        /// <param name="containerName">Name of container to be deleted</param>
        /// <returns>True if deleted successfully
        /// False otherwise</returns>
        public Boolean deleteContainer(String containerName)
        {
            try
            {
                path = CommonConstants.CDMI_PATH_CONSTANT + client.username
                    + "/" + containerName;
                Container container = new Container(client,path);
                if(container.isGetSuccessful())
                    if(!container.getChildrenRange().Equals("0-0"))
                        container.emptyContainer();
                DelHTTPRequest request = new DelHTTPRequest(this.client);
                request.createHTTPRequest(path, Resource1.ContainerRequest);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }


        /// <summary>
        /// Delete an existing object from specified container
        /// </summary>
        /// <param name="containerName">Name of Container from which to delete an object</param>
        /// <param name="objectName">Name of object to be deleted</param>
        /// <returns>True if deleted successfully
        /// False otherwise</returns>
        public Boolean deleteObject(String containerName, String objectName)
        {
            try
            {
                path = CommonConstants.CDMI_PATH_CONSTANT + client.username +
                    "/" + containerName + "/" + objectName;
                DelHTTPRequest request = new DelHTTPRequest(this.client);
                request.createHTTPRequest(path, Resource1.ObjectRequest);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }


        /// <summary>
        /// Gets the specified container
        /// </summary>
        /// <param name="containerName">Name of container</param>
        /// <returns>Container</returns>
        public Container getContainer(String containerName)
        {

            path = CommonConstants.CDMI_PATH_CONSTANT + client.username +
                "/" + containerName;
            return new Container(client, path);

        }

        /// <summary>
        /// Get any object of specified container
        /// </summary>
        /// <param name="containerName">Name of container</param>
        /// <param name="objectName">Name of child object</param>
        /// <returns>Child Object</returns>
        public Object getObject(String containerName,String objectName)
        {
            path = CommonConstants.CDMI_PATH_CONSTANT + client.username +
                "/" + containerName + "/" +objectName;
            Object childObject = new Object(client ,path);
            return childObject;
        }

    }
}
