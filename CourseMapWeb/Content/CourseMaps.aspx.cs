using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using BasicPrimitives.OrgDiagram;
using BasicPrimitives.OrgDiagram.Events;
using CourseMapWeb.Common;
using CourseMapWeb.DataModel;

namespace CourseMapWeb.Content
{
    public partial class CourseMaps : Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var studentData = UiHelpers.StudentData;
        
                using (var uow = new CourseMapDataModel())
                {
                    var majorData =
                        (from mj in uow.MajorInformations where mj.DepartmentId == studentData.DepartmentId select mj).FirstOrDefault();
                    treeViewCourseDiagram.Items.Clear();
                    var majorItem = new Item(majorData.Major, Convert.ToString(majorData.Id), "Major", null)
                    {
                        TemplateName = "UserTemplateContact",
                        TitleColor = Color.LimeGreen
                    };


                    var courseData =
                        (from mj in uow.CourseInformations where mj.MajorId == majorData.Id select mj).ToList();
                    foreach (var courseInformation in courseData)
                    {
                        var courseItem = new Item(courseInformation.CourseName, Convert.ToString(courseInformation.Id), "Course", null)
                        {
                            ShowCheckBox = Enabled.False,
                            ChildrenPlacementType = ChildrenPlacementType.Horizontal
                        };
                        var preCourseData =
                        (from mj in uow.PrerequisiteCourseInformations where mj.CourseId == courseInformation.Id select mj).ToList();
                        foreach (var tempCourseItem in preCourseData)
                        {
                            var preCourseItem = new Item(tempCourseItem.PrerequisiteCourseName,
                                Convert.ToString(tempCourseItem.Id), "Prerequisite Course", null);

                            courseItem.Items.Add(preCourseItem);
                        }
                        var electiveCourseData =
                        (from mj in uow.ElectiveInformations where mj.CourseId == courseInformation.Id select mj).ToList();
                        foreach (var tempCourseItem in electiveCourseData)
                        {
                            var preCourseItem = new Item(tempCourseItem.ElectiveName,
                                Convert.ToString(tempCourseItem.Id), "Elective Course", null);

                            courseItem.Items.Add(preCourseItem);
                        }
                        majorItem.Items.Add(courseItem);

                    }
                    treeViewCourseDiagram.AutoSizeOnWindowSize = true;
                    treeViewCourseDiagram.Items.Add(majorItem);
                }

              
            }
        }

        protected void treeViewCourseDiagram_TemplateButtonClick(object sender, TemplateButtonClickEventArgs e)
        {
            iframePopup.Src = "CourseMapPopup.aspx?ContextId=" + e.Item.Value + "&Type=" + e.Item.Description + "";
            ModalPopupExtenderCourseMap.Show();
        }


      
    }
}