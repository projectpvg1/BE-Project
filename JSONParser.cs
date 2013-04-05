using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectStoreSDK
{
    /// <summary>
    /// Utility class containing function related to JSON parsing
    /// </summary>
    public class JSONParser
    {
        /// <summary>
        /// Parses JSON response.
        /// </summary>
        /// <param name="JSONString">String in which JSON reponse is stored</param>
        /// <returns>Dictionary containing key:value pair of JSONString</returns>
        internal static Dictionary<String, String> parseJSONString(String JSONString)
        {
            List<String> store = new List<string>();
            Dictionary<String, String> ParsedJSON = new Dictionary<String, String>();
            Boolean Flag = false;
            Boolean metadataFlag = false;
            int i;
            StringBuilder temp = new StringBuilder();
            JSONString = JSONString.Remove(JSONString.Length - 1);
            JSONString = JSONString.Substring(1);
            foreach (char c in JSONString)
            {
                if (c == '{')
                {
                    metadataFlag = true;
                }
                else if (c == '}')
                {
                    metadataFlag = false;
                }

                if (metadataFlag == false)
                {
                    if (c != ',')
                    {
                        if (c == '[')
                            Flag = true;
                        else if (c == ']')
                            Flag = false;
                        else if (c != '"' & c != ' ' & c != '\n')
                            temp.Append(c);
                    }
                    else
                    {
                        if (Flag == false)
                        {
                            store.Add(temp.ToString());
                            temp.Clear();
                        }
                        else
                            temp.Append(c);
                    }
                }
                else
                {
                    temp.Append(c);
                }
            }

            store.Add(temp.ToString());
            temp.Clear();
            foreach (String s in store)
            {

                i = s.IndexOf(':');
                temp.Append(s.Substring(i + 1));
                ParsedJSON.Add(s.Remove(i), temp.ToString());
                temp.Clear();

            }
            return ParsedJSON;

        }

        /// <summary>
        /// Gives specific field value from JSON String.
        /// </summary>
        /// <param name="fieldName">Field name to be searched within JSONString</param>
        /// <param name="JSONString">String in which JSON reponse is stored</param>
        /// <returns>value of fieldname requested</returns>
        internal static String getFieldValue(String fieldName, String JSONString)
        {
            Dictionary<String, String> ParsedJSON = JSONParser.parseJSONString(JSONString);
            foreach (KeyValuePair<String, String> entry in ParsedJSON)
            {
                //Console.WriteLine(entry.Key + ":" + entry.Value);
                if (entry.Key.Equals(fieldName))
                    return entry.Value;
            }
            return null;

        }

        /// <summary>
        /// Get all the children
        /// </summary>
        /// <param name="allChildren">String containing all children of container</param>
        /// <returns>Children in a list of String</returns>
        internal static List<String> getChildren(string allChildren)
        //Returns all children of container.
        {
            List<String> children = new List<string>();
            StringBuilder temp = new StringBuilder();
            int i = 0;
            foreach (char c in allChildren)
            {
                if (c == ',')
                {
                    children.Add(temp.ToString());
                    temp.Clear();
                    i++;
                }
                else if (c != '/')
                {
                    temp.Append(c);
                }
            }
            children.Add(temp.ToString());
            return children;
        }

        /// <summary>
        /// Get DateTime from string
        /// </summary>
        /// <param name="dateTimeString">String containing DateTime</param>
        /// <returns>Object of DateTime</returns>
        internal static DateTime getDateTime(String dateTimeString)
        {
            DateTime dateTime;
            dateTimeString = dateTimeString.Insert(10, " ");
            dateTime = Convert.ToDateTime(dateTimeString);
            return dateTime;
        }
    }
}
