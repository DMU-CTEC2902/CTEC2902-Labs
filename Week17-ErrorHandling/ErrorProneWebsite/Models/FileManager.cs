using System;
using System.IO;

namespace ErrorProneWebsite.Models
{
    public class FileManager
    {
        private string _contentFilePath;

        public FileManager(string contentFilePath)
        {
            // TODO: Complete member initialization
            this._contentFilePath = contentFilePath;
        }

        /// <summary>
        /// <para>Reads a file from the _contentFilePath and returns the contents.</para>
        /// <para>This version shows a try / catch / finally block</para>
        /// <para>... but it's a bit clunky to say the least, because you have to create a null StreamReader outside of the error-handling code</para>
        /// <para>However, at least it's now handling one of the most obvious exceptions that can happen with a file read is made</para>
        /// <para>All sorts of other things could STILL go wrong, too...</para>
        /// <para>... so the catch all "Exception" handler is used to catch anything totally unexpected.</para>
        /// </summary>
        /// <returns>
        /// <para>On the main flow, the contents of the file from the file path as a string.</para>
        /// <para>In the exception flow - a friendly message coupled with the error message from .Net</para>
        /// </returns>

        public string GetContent()
        {
            string contentMessage = String.Empty;
            
            StreamReader streamReader = null;
            
            try 
            { 
                streamReader = new StreamReader(_contentFilePath);
                contentMessage = streamReader.ReadToEnd();
            }
            catch(FileNotFoundException fnfEx)
            {
                contentMessage = String.Format("{0}{1}{2}", 
                                    "Oops! The content could not be found at the location specified.",
                                    Environment.NewLine,
                                    fnfEx.Message);
            }
            catch(Exception ex)
            {
                contentMessage = String.Format("{0}{1}{2}",
                                    "Blimey! Something totally unexpected just happened!",
                                    Environment.NewLine,
                                    ex.Message);
            }
            finally
            {
                if(streamReader != null) streamReader.Close();
            }

            return contentMessage;
        }
    }
}