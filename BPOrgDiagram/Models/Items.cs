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
using System.Collections;


namespace BasicPrimitives.OrgDiagram
{
    public class Items : CollectionBase
    {
        #region Primate Fields

        private Item m_owner;
        private bool m_updateParent;

        #endregion // Primate Fields

        #region Constructor

        public Items() 
            : this(null, true)
        {
        }
        
        public Items(Item owner) 
            : this(owner, true)
        {
        }

        internal Items(Item owner, bool updateParent)
        {
            this.m_owner = owner;
            this.m_updateParent = updateParent;
        }

        #endregion // Constructor

        #region Public Properties

        /// <summary>
        /// Get item by index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Item</returns>
        public Item this[int index]
        {
            get
            {
                return (Item)this.InnerList[index];
            }
            set
            {
                this.InnerList[index] = value;
            }
        }

        #endregion // Public Properties

        #region Public Methods

        /// <summary>
        /// Add single item
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Added item</returns>
        public void Add(Item item)
        {
            this.AddAt(this.Count, item);
        }

        /// <summary>
        /// Add single item
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Added item</returns>
        public void AddAt(int index, Item item)
        {
            if (this.m_updateParent)
            {
                if ((item.Owner != null) && (item.Parent == null))
                {
                    item.Owner.Items.Remove(item);
                }
                if (item.Parent != null)
                {
                    item.Parent.Items.Remove(item);
                }
                if (this.m_owner != null)
                {
                    item.SetParent(this.m_owner);
                    item.SetOwner(this.m_owner.Owner);
                }
            }
            this.InnerList.Insert(index, item);
        }

        /// <summary>
        /// Add array of items
        /// </summary>
        /// <param name="items">Items array</param>
        public void AddRange(Item[] items)
        {
            for (int index = 0; index < items.Length; index++)
            {
                this.Add(items[index]);
            }
        }

        /// <summary>
        /// Remove item
        /// </summary>
        /// <param name="item">Item</param>
        public void Remove(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = this.InnerList.IndexOf(item);
            if (index != -1)
            {
                this.RemoveAt(index);
            }
        }

        public new void RemoveAt(int index)
        {
            Item item = (Item)this.InnerList[index];
            if (this.m_updateParent)
            {
                OrgDiagramServerControl owner = item.Owner;
                if (owner != null)
                {
                    if (owner.CheckedItems.Count != 0)
                    {
                        UnCheckUnSelectRecursive(item);
                    }
                    else
                    {
                        for (Item selectedItem = owner.SelectedItem; selectedItem != null; selectedItem = selectedItem.Parent)
                        {
                            if (selectedItem == item)
                            {
                                owner.SetSelectedItem(null);
                                break;
                            }
                        }
                    }
                }
                item.SetParent(null);
            }
            this.InnerList.RemoveAt(index);
        }

        public void Clear()
        {
            if (this.Count != 0)
            {
                if (this.m_owner != null)
                {
                    OrgDiagramServerControl owner = this.m_owner.Owner;
                    if (owner != null)
                    {
                        if (owner.CheckedItems.Count != 0)
                        {
                            owner.CheckedItems.Clear();
                        }
                        for (Item node = owner.SelectedItem; node != null; node = node.Parent)
                        {
                            if (this.Contains(node))
                            {
                                owner.SetSelectedItem(null);
                                break;
                            }
                        }
                    }
                }
                foreach (Item childItem in this.InnerList)
                {
                    childItem.SetParent(null);
                }
                this.InnerList.Clear();
            }
        }

        private static void UnCheckUnSelectRecursive(Item item)
        {
            Items checkedNodes = item.Owner.CheckedItems;
            if (item.Checked)
            {
                checkedNodes.Remove(item);
            }
            Item selectedNode = item.Owner.SelectedItem;
            if (item == selectedNode)
            {
                item.Owner.SetSelectedItem(null);
                selectedNode = null;
            }
            if ((selectedNode != null) || (checkedNodes.Count != 0))
            {
                foreach (Item childItem in item.Items)
                {
                    UnCheckUnSelectRecursive(childItem);
                }
            }
        }

        /// <summary>
        /// Check item in array
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Returns true if array contains item</returns>
        public bool Contains(Item item)
        {
            return this.InnerList.Contains(item);
        }

        /// <summary>
        /// Returns items as array
        /// </summary>
        /// <returns>Array of items</returns>
        public Item[] GetValues()
        {
            Item[] item = new Item[this.InnerList.Count];
            this.InnerList.CopyTo(0, item, 0, this.InnerList.Count);
            return item;
        }

        #endregion Public Methods

        public bool IsTrackingViewState
        {
            get { throw new System.NotImplementedException(); }
        }

        public void LoadViewState(object state)
        {
            throw new System.NotImplementedException();
        }

        public object SaveViewState()
        {
            throw new System.NotImplementedException();
        }

        public void TrackViewState()
        {
            throw new System.NotImplementedException();
        }
    }
}
