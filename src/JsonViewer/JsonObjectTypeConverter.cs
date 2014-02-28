﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Opensource.Json.Viewer
{
    public class JsonObjectTypeConverter: ExpandableObjectConverter
    {
        public JsonObjectTypeConverter()
        {

        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
