using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseMapWeb.Common;
using CourseMapWeb.DataModel;

namespace CourseMapWeb.Content
{
    public partial class CourseMapPopup : Page
    {
       
        private readonly CourseMapDataModel _uowCourseMap = new CourseMapDataModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ContextId"] != null)
                {
                    var contextId = Int64.Parse(Convert.ToString(Request.QueryString["ContextId"]));
                    if (Request.QueryString["Type"] == UiHelpers.Course)
                    {
                        LoadDropdowns(contextId);
                        UiHelpers.SetDropdownSelectedItem(ref ddlElectives, Convert.ToString(contextId));
                    }
                    else if (Request.QueryString["Type"] == UiHelpers.PrerequisiteCourse)
                    {
                        divElective.Visible = false;
                        divGrade.Visible = false;
                    }
                    else
                    {
                        divElective.Visible = false;
                    }

                }

            }
        }

        private void LoadDropdowns(Int64 courseId)
        {
            var electiveData =
                (from el in _uowCourseMap.ElectiveInformations where el.CourseId == courseId orderby el.ElectiveName select el).ToList();

            ddlElectives.Items.Clear();
            ddlElectives.DataTextField = "ElectiveName";
            ddlElectives.DataValueField = "Id";
            ddlElectives.AppendDataBoundItems = true;
            ddlElectives.DataSource = electiveData;
            ddlElectives.DataBind();
            ddlElectives.Items.Insert(0, new ListItem(UiHelpers.SelectOne, string.Empty));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var courseType = Convert.ToString(Request.QueryString["Type"]);
            switch (courseType)
            {
                case UiHelpers.Course:
                    SaveCourse();
                    break;
                case UiHelpers.PrerequisiteCourse:
                    SavePrerequisiteCourse();
                    break;
                case UiHelpers.ElectiveCourse:
                    SaveElectiveCourses();
                    break;
            }
        }

        private void SaveCourse()
        {
            var studentId = UiHelpers.StudentData.Id;
            var contextId = Int64.Parse(Convert.ToString(Request.QueryString["ContextId"]));
            var courseData =
                _uowCourseMap.StudentCourseInformations.FirstOrDefault(
                    s => s.StudentId == studentId && s.CourseId == contextId);
            if (courseData != null)
            {
                courseData.CourseStatus = ddlCompleteIncomplete.SelectedValue;
                courseData.GradeRecieved = ddlGrade.SelectedValue;
                _uowCourseMap.SaveChanges();
            }
           
        }
        private void SavePrerequisiteCourse()
        {
            var studentId = UiHelpers.StudentData.Id;
            var contextId = Int64.Parse(Convert.ToString(Request.QueryString["ContextId"]));
            var courseData =
                _uowCourseMap.StudentPrerequisiteCourses.FirstOrDefault(
                    s => s.StudentId == studentId && s.PrerequisiteCourseId == contextId);
            if (courseData == null)
            {
                var newCourse = new StudentPrerequisiteCours()
                {
                    StudentId = studentId,
                    PrerequisiteCourseId = contextId,
                    PrerequisiteCourseStatus = ddlCompleteIncomplete.SelectedValue
                };
                _uowCourseMap.StudentPrerequisiteCourses.Add(newCourse);
                _uowCourseMap.SaveChanges();

            }
            else
            {
                courseData.PrerequisiteCourseStatus = ddlCompleteIncomplete.SelectedValue;
                _uowCourseMap.SaveChanges();
            }
        }

        private void SaveElectiveCourses()
        {
            var studentId = UiHelpers.StudentData.Id;
            var contextId = Int64.Parse(Convert.ToString(Request.QueryString["ContextId"]));
            var courseData =
                _uowCourseMap.StudentElectiveCourses.FirstOrDefault(
                    s => s.StudentId == studentId && s.ElectiveCourseId == contextId);
            if (courseData == null)
            {
                var newCourse = new StudentElectiveCours()
                {
                    StudentId = studentId,
                    ElectiveCourseId = contextId,
                    ElectiveCourseStatus = ddlCompleteIncomplete.SelectedValue,
                    GradeRecieved = ddlGrade.SelectedValue
                };
                _uowCourseMap.StudentElectiveCourses.Add(newCourse);
                _uowCourseMap.SaveChanges();

            }
            else
            {
                courseData.ElectiveCourseStatus = ddlCompleteIncomplete.SelectedValue;
                courseData.GradeRecieved = ddlGrade.SelectedValue;
                _uowCourseMap.SaveChanges();
            }
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrerequisites_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Please complete prerequisite Course to proceed";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var studentId = UiHelpers.StudentData.Id;
            var contextId = Int64.Parse(Convert.ToString(Request.QueryString["ContextId"]));
            if (!_uowCourseMap.StudentCourseInformations.Any(s => s.StudentId == studentId))
            {
                var preCourseData =
                    (from pr in _uowCourseMap.PrerequisiteCourseInformations where pr.CourseId == contextId select pr).ToList();
                var studentPreCourseData =
                    (from pr in _uowCourseMap.StudentPrerequisiteCourses where pr.StudentId == studentId select pr).ToList();
                if (preCourseData.Count != studentPreCourseData.Count && preCourseData.Count > 0)
                {
                    var newStudentCourse = new StudentCourseInformation
                    {
                        StudentId = studentId,
                        CourseId = contextId,
                        CourseStatus =ddlCompleteIncomplete.SelectedValue,
                        GradeRecieved = ddlGrade.SelectedValue
                    };
                    _uowCourseMap.StudentCourseInformations.Add(newStudentCourse);
                    _uowCourseMap.SaveChanges();
                }
                else lblMessage.Text = "Prerequisites Courses need to completed before Course Registration";

            }
            else lblMessage.Text = "You have already registered for this course";

        }
    }
}