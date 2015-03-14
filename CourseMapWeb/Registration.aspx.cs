using System;
using System.Linq;
using System.Web.UI.WebControls;
using CourseMapWeb.Common;
using CourseMapWeb.DataModel;

namespace CourseMapWeb
{
    public partial class Registration : System.Web.UI.Page
    {
        private readonly CourseMapDataModel _uowCourseMap = new CourseMapDataModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStateInformation();
            }
        }

        private void LoadStateInformation()
        {
            var stateData = _uowCourseMap.States.OrderBy(s => s.StateName).ToList();
            ddlState.Items.Clear();
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateName";
            ddlState.AppendDataBoundItems = true;
            ddlState.DataSource = stateData;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem(UiHelpers.SelectOne, string.Empty));
            LoadSchoolOrCollageInformation(rbSchool.Text);


        }
        private void LoadSchoolOrCollageInformation(string schoolCollageType)
        {
            var schoolCollageData = _uowCourseMap.SchoolCollageInformations.Where(s => s.TypeSc == schoolCollageType).OrderBy(s => s.SchoolOrCollageName).ToList();
            ddlSchoolCollage.Items.Clear();
            ddlSchoolCollage.DataTextField = "SchoolOrCollageName";
            ddlSchoolCollage.DataValueField = "Id";
            ddlSchoolCollage.AppendDataBoundItems = true;
            ddlSchoolCollage.DataSource = schoolCollageData;
            ddlSchoolCollage.DataBind();
            ddlSchoolCollage.Items.Insert(0, new ListItem(UiHelpers.SelectOne, string.Empty));
            ResetComboBox(ref ddlMajor);
            ResetComboBox(ref ddlDepartment);
        }

        private void ResetComboBox(ref DropDownList comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Insert(0, new ListItem(UiHelpers.SelectOne, string.Empty));
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            using (var uow = new CourseMapDataModel())
            {
                if (uow.StudentBasicInformations.Any(st => st.StudentId.Equals(txtStudentId.Text, StringComparison.InvariantCultureIgnoreCase)))
                {
                    lblMessage.Text = "Same Student ID already registerd";
                    return;
                }
                var newRegistration = new StudentBasicInformation
                {
                    StudentId = txtStudentId.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    FullName = txtFirstName.Text + " " + txtLastName.Text,
                    Email = txtEmail.Text,
                    Password = UiHelpers.HashPassword(txtPassword.Text),
                    SchoolOrCollage = rbSchool.Checked ? rbSchool.Text : rbCollage.Text,
                    SchoolOrCollageId = Int64.Parse(ddlSchoolCollage.SelectedValue),
                    DepartmentId = Int64.Parse(ddlDepartment.SelectedValue),
                    MajorId = Int64.Parse(ddlMajor.SelectedValue),
                    Dob = DateTime.Parse(txtDOB.Text),
                    Address1 = txtAddress1.Text,
                    Address2 = txtAddress2.Text,
                    State = ddlState.SelectedValue,
                    City = txtCity.Text,
                    ZipCode = txtZipCode.Text
                };
                uow.StudentBasicInformations.Add(newRegistration);
                uow.SaveChanges();
                lblMessage.Text = "Registered successfully, please login";
            }
        }

        protected void LoadSchoolCollage(object sender, EventArgs e)
        {
            ddlSchoolCollage.Items.Clear();
            LoadSchoolOrCollageInformation(((RadioButton)(sender)).Text);

        }

        protected void SchoolCollageSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetComboBox(ref ddlMajor);
            if (!string.IsNullOrEmpty(ddlSchoolCollage.SelectedValue))
            {
                var schoolCollageId = Int64.Parse(ddlSchoolCollage.SelectedValue);
                var departmentData = _uowCourseMap.DepartmentsInformations.Where(d => d.SchoolOrCollageId == schoolCollageId).OrderBy(d => d.DepartmentName).ToList();
                ddlDepartment.Items.Clear();
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.AppendDataBoundItems = true;
                ddlDepartment.DataSource = departmentData;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem(UiHelpers.SelectOne, string.Empty));

            }
            else
                ResetComboBox(ref ddlDepartment);

          
        }

        protected void DepartmentSelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue))
            {
                var departmentId = Int64.Parse(ddlDepartment.SelectedValue);
                var majorData = _uowCourseMap.MajorInformations.Where(d => d.DepartmentId == departmentId).OrderBy(d => d.Major).ToList();
                ddlMajor.Items.Clear();
                ddlMajor.DataTextField = "Major";
                ddlMajor.DataValueField = "Id";
                ddlMajor.AppendDataBoundItems = true;
                ddlMajor.DataSource = majorData;
                ddlMajor.DataBind();
                ddlMajor.Items.Insert(0, new ListItem(UiHelpers.SelectOne, string.Empty));

            }
            else
                ResetComboBox(ref ddlMajor);

        }
    }

}