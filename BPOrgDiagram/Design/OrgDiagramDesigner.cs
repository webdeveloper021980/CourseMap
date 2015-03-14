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
using System.Web.UI.Design;
using System.IO;
using System.Web.UI;

namespace BasicPrimitives.OrgDiagram.Design
{
    public class OrgDiagramDesigner : ControlDesigner
    {
        #region Private member variables

        private OrgDiagramServerControl m_instance;

        #endregion // Private member variables

        #region Public Methods

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            m_instance = (OrgDiagramServerControl)component;
            base.Initialize(component);
        }

        public override string GetDesignTimeHtml()
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            m_instance.RenderBeginTag(writer);

            new LiteralControl("<span style='font-size:12px;'>Basic Primitives BPOrgDiagram</span>").RenderControl(writer);

            m_instance.RenderEndTag(writer);
            return sw.ToString();
        }

        #endregion // Public Methods
    }
}

