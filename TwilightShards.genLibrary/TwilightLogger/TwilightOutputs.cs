using System;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This enum is used for output flags.
    /// </summary>
    [Flags]
    public enum TwilightOutputs
    {
        /// <summary>
        /// This flag means we should output to nothing.
        /// </summary>
        None = 1,

        /// <summary>
        /// This flag means we should output to a file
        /// </summary>
        File = 2,

        /// <summary>
        /// This flag means we should output to a console
        /// </summary>
        Console = 4,

        /// <summary>
        /// This flag means we should output to a textbox
        /// </summary>
        TextBox = 8,

        /// <summary>
        /// This flag means we should output to a database
        /// </summary>
        /// <remarks>Logic of WHICH database is left up to implementation.</remarks>
        Database = 16,
        
        /// <summary>
        /// This flag means we should output to the internet.
        /// </summary>
        InternetStream = 32
    }
}
