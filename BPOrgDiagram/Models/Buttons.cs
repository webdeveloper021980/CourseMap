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
    public class Buttons : CollectionBase
    {
            #region Constructor

            public Buttons()
            {

            }

            #endregion // Constructor

            #region Public Properties

            /// <summary>
            /// Get item by index.
            /// </summary>
            /// <param name="index">Index</param>
            /// <returns>Button</returns>
            public Button this[int index]
            {
                get
                {
                    return (Button)this.InnerList[index];
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
            /// <param name="item">Button</param>
            /// <returns>Added item</returns>
            public void Add(Button item)
            {
                this.AddAt(this.Count, item);
            }

            /// <summary>
            /// Add single item
            /// </summary>
            /// <param name="item">Button</param>
            /// <returns>Added item</returns>
            public void AddAt(int index, Button item)
            {
                this.InnerList.Insert(index, item);
            }

            /// <summary>
            /// Add array of items
            /// </summary>
            /// <param name="items">Buttons array</param>
            public void AddRange(Button[] items)
            {
                for (int index = 0; index < items.Length; index++)
                {
                    this.Add(items[index]);
                }
            }

            /// <summary>
            /// Remove item
            /// </summary>
            /// <param name="item">Button</param>
            public void Remove(Button item)
            {
                if (item == null)
                {
                    throw new ArgumentNullException("button");
                }
                int index = this.InnerList.IndexOf(item);
                if (index != -1)
                {
                    this.RemoveAt(index);
                }
            }

            public new void RemoveAt(int index)
            {
                this.InnerList.RemoveAt(index);
            }

            public void Clear()
            {
                this.InnerList.Clear();
            }

            /// <summary>
            /// Check item in array
            /// </summary>
            /// <param name="item">Button</param>
            /// <returns>Returns true if array contains item</returns>
            public bool Contains(Button item)
            {
                return this.InnerList.Contains(item);
            }

            /// <summary>
            /// Returns items as array
            /// </summary>
            /// <returns>Array of items</returns>
            public Button[] GetValues()
            {
                Button[] item = new Button[this.InnerList.Count];
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
