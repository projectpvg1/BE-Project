using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Defines metadata
    /// </summary>
    public class Metadata
    {
        public Dictionary<String, String> userMetadata = new Dictionary<String, String>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metadata">JSON String for metadata in response</param>
        internal Metadata(String metadata)
        //constructor
        {
            userMetadata = JSONParser.parseJSONString(metadata);
        }


        /// <summary>
        /// Get Creation Time
        /// </summary>
        /// <returns>Creation Time</returns>
        internal String getCTime()
        {
            foreach (KeyValuePair<String, String> entry in userMetadata)
            {
                if (entry.Key.Equals("cTime"))
                    return entry.Value;
            }
            return null;
        }


        /// <summary>
        /// Get CDMI Size
        /// </summary>
        /// <returns>CDMI Size</returns>
        internal int getSize()
        {
            foreach (KeyValuePair<String, String> entry in userMetadata)
            {
                if (entry.Key.Equals("cdmi_size"))
                    return Convert.ToInt32(entry.Value);
            }
            return 0;
        }

        /// <summary>
        /// displays all key:value pairs in metadata.
        /// </summary>
        public void toString()
        {
            foreach (KeyValuePair<String, String> entry in userMetadata)
                Console.WriteLine(entry.Key + " :- " + entry.Value);
        }
    }

}
