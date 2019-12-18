using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TropicalServer.BLL;

namespace TropicalServer.UserControls
{
    public partial class WebUserControl : System.Web.UI.UserControl
    {
        readonly ReportsBLL BLL = new ReportsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Control controlMenu = Page.Master.FindControl("NavigationMenu");
            if (controlMenu != null)
            {
                controlMenu.Visible = false;
            }
            if (IsPostBack)
            {

                if (BLL.GetUserAuthentication(useridtextbox.Text, passwordtextbox.Text))
                {
                    //Response.Redirect("/UI/Products.aspx");
                    //FormsAuthentication.RedirectFromLoginPage(useridtextbox.Text, rememberCheckbox.Checked);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                         useridtextbox.Text,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(60),
                         rememberCheckbox.Checked,
                         "UserData",
                         FormsAuthentication.FormsCookiePath);

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));

                    Response.Redirect(FormsAuthentication.GetRedirectUrl(useridtextbox.Text, rememberCheckbox.Checked));
                }
                else
                {
                    errorlbl.Visible = true;
                }
            }
        }

        protected void forgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ForgotPassword.aspx");
        }
    }
}