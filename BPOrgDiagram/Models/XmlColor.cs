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
using System.Drawing;
using System.Xml.Serialization;


namespace BasicPrimitives.OrgDiagram
{
    public class XmlColor
    {
        #region Fields

        private Color m_color = Color.Transparent;

        #endregion // Fields

        #region Constructor

        public XmlColor() 
        { 
        
        }

        public XmlColor(Color color) 
        { 
            m_color = color; 
        }

        #endregion // Constructor

        #region Public Properties

        [XmlAttribute]
        public string Value
        {
            get 
            {
                string result = String.Empty;

                if (this.m_color.IsNamedColor)
                {
                    result = m_color.Name;
                }
                else
                {
                    result = string.Format("{0}:{1}:{2}:{3}", m_color.A, m_color.R, m_color.G, m_color.B);
                }

                return result; 
            }
            set
            {
                if (value.Contains(":"))
                {
                    string[] argb = value.Split(':');
                    this.m_color = Color.FromArgb(byte.Parse(argb[0]), byte.Parse(argb[1]), byte.Parse(argb[2]), byte.Parse(argb[3]));
                }
                else
                {
                    this.m_color = Color.FromName(value);
                }
            }
        }

        #endregion // Public Properties

        #region Public Methods

        public Color ToColor()
        {
            return m_color;
        }

        public void FromColor(Color color)
        {
            m_color = color;
        }

        public static implicit operator Color(XmlColor xmlColor)
        {
            return xmlColor.ToColor();
        }

        public static implicit operator XmlColor(Color color)
        {
            return new XmlColor(color);
        }

        #endregion // Public Methods
    }
}
