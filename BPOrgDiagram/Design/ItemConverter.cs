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
using System.Globalization;
using System.ComponentModel.Design.Serialization;


namespace BasicPrimitives.OrgDiagram.Design
{
    internal class ItemConverter : ExpandableObjectConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
        {
            if (t == typeof(string)) 
            { 
                return true; 
            }
            return base.CanConvertFrom(context, t);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            if (destType == typeof(InstanceDescriptor) /*|| destType == typeof(string)*/)
            {
                return true;
            }
            return base.CanConvertTo(context, destType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
        {
            return base.ConvertFrom(context, info, value);
        }


        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo info, object value, Type destType)
        {
            if (destType == typeof(InstanceDescriptor))
            {
                return new InstanceDescriptor(
                    typeof(Item).GetConstructor(new Type[] { typeof(string), typeof(string), typeof(Item[]) }), 
                    new object[] { ((Item)value).Title, ((Item)value).Value, ((Item)value).Items.GetValues() }, 
                    true
                    );

            }
            return base.ConvertTo(context, info, value, destType);
        }
    }
}
