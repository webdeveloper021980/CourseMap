using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using CourseMapWeb.Common;
using CourseMapWeb.DataModel;

namespace CourseMapWeb.Content
{
    public partial class CourseMapView : System.Web.UI.Page
    {
        private readonly CourseMapDataModel _uowCourseMap = new CourseMapDataModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var query = " SELECT * FROM Vw_StudentCourseInformation WHERE MajorId = " +
                            Convert.ToString(UiHelpers.StudentData.MajorId) + " ORDER BY CourseId";

                var dtCourse = SqlHelper.DataTable(query, CommandType.Text);
                GridViewCourseMaps.DataSource = dtCourse;
                GridViewCourseMaps.DataBind();
            }
        }

        private bool IsPrerequisiteCoursesDone(Int64 courseId)
        {
            var preCourseData =
                 (from pr in _uowCourseMap.PrerequisiteCourseInformations where pr.CourseId == courseId select pr).ToList();
            var studentPreCourseData =
                (from pr in _uowCourseMap.StudentPrerequisiteCourses where pr.StudentId == UiHelpers.StudentData.Id select pr).ToList();
            if (preCourseData.Count != studentPreCourseData.Count && preCourseData.Count > 0)
                return false;
            return true;
        }
        protected void GridViewCourseMaps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var courseId = Int64.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "I")
            {
                if (!IsPrerequisiteCoursesDone(courseId))
                    lblMessage.Text = "Please complete prerequisite Course to proceed";
            }
            else if (e.CommandName == "UpdateRow")
            {
                var courseData =
                    _uowCourseMap.StudentCourseInformations.FirstOrDefault(
                        s => s.StudentId == UiHelpers.StudentData.Id && s.CourseId == courseId);
                var ddlGradeReceived = (DropDownList)GridViewCourseMaps.Rows[0].FindControl("ddlGradeReceived");
                var ddlStatus = (DropDownList)GridViewCourseMaps.Rows[0].FindControl("ddlStatus");
                if (string.IsNullOrEmpty(ddlGradeReceived.SelectedValue) ||
                    string.IsNullOrEmpty(ddlStatus.SelectedValue))
                {
                    lblMessage.Text = "Grade/Status is required";
                    return;
                }
                if (courseData != null)
                {
                    courseData.CourseStatus = ddlStatus.SelectedValue;
                    courseData.GradeRecieved = ddlGradeReceived.SelectedValue;
                    _uowCourseMap.SaveChanges();
                    lblMessage.Text = "Grade/Status updated.";
                }
                else
                {
                    var newStudentCourse = new StudentCourseInformation
                    {
                        StudentId = UiHelpers.StudentData.Id,
                        CourseId = courseId,
                        CourseStatus = ddlStatus.SelectedValue,
                        GradeRecieved = ddlGradeReceived.SelectedValue
                    };
                    _uowCourseMap.StudentCourseInformations.Add(newStudentCourse);
                    _uowCourseMap.SaveChanges();

                    lblMessage.Text = "Grade/Status updated.";
                }
            }
        }

        protected void GridViewCourseMaps_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var gradeRecieved = GridViewCourseMaps.DataKeys[e.Row.RowIndex].Values[1].ToString();
                var ddlGradeReceived = (DropDownList)e.Row.FindControl("ddlGradeReceived");
                if (ddlGradeReceived != null)
                    UiHelpers.SetDropdownSelectedItem(ref ddlGradeReceived, gradeRecieved);

                var courseStatus = GridViewCourseMaps.DataKeys[e.Row.RowIndex].Values[2].ToString();
                var ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                if (ddlStatus != null)
                    UiHelpers.SetDropdownSelectedItem(ref ddlStatus, courseStatus);

                if (!IsPrerequisiteCoursesDone(Int64.Parse(e.Row.Cells[0].Text)))
                {
                    var btnUpdate = (Button)e.Row.FindControl("btnUpdate");
                    if (btnUpdate != null)
                        btnUpdate.Enabled = false;
                }
            }
        }
    }
}