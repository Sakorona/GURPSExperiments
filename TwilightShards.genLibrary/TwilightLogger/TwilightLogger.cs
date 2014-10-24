using System;
using System.Collections.Generic;
using System.IO;

namespace TwilightShards.genLibrary {
    /// <summary>
    /// This is a logger class.
    /// </summary>
    public class TwilightLogger
    {
        /// <summary>
        /// This is the flags for what output we are using.
        /// </summary>
        protected TwilightOutputs OutputFlags { get; set; }

        /// <summary>
        /// This is the internal storage for events. 
        /// </summary>
        protected List<string> LoggedEvents { get; set; }
        
        /// <summary>
        /// This is the file path for output, if used.
        /// </summary>
        protected StreamWriter FileOutput { get; set; }

        /// <summary>
        /// This tracks the last written event from the logger.
        /// </summary>
        protected int CurrentLastWrittenEvent { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public TwilightLogger() 
        {
            LoggedEvents = new List<string>();
            OutputFlags = TwilightOutputs.None;
            CurrentLastWrittenEvent = 0;
        }

        /// <summary>
        /// Constructor with a file path.
        /// </summary>
        /// <param name="FilePath">File Path to the logged file</param>
        public TwilightLogger(string FilePath)
        {
            LoggedEvents = new List<string>();
            OutputFlags = TwilightOutputs.None;
            CurrentLastWrittenEvent = 0;
            SetFilePath(FilePath);
        }

        /// <summary>
        /// Destructor. Closes file
        /// </summary>
        ~TwilightLogger()
        {
            if (FileOutput != null)
                FileOutput.Close();
        }

        //******************************* Base Logger Functionality
        /// <summary>
        /// This adds a log event
        /// </summary>
        /// <param name="lEvent">The log event</param>
        public void AddLogEvent(string lEvent)
        {
            LoggedEvents.Add(lEvent);
        }

        /// <summary>
        /// This function sets the file output
        /// </summary>
        /// <param name="filePath"></param>
        public void SetFilePath(string filePath)
        {
            FileOutput = File.AppendText(filePath);
        }

        /// <summary>
        /// This resets the logger.
        /// </summary>
        public void ResetLogger()
        {
            OutputFlags = TwilightOutputs.None;
            LoggedEvents.Clear();
        }
       
        //******************************* Output Flag management
        /// <summary>
        /// This sets if we allow console output
        /// </summary>
        /// <param name="isAllowed">Whether if console output is allowed or not.</param>
        public void SetConsoleOutput(bool isAllowed) 
        {
            if (isAllowed)
                OutputFlags = OutputFlags | TwilightOutputs.Console;
            else
                OutputFlags = OutputFlags & ~TwilightOutputs.Console;
        }
        
        /// <summary>
        /// This sets if we allow file output
        /// </summary>
        /// <param name="isAllowed">Whether if file output is allowed or not.</param>
        public void SetFileOutput(bool isAllowed)
        {
            if (isAllowed)
                OutputFlags = OutputFlags | TwilightOutputs.File;
            else
                OutputFlags = OutputFlags & ~TwilightOutputs.File;
        }

        /// <summary>
        /// This sets if we allow textbox output
        /// </summary>
        /// <param name="isAllowed">Whether if textbox output is allowed or not.</param>
        public void SetTextBoxOutput(bool isAllowed)
        {
            if (isAllowed)
                OutputFlags = OutputFlags | TwilightOutputs.TextBox;
            else
                OutputFlags = OutputFlags & ~TwilightOutputs.TextBox;
        }

        /// <summary>
        /// This sets if we allow database output
        /// </summary>
        /// <param name="isAllowed">Whether if database output is allowed or not.</param>
        public void SetDatabaseOutput(bool isAllowed)
        {
            if (isAllowed)
                OutputFlags = OutputFlags | TwilightOutputs.Database;
            else
                OutputFlags = OutputFlags & ~TwilightOutputs.Database;
        }

        /// <summary>
        /// This sets if we allow internet output
        /// </summary>
        /// <param name="isAllowed">Whether if internet output is allowed or not.</param>
        public void SetInternetOutput(bool isAllowed)
        {
            if (isAllowed)
                OutputFlags = OutputFlags | TwilightOutputs.InternetStream;
            else
                OutputFlags = OutputFlags & ~TwilightOutputs.InternetStream;
        }

        /// <summary>
        /// This clears all output flags.
        /// </summary>
        public void ClearAllOutputFlags()
        {
            OutputFlags = TwilightOutputs.None;
        }

        /// <summary>
        /// This function checks if console output has been set.
        /// </summary>
        /// <returns>True if console output is allowed</returns>
        public bool GetConsoleOutputAllowed()
        {
            if ((OutputFlags & TwilightOutputs.Console) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This function checks if text box output has been set.
        /// </summary>
        /// <returns>True if text box output is allowed</returns>
        public bool GetTextBaseOutputAllowed()
        {
            if ((OutputFlags & TwilightOutputs.TextBox) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This function checks if file output has been set.
        /// </summary>
        /// <returns>True if file output is allowed</returns>
        public bool GetFileOutputAllowed()
        {
            if ((OutputFlags & TwilightOutputs.File) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This function checks if database output has been set.
        /// </summary>
        /// <returns>True if database output is allowed</returns>
        public bool GetDatabaseOutputAllowed()
        {
            if ((OutputFlags & TwilightOutputs.Database) != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This function checks if internet output has been set.
        /// </summary>
        /// <returns>True if internet output is allowed</returns>
        public bool GetInternetOutputAllowed()
        {
            if ((OutputFlags & TwilightOutputs.InternetStream) != 0)
                return true;
            else
                return false;
        }

        //******************************* Output Functionality
        /// <summary>
        /// This writes the full logger out.
        /// </summary>
        public void WriteLogger()
        {
            WriteOutput();
        }

        /// <summary>
        /// This writes any new events from the last write out
        /// </summary>
        public void WriteNewEvent()
        {
            //check to make sure something new has been logged.
            if (CurrentLastWrittenEvent == LoggedEvents.Count)
                return;

            WriteOutput(CurrentLastWrittenEvent);
        }

        /// <summary>
        /// This is the internal output writer.
        /// </summary>
        /// <param name="start">The logEvent you want to start with.</param>
        protected void WriteOutput(int start = 0)
        {
            for (int i = start; i < LoggedEvents.Count; i++)
            {
                if (GetConsoleOutputAllowed())
                    Console.WriteLine(LoggedEvents[i]);
                if (GetFileOutputAllowed() && FileOutput != null)
                    FileOutput.Write(LoggedEvents[i]);
            }
            CurrentLastWrittenEvent = LoggedEvents.Count;
        }
    }
}
