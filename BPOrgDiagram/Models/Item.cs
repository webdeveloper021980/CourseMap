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
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using BasicPrimitives.OrgDiagram.Design;
using System.Web.Script.Serialization;


namespace BasicPrimitives.OrgDiagram
{
    [TypeConverter(typeof(ItemConverter))]
    [Serializable]
    public class Item
    {
        private string m_title;
        private string m_groupTitle;
        private string m_description;
        private string m_value;
        private string m_templateName;

        private ItemType m_itemType = ItemType.Regular;
        private AdviserPlacementType m_adviserPlacementType = AdviserPlacementType.Auto;
        private ChildrenPlacementType m_childrenPlacementType = ChildrenPlacementType.Auto;

        private OrgDiagramServerControl m_owner;
        private bool m_isRoot;
        private Item m_parent;
        private int m_index;
        private int m_selectDesired;
        private bool m_modifyCheckedItems;
        private bool m_selected;
        private bool m_isVisible = true;
        private bool m_checked;
        private Color m_titleColor = Color.Navy;
        private Color m_groupTitleColor = Color.Navy;

        private Items m_items;

        #region Constructor

        public Item()
            : this("Title", "Value")
        {
            this.m_selectDesired = 0;
        }

        protected internal Item(OrgDiagramServerControl owner, bool isRoot) 
            : this()
        {
            this.m_owner = owner;
            this.m_isRoot = isRoot;
        }

        public Item(string title, string value)
        {
            this.m_title = title;
            this.m_value = value;

            this.m_items = new Items(this);
        }



        public Item(string title, string value, string description, string imageUrl)
            : this(title, value)
        {
            this.Description = description;
            this.ImageUrl = imageUrl;
        }

        public Item(string title, string value, Item[] items)
            : this(title, value)
        {
            this.Items.AddRange(items);
        }

        #endregion //COntructor

        #region Internal

        internal void SetOwner(OrgDiagramServerControl owner)
        {
            this.m_owner = owner;
            if (this.m_selectDesired == 1)
            {
                this.m_selectDesired = 0;
                this.Selected = true;
            }
            else if (this.m_selectDesired == -1)
            {
                this.m_selectDesired = 0;
                this.Selected = false;
            }
            if (this.m_modifyCheckedItems && (this.m_owner != null))
            {
                this.m_modifyCheckedItems = false;
                if (this.Checked)
                {
                    if (!this.m_owner.CheckedItems.Contains(this))
                    {
                        this.m_owner.CheckedItems.Add(this);
                    }
                }
                else
                {
                    this.m_owner.CheckedItems.Remove(this);
                }
            }
            foreach (Item node in this.Items)
            {
                node.SetOwner(this.m_owner);
            }
        }

        internal void SetParent(Item parent)
        {
            this.m_parent = parent;
        }

        internal void SetSelected(bool value)
        {
            this.m_selected = value;
            if (this.m_owner == null)
            {
                this.m_selectDesired = value ? 1 : -1;
            }
        }

        internal Item GetParentInternal()
        {
            return this.m_parent;
        }


        internal OrgDiagramServerControl Owner
        {
            get
            {
                return this.m_owner;
            }
        }

        internal int Index
        {
            get
            {
                return this.m_index;
            }
            set
            {
                this.m_index = value;
            }
        }

        #endregion // Internal Methods

        #region Public Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [ScriptIgnore]
        public Item Parent
        {
            get
            {
                if ((this.m_parent != null) && !this.m_parent.m_isRoot)
                {
                    return this.m_parent;
                }
                return null;
            }
        }

        [Category()]
        [DefaultValue(typeof(string), "Title")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get 
            { 
                return m_title; 
            }
            set 
            { 
                m_title = value; 
            }
        }

        [Category()]
        [DefaultValue(typeof(string), "")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string GroupTitle
        {
            get
            {
                return m_groupTitle;
            }
            set
            {
                m_groupTitle = value;
            }
        }

        [Category()]
        [DefaultValue(typeof(string), "")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Description
        {
            get
            {
                return m_description;
            }
            set
            {
                m_description = value;
            }
        }

        [Category("")]
        [DefaultValue(ItemType.Regular)]
        public ItemType ItemType
        {
            get
            {
                return m_itemType;
            }
            set
            {
                this.m_itemType = value;
            }
        }

        [Category("")]
        [DefaultValue(AdviserPlacementType.Auto)]
        public AdviserPlacementType AdviserPlacementType
        {
            get
            {
                return m_adviserPlacementType;
            }
            set
            {
                this.m_adviserPlacementType = value;
            }
        }

        [Category("")]
        [DefaultValue(ChildrenPlacementType.Auto)]
        public ChildrenPlacementType ChildrenPlacementType
        {
            get
            {
                return m_childrenPlacementType;
            }
            set
            {
                this.m_childrenPlacementType = value;
            }
        }

        /// <summary>
        /// Default item template description.
        /// </summary>
        [Category()]
        [DefaultValue(typeof(string), "default")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] 
        public string TemplateName
        {
            get
            {
                return this.m_templateName;
            }
            set
            {
                this.m_templateName = value;
            }
        }

        //
        // Summary:
        //     Gets or sets a value that indicates whether the node is visible.
        //
        // Returns:
        //     true if the node is visible; otherwise, false. The default is true.
        [DefaultValue(true)]
        [Description("Indicates whether the node is visible")]
        public bool IsVisible
        {
            get
            {
                return this.m_isVisible;
            }
            set
            {
                this.m_isVisible = value;
            }
        }

        //
        // Summary:
        //     Gets or sets a value that indicates whether the node is selected.
        //
        // Returns:
        //     true if the node is selected; otherwise, false. The default is false.
        [DefaultValue(false)]
        [Description("Indicates whether the node is selected")]
        public bool Selected 
        {
            get
            {
                return this.m_selected;
            }
            set
            {
                this.SetSelected(value);
                if (this.m_owner == null)
                {
                    this.m_selectDesired = value ? 1 : -1;
                }
                else if (value)
                {
                    this.m_owner.SetSelectedItem(this);
                }
                else if (this == this.m_owner.SelectedItem)
                {
                    this.m_owner.SetSelectedItem(null);
                }            
            }
        }

        //
        // Summary:
        //     Gets or sets the URL to an image that is displayed next to the node.
        //
        // Returns:
        //     The URL to a custom image that is displayed next to the node. The default
        //     value is an empty string (""), which indicates that this property is not
        //     set.
        [DefaultValue("")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty]
        [Description("The URL to a custom image that is displayed in the node")]
        public string ImageUrl { get; set; }

        [Category()]
        [DefaultValue(typeof(string), "Value")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Value
        {
            get 
            { 
                return m_value; 
            }
            set 
            { 
                m_value = value; 
            }
        }

        [Category()]
        [DefaultValue(typeof(Color), "navy")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(WebColorConverter))]
        [XmlElement(Type = typeof(XmlColor))]
        public Color TitleColor
        {
            get
            {
                return m_titleColor;
            }
            set
            {
                m_titleColor = value;
            }
        }

        [Category()]
        [DefaultValue(typeof(Color), "navy")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(WebColorConverter))]
        [XmlElement(Type = typeof(XmlColor))]
        public Color GroupTitleColor
        {
            get
            {
                return m_groupTitleColor;
            }
            set
            {
                m_groupTitleColor = value;
            }
        }

        // Summary:
        //     Gets or sets a value that indicates whether the node's check box is selected.
        //
        // Returns:
        //     true if the node's check box is selected; otherwise, false. The default is
        //     false.
        [DefaultValue(false)]
        [Description("Indicates whether the item's check box is selected")]
        public bool Checked
        {
            get
            {
                return m_checked;
            }
            set
            {
                this.m_checked = value;
                this.NotifyOwnerChecked();
            }
        }

        //
        // Summary:
        //     Gets or sets a value that indicates whether a check box is displayed next
        //     to the node.
        //
        // Returns:
        //     true to display the check box; otherwise, false.
        [Description("Indicates whether a check box is displayed in default cursor template.")]
        [DefaultValue(BasicPrimitives.OrgDiagram.Enabled.Auto)]
        public Enabled ShowCheckBox { get; set; }

        //
        // Summary:
        //     Gets or sets a value that indicates whether a buttons panel is displayed next
        //     to the node.
        //
        // Returns:
        //     true to display buttons; otherwise, false.
        [Description("Indicates whether a buttons panel is displayed in default cursor template.")]
        [DefaultValue(BasicPrimitives.OrgDiagram.Enabled.Auto)]
        public Enabled ShowButtons { get; set; }

        [Category()]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ItemCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("Child Items")]
        public virtual Items Items
        {
            get 
            { 
                return m_items; 
            }
        }

        #endregion // Public Properties

        #region Public Methods

        public override string ToString()
        {
            string result = base.ToString();

            if (!string.IsNullOrEmpty(m_title))
            {
                result = m_title;
            }

            return result;
        }

        #endregion // Public Methods

        #region Private Method

        private void NotifyOwnerChecked()
        {
            if (this.m_owner == null)
            {
                this.m_modifyCheckedItems = true;
            }
            else
            {
                if (this.m_checked)
                {
                    if (!this.m_owner.CheckedItems.Contains(this))
                    {
                        this.m_owner.CheckedItems.Add(this);
                    }
                }
                else
                {
                    this.m_owner.CheckedItems.Remove(this);
                }
            }
        }

        #endregion // Private Method
    }
}
