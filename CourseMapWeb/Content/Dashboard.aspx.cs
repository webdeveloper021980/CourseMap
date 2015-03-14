using System;
using CourseMapWeb.Common;

namespace CourseMapWeb.Content
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(UiHelpers.StudentData ==null) Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                lblUser.Text = UiHelpers.StudentData.FullName;
            }
        }
    }
}