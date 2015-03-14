/**
 * Basic Primitives ASP.NET BPOrgDiagram
 *
 * (c) Basic Primitives Inc
 *
 *
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 *
 */

using System;
using System.ComponentModel;
using BasicPrimitives.OrgDiagram.Design;


namespace BasicPrimitives.OrgDiagram
{
    [TypeConverter(typeof(ItemConverter))]
    [Serializable]
    public class Button
    {
        private string m_name;
        private IconType m_icon;

        #region Constructor

        public Button()
        {
            this.m_name = "name";
            this.m_icon = IconType.Home;
        }

        #endregion //COntructor

        #region Internal

        [Category()]
        [DefaultValue(typeof(string), "Button Name")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Name
        {
            get 
            { 
                return m_name; 
            }
            set 
            { 
                m_name = value; 
            }
        }

        [Category()]
        [DefaultValue(IconType.Home)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public IconType Icon
        {
            get
            {
                return m_icon;
            }
            set
            {
                m_icon = value;
            }
        }

        #endregion // Public Properties

        #region Public Methods

        public override string ToString()
        {
            string result = base.ToString();

            if (!string.IsNullOrEmpty(m_name))
            {
                result = m_name;
            }

            return result;
        }

        #endregion // Public Methods
    }
}
