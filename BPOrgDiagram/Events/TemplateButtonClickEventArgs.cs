using System;

namespace BasicPrimitives.OrgDiagram.Events
{
    public class TemplateButtonClickEventArgs : System.EventArgs
    {
        private string m_name;
        private Item m_item;

        #region Constructor

        public TemplateButtonClickEventArgs(Item item, String buttonName)
        {
            this.m_name = buttonName;
            this.m_item = item;
        }

        #endregion //Contructor

        public string ButtonName 
        { 
            get 
            { 
                return this.m_name; 
            } 
        }

        public Item Item
        {
            get
            {
                return m_item;
            }
        }
    }
}

