using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ObjectStoreSDK
{ /// <summary>
    /// This class is used to perform CRUD operations 
    /// on container and it's children
    /// </summary>
    public class Container
    {
        internal Client client;
        internal String path;
        String objectName;
        String capabilitiesURI;
        String parentURI;
        List<String> children = new List<string>();
        Metadata metadata;
        String objectType;
        String childrenRange;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">current instance of Client</param>
        /// <param name="path">Path to container</param>
        public Container(Client client, String path)
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
        /// Check if the GetContainer operation is successful or not
        /// </summary>
        /// <returns>
        /// True if successful
        /// False otherwise
        /// </returns>
        public bool isGetSuccessful()
        {
            if (objectType.Equals(CommonConstants.CDMI_CONTAINER_CONSTANT))
            {
                return true; ;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates child container under given parent container.
        /// </summary>
        /// <param name="childName">Name of Child container</param>
        ///  <return>True if created successfully
        /// False otherwise</return>
        public Boolean createChildContainer(String childName)
        {
            try
            {
                byte[] buf = Encoding.UTF8.GetBytes(
                    "{ \"metadata\" : { \"cTime\" : \"" + DateTime.Now.ToString() +
                    "\" } }");
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                request.createHTTPRequest(path + "/" + childName,
                    Resource1.ContainerRequest);
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
        /// Creates child container under given parent container.
        /// </summary>
        /// <param name="childName">Name of Child container</param>
        /// <param name="metadataFilePath">Path of metadata file</param>
        ///  <return>True if created successfully
        /// False otherwise</return>
        public Boolean createChildContainer(String childName,String metadataFilePath)
        {
            try
            {
                String[] metadataFile = File.ReadAllLines(metadataFilePath);
                StringBuilder metadataContents = new StringBuilder();
                foreach (String entry in metadataFile)
                {
                    metadataContents.Append(entry);
                }

                byte[] buf = Encoding.UTF8.GetBytes(
                    "{ \"metadata\" : { \"cTime\" : \"" + DateTime.Now.ToString() +
                    "\"" +","+ metadataContents.ToString()+" } }");
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                request.createHTTPRequest(path + "/" + childName,
                    Resource1.ContainerRequest);
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
        /// Creates child object under given parent container.
        /// </summary>
        /// <param name="childName">Name of Child Object</param>
        /// <param name="filePath">Path of file to be uploaded</param>
        ///  <return>True if created successfully
        /// False otherwise</return>
        public Boolean createChildObject(String childName, String filePath)
        {
            try
            {
                String objectValue = File.ReadAllText(filePath);
                byte[] value = Encoding.UTF8.GetBytes(objectValue);
                objectValue = Convert.ToBase64String(value);
                byte[] buf = Encoding.UTF8.GetBytes(
                    "{ \"mimetype\" : \"text/plain\"," + "\"value\" : \"" +
                    objectValue + "\" ,\"metadata\" : { \"cTime\" : \"" +
                    DateTime.Now.ToString() + "\" } }");
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                request.createHTTPRequest(path + "/" + childName,
                    Resource1.ObjectRequest);
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
        /// Creates child object under given parent container with medatadata file.
        /// </summary>
        /// <param name="childName">Name of child object</param>
        /// <param name="filePath">Path of file to be uploaded</param>
        /// <param name="metadataFilePath">Path of metadata file</param>
        /// <returns>True if created successfully 
        /// False otherwise</returns>
        public Boolean createChildObject(String childName, String filePath, String metadataFilePath)
        {

            try
            {
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

                String trial = System.Text.Encoding.UTF8.GetString(buf);
                PutHTTPRequest request = new PutHTTPRequest(this.client);
                request.createHTTPRequest(path + "/" + childName,
                    Resource1.ObjectRequest);
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
        /// Deletes child container under given parent container.
        /// </summary>
        /// <param name="childName">Name of child container to be deleted</param>
        /// <returns>True if deleted successfully
        /// False otherwise</returns>
        public Boolean deleteChildContainer(String childName)
        {
            try
            {
                Container container = getChildContainer(childName);
                if(container.isGetSuccessful())
                    if (!container.getChildrenRange().Equals("0-0"))
                    container.emptyContainer();
                DelHTTPRequest request = new DelHTTPRequest(this.client);
                request.createHTTPRequest(path + "/" + childName,
                    Resource1.ContainerRequest);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes child object under given parent container
        /// </summary>
        /// <param name="childName">Name of child object to be deleted</param>
        /// <returns>True if deleted successfully
        /// False otherwise</returns>
        public Boolean deleteChildObject(String childName)
        {
            try
            {
                DelHTTPRequest request = new DelHTTPRequest(this.client);
                request.createHTTPRequest(path + "/" +
                    childName, Resource1.ObjectRequest);
                ExecutionResult executionResult = request.executeRequest();
                return true;
            }
            catch (ExceptionHandler)
            {
                return false;
            }
        }

        /// <summary>
        /// Get any child container of current container
        /// </summary>
        /// <param name="childName">Name of child container</param>
        /// <returns>Child Container</returns>
        public Container getChildContainer(String childName)
        {
            Container container = new Container(client, path + "/" + childName);
            return container;
        }


        /// <summary>
        /// Get parent container of current container
        /// </summary>
        /// <returns>Parent Container</returns>
        public Container getParentContainer()
        {
            Container container = new Container(client, parentURI);
            return container;
        }

        /// <summary>
        /// Retrieve the details of container from JSON repsonse
        /// </summary>
        private void toString()
        {
            try
            {
                GetHTTPRequest request = new GetHTTPRequest(this.client);
                request.createHTTPRequest(path, Resource1.ContainerRequest);
                ExecutionResult executionResult = request.executeRequest();
                childrenRange = JSONParser.getFieldValue("childrenRange",
                    executionResult.responseFromServer);
                objectName = JSONParser.getFieldValue("objectName",
                    executionResult.responseFromServer);
                capabilitiesURI = JSONParser.getFieldValue("capabilitiesURI",
                    executionResult.responseFromServer);
                parentURI = JSONParser.getFieldValue("parentURI",
                    executionResult.responseFromServer);
                parentURI = parentURI.Remove(parentURI.Length - 1);
                metadata = new Metadata(JSONParser.getFieldValue("metadata",
                    executionResult.responseFromServer));
                objectType = JSONParser.getFieldValue("objectType",
                    executionResult.responseFromServer);
                children = JSONParser.getChildren(JSONParser.getFieldValue("children",
                    executionResult.responseFromServer));

            }
            catch (ExceptionHandler)
            { }
        }

        /// <summary>
        /// Get any child object of current container
        /// </summary>
        /// <param name="objectName">Name of child object</param>
        /// <returns>Child Object</returns>
        public Object getChildObject(String objectName)
        {
            Object childObject = new Object(client, path + "/" + objectName);
            return childObject;
        }

        /// <summary>
        /// delete all the contents of the container
        /// </summary>
        public void emptyContainer()
        {
            foreach (String s in children)
            {
                if (!s.Equals(null))
                {
                    
                    if (!this.deleteChildObject(s))
                    {
                        Container container = getChildContainer(s);
                        if (!container.getChildrenRange().Equals("0-0"))
                        {
                            container.emptyContainer();
                            this.deleteChildContainer(s);
                        }
                    }
                       
                }
            }
        }

        /// <summary>
        /// Returns Container name
        /// </summary>
        /// <returns>Container name as string</returns>
        public String getContainerName()
        {
            return objectName;
        }

        /// <summary>
        /// Return children range
        /// </summary>
        /// <returns>String containing children range</returns>
        public String getChildrenRange()
        {
            return childrenRange;
        }

        /// <summary>
        /// Returns children
        /// </summary>
        /// <returns>ist of type string containing all children</returns>
        public List<String> getChildren()
        {
            return children;
        }

        /// <summary>
        /// Return parent URI
        /// </summary>
        /// <returns>String containing parent URI</returns>
        public String getParentURI()
        {
            return parentURI;
        }

        /// <summary>
        /// Returns container path
        /// </summary>
        /// <returns>String containing path of container</returns>
        public String getContainerPath()
        {
            return path;
        }

        /// <summary>
        /// Return Metadata of current container
        /// </summary>
        /// <returns>Object of Metadata</returns>
        public Metadata getMetadata()
        {
            return metadata;
        }


    } 
}

