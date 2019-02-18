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
        /// <para>What sort of things could go wrong? There's quite a few!</para>
        /// <para>In fact, there's already a big, fat error in this code that needs fixing, never mind all the other things that could happen...</para>
        /// <para>So at the very least some error handling needs to happen here...</para>
        /// </summary>
        /// <returns>The contents of the file from the file path as a string.</returns>




        //public string GetContent()
        //{
        //    //Sets Up the Reader
        //    StreamReader streamReader = new StreamReader(_contentFilePath);

        //    //Returns the string from the file
        //    return streamReader.ReadToEnd();

        //}

        ////Method 1 Try Catch Finally
        //public string GetContent()
        //{
        //    string contentMessage = String.Empty;
        //    StreamReader streamReader = null;

        //    try
        //    {
        //        streamReader = new StreamReader(_contentFilePath);
        //        contentMessage = streamReader.ReadToEnd();
        //    }
        //    catch (Exception ex)
        //    {
        //        contentMessage = String.Format("{0}{1}{2}",
        //                  "Blimey! Something totally unexpected just happened!",
        //                  Environment.NewLine,
        //                  ex.Message);
        //    }
        //    finally
        //    {
        //        if (streamReader != null) streamReader.Close();
        //    }

        //    return contentMessage;
        //}

        ////Method 2 Try Catch Finally Custom Exception
        //public string GetContent()
        //{
        //    string contentMessage = String.Empty;


        //    StreamReader streamReader = null;

        //    try
        //    {
        //        streamReader = new StreamReader(_contentFilePath);
        //        contentMessage = streamReader.ReadToEnd();
        //    }
        //    catch (FileNotFoundException fnfEx)
        //    {
        //        contentMessage = String.Format("{0}{1}{2}",
        //                   "Oops! The content could not be found at the location specified.",
        //                     Environment.NewLine,
        //                     fnfEx.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        contentMessage = String.Format("{0}{1}{2}",
        //                  "Blimey! Something totally unexpected just happened!",
        //                  Environment.NewLine,
        //                  ex.Message);
        //    }
        //    finally
        //    {
        //        if (streamReader != null) streamReader.Close();
        //    }

        //    return contentMessage;
        //}


        //Method 3 Try Catch Using
        public string GetContent()
        {
            string contentMessage = String.Empty;

            //Using using so that StreamReader is closed automatically once we are finished using it.
            try
            {
                using (StreamReader streamReader = new StreamReader(_contentFilePath))
                {
                    contentMessage = streamReader.ReadToEnd();
                }
            }
            catch (FileNotFoundException fnfEx)
            {
                contentMessage = String.Format("{0}{1}{2}",
                           "Oops! The content could not be found at the location specified.",
                             Environment.NewLine,
                             fnfEx.Message);
            }
            catch (Exception ex)
            {
                contentMessage = String.Format("{0}{1}{2}",
                          "Blimey! Something totally unexpected just happened!",
                          Environment.NewLine,
                          ex.Message);
            }

            return contentMessage;
        }

        //Method 4 Throw
        public string GetEvenMoreContent()
        {
            string contentMessage = String.Empty;

            if (!File.Exists(_contentFilePath))
            {
                throw new FileNotFoundException("The content file doesn't exist in the location specified!!!!!...");
            }
            using (StreamReader streamReader = new StreamReader(_contentFilePath))
            {
                contentMessage = streamReader.ReadToEnd();
            }
            return contentMessage;
        }


    }

}