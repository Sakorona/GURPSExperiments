using System;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This attribute is used to define properties. Used primarily for the setting
    /// container.
    /// </summary>
    public class PropDefs : Attribute
    {
        /// <summary>
        /// This is the attribute type of the property
        /// </summary>
        ValType attrType;

        /// <summary>
        /// The minimum value of the property
        /// </summary>
        object minVal;
        
        /// <summary>
        /// The maximum value of the property
        /// </summary>
        object maxVal;

        /// <summary>
        /// The default value of the property
        /// </summary>
        object defVal;

        /// <summary>
        /// The minimum length of the property, if it's a string.
        /// </summary>
        int minLength;

        /// <summary>
        /// This function returns the default value of the property.
        /// </summary>
        public object DefaultValue
        {
            get { return defVal; }
        }

        /// <summary>
        /// This function returns the minimum value of the property.
        /// </summary>
        public object MinValue
        {
            get { return minVal; }
        }

        /// <summary>
        /// This function returns the maximum value of the property.
        /// </summary>
        public object MaxValue
        {
            get { return maxVal; }
        }

        /// <summary>
        /// This function returns the type of the property.
        /// </summary>
        public ValType AttributeType
        {
            get { return attrType; }
        }
        
        /// <summary>
        /// This function returns the minimum string length of the property.
        /// </summary>
        public int MinStringLength
        {
            get { return minLength; }
        }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="v">Type of the property</param>
        /// <param name="min">Minimum value of the property</param>
        /// <param name="max">Maximum value of the property</param>
        /// <param name="def">Default value of the property</param>
        /// <param name="length">Minimum value of the string property</param>
        public PropDefs(ValType v, object min, object max, object def, int length = 0)
        {
            this.attrType = v;
            this.minVal = min;
            this.maxVal = max;
            this.defVal = def;
            this.minLength = length;
        }
    }
}