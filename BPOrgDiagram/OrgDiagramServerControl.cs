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
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using BasicPrimitives.OrgDiagram.Design;
using System.IO;
using System.Xml;
using System.Web.Script.Serialization;
using System.Reflection;
using BasicPrimitives.OrgDiagram.Events;

namespace BasicPrimitives.OrgDiagram
{
    /// <summary>
    /// Summary description for OrgDiagramServerControl
    /// </summary>
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:OrgDiagramServerControl runat=server></{0}:OrgDiagramServerControl>")]
    [Designer("BasicPrimitives.OrgDiagram.Design.OrgDiagramDesigner")]
    public class OrgDiagramServerControl : ScriptControl, INamingContainer, IPostBackEventHandler
    {
        #region Private Member Variables

        private Item m_rootItem;
        private Items m_items;
        private Buttons m_buttons;
        private String m_defaultTemplateName = String.Empty;
        private PageFitMode m_pageFitMode = PageFitMode.FitToPage;
        private OrientationType m_orientationType = OrientationType.Top;
        private SelectionPathMode m_selectionPathMode = SelectionPathMode.FullStack;
        private ItemVisibility m_minimalVisibility = ItemVisibility.Dot;
        private HorizontalAlignmentType m_horizontalAlignmentType = HorizontalAlignmentType.Center;
        private VerticalAlignmentType m_verticalAlignmentType = VerticalAlignmentType.Middle;
        private ConnectorType m_connectorType = ConnectorType.Squared;
        private ChildrenPlacementType m_childrenPlacementType = ChildrenPlacementType.Horizontal;
        private ChildrenPlacementType m_leavesPlacementType = ChildrenPlacementType.Matrix;
        private BasicPrimitives.OrgDiagram.Enabled m_showCheckBoxes = OrgDiagram.Enabled.True;
        private BasicPrimitives.OrgDiagram.Enabled m_showButtons = OrgDiagram.Enabled.Auto;
        private bool m_autoSizeOnWindowSize = true;
        private bool m_autoPostBack = false;

        private Items m_checkedItems;
        private Item m_selectedItem;
        private String m_clickedButtonName;
        private Item m_clickedButtonItem;

        private List<Type> m_itemTypes = new List<Type>();

        #endregion

        #region Constructor 

        public OrgDiagramServerControl()
        {
            m_items = new Items(RootItem);
            m_buttons = new Buttons();

            NormalLevelShift = 20;
            DotLevelShift = 20;
            LineLevelShift = 10;
            NormalItemsInterval = 10;
            DotItemsInterval = 1;
            LineItemsInterval = 2;

            MaximumColumnsInMatrix = 6;

            Width = 640;
            Height = 480;

            if (HttpContext.Current != null)
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.GlobalAssemblyCache)
                    {
                        try
                        {
                            foreach (Type availableType in assembly.GetTypes())
                            {
                                if (typeof(Item).IsAssignableFrom(availableType))
                                {
                                    m_itemTypes.Add(availableType);
                                }
                            }
                        } catch {}
                    }
                }
            }
        }

        #endregion // Constructor
        
        #region Public properties

        [Category("Behavior")]
        [DefaultValue(PageFitMode.FitToPage)]
        public PageFitMode PageFitMode
        {
            get 
            { 
                return m_pageFitMode; 
            }
            set
            {
                m_pageFitMode = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(OrientationType.Top)]
        public OrientationType OrientationType
        {
            get
            {
                return m_orientationType;
            }
            set
            {
                m_orientationType = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(SelectionPathMode.FullStack)]
        public SelectionPathMode SelectionPathMode
        {
            get
            {
                return m_selectionPathMode;
            }
            set
            {
                m_selectionPathMode = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(ItemVisibility.Dot)]
        public ItemVisibility MinimalVisibility
        {
            get
            {
                return m_minimalVisibility;
            }
            set
            {
                m_minimalVisibility = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(HorizontalAlignmentType.Center)]
        public HorizontalAlignmentType HorizontalAlignmentType
        {
            get
            {
                return m_horizontalAlignmentType;
            }
            set
            {
                m_horizontalAlignmentType = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(VerticalAlignmentType.Middle)]
        public VerticalAlignmentType VerticalAlignmentType
        {
            get
            {
                return m_verticalAlignmentType;
            }
            set
            {
                m_verticalAlignmentType = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(ConnectorType.Squared)]
        public ConnectorType ConnectorType
        {
            get
            {
                return m_connectorType;
            }
            set
            {
                m_connectorType = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(ChildrenPlacementType.Horizontal)]
        public ChildrenPlacementType ChildrenPlacementType
        {
            get
            {
                return m_childrenPlacementType;
            }
            set
            {
                m_childrenPlacementType = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(ChildrenPlacementType.Matrix)]
        public ChildrenPlacementType LeavesPlacementType
        {
            get
            {
                return m_leavesPlacementType;
            }
            set
            {
                m_leavesPlacementType = value;
            }
        }

        [Category("Behavior")]
        [Description("Defines number of columns in leaves matrix.")]
        [DefaultValue(6)]
        public int MaximumColumnsInMatrix
        {
            get;
            set;
        }

        [Category()]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ItemCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Items Items
        {
            get 
            { 
                return m_items;
            }
        }

        [Category()]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ButtonCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public Buttons Buttons
        {
            get
            {
                return m_buttons;
            }
        }

        [Browsable(true)]
        [Description("Default template name.")]
        public String DefaultTemplateName
        {
            get
            {
                return m_defaultTemplateName;
            }
            set
            {
                m_defaultTemplateName = value;
            }
        }

        [Description("Indicates whether a check box is displayed in default cursor template.")]
        [DefaultValue(BasicPrimitives.OrgDiagram.Enabled.True)]
        [Category("Behavior")]
        public BasicPrimitives.OrgDiagram.Enabled ShowCheckBoxes 
        {
            get
            {
                return m_showCheckBoxes;
            }
            set
            {
                m_showCheckBoxes = value;
            }
        }

        [Description("Indicates whether a buttons panel is displayed in default cursor template.")]
        [DefaultValue(BasicPrimitives.OrgDiagram.Enabled.Auto)]
        [Category("Behavior")]
        public BasicPrimitives.OrgDiagram.Enabled ShowButtons 
        {
            get
            {
                return m_showButtons;
            }
            set
            {
                m_showButtons = value;
            }
        }

        [Category("Behavior")]
        [Description("Autosize control on window size. Use client side code to resize widget if you have a custom layout manager.")]
        [DefaultValue(true)]
        public bool AutoSizeOnWindowSize 
        {
            get
            {
                return m_autoSizeOnWindowSize;
            }
            set
            {
                m_autoSizeOnWindowSize = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public virtual bool AutoPostBack
        {
            get
            {
                return m_autoPostBack;
            }
            set
            {
                m_autoPostBack = value;
            }
        }

        #endregion // Public properties

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Item SelectedItem
        {
            get
            {
                return m_selectedItem;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Items CheckedItems
        {
            get
            {
                if (m_checkedItems == null)
                {
                    m_checkedItems = new Items(null, false);
                }
                return m_checkedItems;
            }
        }

      
        [Category("Styles")]
        [Description("Defines interval after level of items in  diagram having items in normal state.")]
        [DefaultValue(20)]
        public int NormalLevelShift 
        {
            get; set;
        }

        [Category("Styles")]
        [Description("Defines interval after level of items in  diagram having items in dot state.")]
        [DefaultValue(20)]
        public int DotLevelShift 
        {
            get; set;
        }

        [Category("Styles")]
        [Description("Defines interval after level of items in  diagram having items in line state.")]
        [DefaultValue(10)]
        public int LineLevelShift 
        {
            get; set;
        }

        [Category("Styles")]
        [Description("Defines interval between items at the same level in  diagram having items in normal state.")]
        [DefaultValue(10)]
        public int NormalItemsInterval 
        {
            get; set;
        }

        [Category("Styles")]
        [Description("Defines interval between items at the same level in  diagram having items in dot state.")]
        [DefaultValue(1)]
        public int DotItemsInterval 
        {
            get; set;
        }

        [Category("Styles")]
        [Description("Defines interval between items at the same level in  diagram having items in line state.")]
        [DefaultValue(2)]
        public int LineItemsInterval 
        {
            get; set;
        }

        #region Public Events

        public event EventHandler SelectedItemChanged;

        protected virtual void OnSelectedNodeChanged(EventArgs e)
        {
            if (SelectedItemChanged != null)
            {
                SelectedItemChanged(this, e);
            }
        }

        public event EventHandler ItemCheckChanged;

        protected virtual void OnItemCheckChanged(EventArgs e)
        {
            if (ItemCheckChanged != null)
            {
                ItemCheckChanged(this, e);
            }
        }

        public delegate void TemplateButtonClickEventHandler(object sender, TemplateButtonClickEventArgs e);

        public event TemplateButtonClickEventHandler TemplateButtonClick;

        protected virtual void OnTemplateButtonClick(TemplateButtonClickEventArgs e)
        {
            if (TemplateButtonClick != null)
            {
                TemplateButtonClick(this, e);
            }
        }

        
        public void RaisePostBackEvent(string eventArgument)
        {
            switch (eventArgument)
            {
                case "ItemCheckChanged":
                    OnItemCheckChanged(null);
                    break;
                case "SelectedItemChanged":
                    OnSelectedNodeChanged(null);
                    break;
                case "TemplateButtonClick":
                    OnTemplateButtonClick(new TemplateButtonClickEventArgs(m_clickedButtonItem, m_clickedButtonName));
                    break;
                default:
                    break;
            }
        }

        #endregion // Public Events

        #region Public Methods

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            LoadControlViewState();

            LoadDataPostBack();
        }

        #endregion // Public Methods

        #region Private Methods

        protected override void CreateChildControls()
        {
            Controls.Clear();

            PlaceHolder placeHolder = new PlaceHolder();
            HiddenField parametersField = new HiddenField();
            parametersField.ID = "parameters";
            placeHolder.Controls.Add(parametersField);
            Controls.Add(placeHolder);
        }

        protected override object SaveViewState()
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Items), m_itemTypes.ToArray());
            StringWriter stringWriter = new StringWriter();
            XmlWriter writer = XmlWriter.Create(stringWriter);
            xmlSerializer.Serialize(writer, Items);
            var xml = stringWriter.ToString();
            ViewState.Add("Items", xml);

            return base.SaveViewState();
        }

        private void LoadControlViewState()
        {
            if (ViewState["Items"] != null)
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Items), m_itemTypes.ToArray());
                Items items;
                StringReader stringReader = new StringReader((string)ViewState["Items"]);
                using (XmlReader reader = XmlReader.Create(stringReader))
                {
                    items = (Items)xmlSerializer.Deserialize(reader);
                }

                Items.Clear();
                Items.AddRange(items.GetValues());
            }
        }

        private void LoadDataPostBack()
        {
            Dictionary<int, Item> itemsHash = new Dictionary<int,Item>();
            int index = 0;
            CollectRecursiveItemsHash(Items, itemsHash, ref index);

            HttpContext context = HttpContext.Current;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Control parametersControl = FindControl("parameters");
            String id = parametersControl.ClientID.Replace("_", "$");
            //var body = context.Request[id];
            var body = context.Request[id] != null ? context.Request[id] : context.Request[parametersControl.UniqueID];
            if (body != null)
            {
                var obj = serializer.DeserializeObject(body);
                Dictionary<string, object> parameters = (Dictionary<string, object>)obj;
                int selectedItemIndex = -1;
                if (parameters["selectedItem"] != null)
                {
                    int.TryParse(parameters["selectedItem"].ToString(), out selectedItemIndex);
                    SetSelectedItem(null);
                    if (itemsHash.ContainsKey(selectedItemIndex))
                    {
                        itemsHash[selectedItemIndex].Selected = true;
                    }
                }
                UncheckItems();
                if (parameters["checkedItems"] != null)
                {
                    object[] checkedItems = (object[])parameters["checkedItems"];
                    foreach (object checkItem in checkedItems)
                    {
                        int checkedItemIndex = -1;
                        int.TryParse(checkItem.ToString(), out checkedItemIndex);

                        if (itemsHash.ContainsKey(checkedItemIndex))
                        {
                            itemsHash[checkedItemIndex].Checked = true;
                        }
                    }
                }

                m_clickedButtonItem = null;
                if (parameters.ContainsKey("clickedButtonItemIndex"))
                {
                    int clickedButtonItemIndex = -1;
                    int.TryParse(parameters["clickedButtonItemIndex"].ToString(), out clickedButtonItemIndex);
                    if (itemsHash.ContainsKey(clickedButtonItemIndex))
                    {
                        m_clickedButtonItem = itemsHash[clickedButtonItemIndex];
                       
                    }
                }

                m_clickedButtonName = String.Empty;
                if (parameters.ContainsKey("clickedButtonName"))
                {
                    m_clickedButtonName = parameters["clickedButtonName"].ToString();
                    RaisePostBackEvent("TemplateButtonClick");
                }
            }
        }



        private void CollectRecursiveItemsHash(Items items, Dictionary<int, Item> itemsHash, ref int index)
        {
            foreach (Item item in items)
            {
                item.Index = index++;
                itemsHash.Add(item.Index, item);

                CollectRecursiveItemsHash(item.Items, itemsHash, ref index);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            SaveWidgetParameters();

            base.Render(writer);
        }

        private void SaveWidgetParameters()
        {
            var obj = new {AutoPostBack
                    , AutoSizeOnWindowSize
                    , MinimalVisibility
                    , SelectionPathMode
                    , ShowCheckBoxes
                    , ShowButtons
                    , PageFitMode
                    , OrientationType
                    , DefaultTemplateName
                    , NormalLevelShift
                    , DotLevelShift
                    , LineLevelShift
                    , NormalItemsInterval
                    , DotItemsInterval
                    , LineItemsInterval
                    , HorizontalAlignmentType
                    , VerticalAlignmentType
                    , ConnectorType
                    , ChildrenPlacementType
                    , LeavesPlacementType
                    , MaximumColumnsInMatrix
                    , Items
                    , Buttons
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string parameters = serializer.Serialize(obj);
            Attributes.Add("data-parameters", parameters);
        }

        protected override IEnumerable<ScriptDescriptor>
        GetScriptDescriptors()
        {
            yield return new ScriptControlDescriptor("BasicPrimitives.OrgDiagram.OrgDiagramClientControl", ClientID);
        }

        // Generate the script reference
        protected override IEnumerable<ScriptReference>
                GetScriptReferences()
        {
            yield return new ScriptReference("BasicPrimitives.OrgDiagram.OrgDiagramClientControl.js", GetType().Assembly.FullName);
        }

        protected override System.Web.UI.HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        #endregion // Private Methods

        #region Internal

        internal Item RootItem
        {
            get
            {
                if (m_rootItem == null)
                {
                    m_rootItem = new Item(this, true);
                }
                return m_rootItem;
            }
        }

        internal void SetSelectedItem(Item item)
        {
            if (m_selectedItem != item)
            {
                if ((m_selectedItem != null) && m_selectedItem.Selected)
                {
                    m_selectedItem.SetSelected(false);
                }
                m_selectedItem = item;
                if ((m_selectedItem != null) && !m_selectedItem.Selected)
                {
                    m_selectedItem.SetSelected(true);
                }
            }
        }

        internal void UncheckItems()
        {
            foreach (Item item in CheckedItems.GetValues())
            {
                item.Checked = false;
            }
        }

        #endregion // INternal
    }
}