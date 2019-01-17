using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ErrorProneWebsite.Models;

namespace ErrorProneWebsite
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // The first two lines find and set the name of the directory and file that the content is stored in
            
            string appDataDirectory = Server.MapPath("/App_Data");

            string contentFilePath = string.Format(@"{0}\{1}", appDataDirectory, "Contenfgmkbt.txt");

            // Then a file manager is created to read the content from the file

            FileManager contentManager = new FileManager(contentFilePath);

            // The text property of the label contained in the ASPX file is set with the content returned by the FileManager 

            lblContent.Text = contentManager.GetContent();


            string outcomeOfAddingEvenMoreContent = String.Empty;

            try
            {
                outcomeOfAddingEvenMoreContent = contentManager.GetEvenMoreContent();
            }
            catch (Exception ex)
            {
                outcomeOfAddingEvenMoreContent = ex.Message;
            }
            finally
            {
                lblEvenMoreContent.Text = outcomeOfAddingEvenMoreContent;
            }

        }
    }
}