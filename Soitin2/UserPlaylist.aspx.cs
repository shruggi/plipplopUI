using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Soitin2
{
    public partial class UserPlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (MopidyContext db = new MopidyContext())
            {
                var currentplaylist = db.SupplementalPlaylistView.ToList();

                foreach (var item in currentplaylist)
                {
                    Response.Write("<tr>");
                    Response.Write("<td>" + item.artist + "</td>");
                    Response.Write("<td>" + item.title + "</td>");
                    Response.Write("<td>" + item.Owner + "</td>");
                    Response.Write("<td></td>");
                    Response.Write("</tr>");
                }
            }
        }
    }
}