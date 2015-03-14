using System;
using System.Linq;
using CourseMapWeb.Common;
using CourseMapWeb.DataModel;

namespace CourseMapWeb
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                //txtStudentId.Text = "afroz";
                //txtPassword.Text = "a";
                //DoLogin();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void DoLogin()
        {
            using (var uow = new CourseMapDataModel())
            {
                var password = UiHelpers.HashPassword(txtPassword.Text);
                var studentDataTemp =
                    uow.StudentBasicInformations.FirstOrDefault(
                        st => st.StudentId.Equals(txtStudentId.Text, StringComparison.InvariantCultureIgnoreCase) &&
                              st.Password == password);
                if (studentDataTemp != null)
                {
                    UiHelpers.StudentData = studentDataTemp;
                    Response.Redirect("Content/Dashboard.aspx");
                }

                else lblMessage.Text = "User Name/password is incorrect";
            }
        }
    }
}