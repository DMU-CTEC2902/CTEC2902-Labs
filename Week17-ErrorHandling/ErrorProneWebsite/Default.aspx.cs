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
            string appDataDirectory = Server.MapPath("/App_Data");

            string contentFilePath = string.Format(@"{0}\{1}", appDataDirectory, "Content.txt");

            FileManager contentManager = new FileManager(contentFilePath);

            lblContent.Text = contentManager.GetContent();
        }
    }
}