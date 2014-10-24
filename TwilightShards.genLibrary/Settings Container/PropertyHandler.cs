//CONTRIBUTED CODE. See CONTRIBUTORS for details.
using System;
using System.Collections.Generic;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This class is meant to be a generic container for properties. 
    /// </summary>
    /// <remarks>Abstract because you will need to have your own Enum for Property names.</remarks>
    abstract public class PropertyHandler
    {
        /// <summary>
        /// Internal dictionary tracking properties and values
        /// </summary>
        /// <remarks>The type is specified as an Enum, which contains the name of all of the settings</remarks>
        protected Dictionary<System.Enum, object> Properties = new Dictionary<System.Enum, object>();

        /// <summary>
        /// The path to the file where settings are stored.
        /// </summary>
        protected string FilePath;

        /// <summary>
        /// This tracks if the file is up to date
        /// </summary>
        public bool isSettingsModified = true;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PropertyHandler()
        {
        }

        /// <summary>
        /// This constructor takes the filepath and adds 
        /// </summary>
        /// <param name="filePath"></param>
        public PropertyHandler(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// This sets the file path settings are save and read from
        /// </summary>
        /// <param name="filePath">The file path</param>
        public void SaveFilePath(string filePath)
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// This function gets the value from the dictionary and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the value we want</typeparam>
        /// <param name="PropertyName">The property we are retriveing</param>
        /// <returns>The value</returns>
        public T Get<T>(System.Enum PropertyName)
        {
            if (Properties.Count == 0)
                throw new InvalidOperationException("The settings container has not been initiated!");

            if (!Properties.ContainsKey(PropertyName))
                throw new ArgumentException("Invalid property: " + PropertyName);                
            
            if (Properties[PropertyName].GetType() != typeof(T))
                throw new ArgumentException("Invalid type. Type requested is " + typeof(T) + " , type gotten is " + Properties[PropertyName].GetType());
      
            return (T)Properties[PropertyName];
        }

        /// <summary>
        /// This function sets the value
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        public void Set(System.Enum PropertyName, object PropertyValue)
        {
            Properties[PropertyName] = PropertyValue;
            this.isSettingsModified = true;
        }

    }

}