using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Configuration;
using System.Web.Security;

namespace WebApplication1
{
    public partial class Pitc_Testing_Tool_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            int kont = 0;
            using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand("SELECT * FROM users1 where username = '" + user.Text + "' and password = '" + passwordbox.Text + "';", connection))
                using (OdbcDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        kont++;
                    }
                    dr.Close();
                }
                connection.Close();
            }
            string username = user.Text;
            string password = passwordbox.Text;
            bool rememberMe = RememberMeCheckBox.Checked;

            if (rememberMe)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddDays(30), true, String.Empty);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                cookie.Expires = ticket.Expiration;
                Response.Cookies.Add(cookie);
            }
            else
            {
                FormsAuthentication.SignOut();
             
            }


            if (kont>0)
            {
                Session["BoolV"] = true;
                Response.Redirect("Pitc_Testing_Tool_main.aspx");
            }
            else 
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowPopup('Invalid username or password!');", true);

            }
        }
    }
}