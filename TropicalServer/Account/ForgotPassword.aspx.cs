using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TropicalServer.BLL;

namespace TropicalServer.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        readonly ReportsBLL BLL = new ReportsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            if (submitButton.Text == "Get Reset Link")
            {
                bool check = BLL.ValidateEmail(email.Text);
                if (check)
                {
                    passwordConfirm.Visible = true;
                    emailConfirm.Visible = false;
                }
                else
                {
                    errorlbl.Text = "Email Does Not Exist";
                    errorlbl.Visible = true;
                }
            }
             else
            {

            }
        }
    }
}