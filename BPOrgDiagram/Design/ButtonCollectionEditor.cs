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
using System.ComponentModel.Design;

namespace BasicPrimitives.OrgDiagram.Design
{
    public class ButtonCollectionEditor : CollectionEditor
    {
        #region Private Fields

        private CollectionForm m_collectionForm;

        #endregion // Private Fields

        #region Contructor

        public ButtonCollectionEditor(Type type) 
            : base(type) 
        { 
        
        }

        #endregion // Contructor

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            if (this.m_collectionForm != null)
            {
                ButtonCollectionEditor itemCollectionEditor = new ButtonCollectionEditor(this.CollectionType);
                return itemCollectionEditor.EditValue(context, provider, value);
            }

            return base.EditValue(context, provider, value);
        }


        protected override CollectionForm CreateCollectionForm()
        {
            this.m_collectionForm = base.CreateCollectionForm();
            return this.m_collectionForm;
        }


        protected override object CreateInstance(Type ItemType)
        {
            Button item = (Button)base.CreateInstance(ItemType);

            if (this.Context.Instance != null)
            {
                item.Name = "Name";
            }

            return item;

        }
    }
}
