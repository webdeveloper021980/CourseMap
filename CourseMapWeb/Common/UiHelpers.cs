using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using CourseMapWeb.DataModel;

namespace CourseMapWeb.Common
{
    public static class UiHelpers
    {
        public const string SelectOne = "Select One";
        public const string Major = "Major";
        public const string Course = "Course";
        public const string PrerequisiteCourse = "Prerequisite Course";
        public const string ElectiveCourse = "Elective Course";
        public static string HashPassword(string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        private const string Studentkey = "StudentKey";
        public static StudentBasicInformation StudentData
        {
            get { return (StudentBasicInformation) HttpContext.Current.Session[Studentkey]; }
            set { HttpContext.Current.Session[Studentkey] = value; }
        }

        public static void SetDropdownSelectedItem(ref DropDownList comboBox,string contextId)
        {
            comboBox.ClearSelection();
            var combo = comboBox.Items.FindByValue(contextId);
            if (combo != null)
                combo.Selected = true;
        }
    }
}