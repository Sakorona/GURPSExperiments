using System;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This specifies which type the Property is for.
    /// </summary>
    public enum ValType
    {
        /// <summary>
        /// This is a Short (int16)
        /// </summary>
        ShortType,

        /// <summary>
        /// This is an Integer (int)
        /// </summary>
        IntegerType,

        /// <summary>
        /// This is an Long (long)
        /// </summary>
        LongType,

        /// <summary>
        /// This is a Double (double)
        /// </summary>
        DoubleType,

        /// <summary>
        /// This is a String (string)
        /// </summary>
        StringType,

        /// <summary>
        /// This is a Character (char)
        /// </summary>
        CharType,

        /// <summary>
        /// This is a Boolean (bool)
        /// </summary>
        BoolType,

        /// <summary>
        /// This is a Unsigned short (uint16)
        /// </summary>
        UShortType,
        
        /// <summary>
        /// This is a Unsigned integer (uint)
        /// </summary>
        UIntegerType,

        /// <summary>
        /// This is a Unsigned Long (ulong)
        /// </summary>
        ULongType,

        /// <summary>
        /// This is an Enum (enum)
        /// </summary>
        EnumType,

        /// <summary>
        /// This is a generic object. Used as a catch all.
        /// </summary>
        ObjectType
    }
}
