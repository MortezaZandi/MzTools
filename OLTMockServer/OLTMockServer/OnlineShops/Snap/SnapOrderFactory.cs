﻿using OLTMockServer.DataStructures;
using OLTMockServer.DataStructures.Snap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls;
using static Telerik.WinControls.UI.ValueMapper;

namespace OLTMockServer
{
    public class SnapOrderFactory : OrderFactory
    {
        public SnapOrderFactory() : base()
        {
        }

        public override Order CreateNewOrder(OrderPattern pattern)
        {
            var order = new SnapOrder();
            var props = typeof(SnapOrder).GetProperties();

            foreach (OrderPatternItem item in pattern.PatternItems)
            {
                var orderProp = props.FirstOrDefault(p => p.Name == item.PropertyName && p.PropertyType.Name == item.PropertyType);

                if (orderProp == null)
                {
                    throw new ApplicationException($"SnapOrder does not conatin property '{item.PropertyName}' with type of {item.PropertyType}");
                }

                object value = null;

                for (int i = 0; i < 50; i++)
                {
                    bool isNew = true;

                    value = CreatePropertyValue(item, pattern, orderProp, out isNew);

                    if (!item.Unique)
                    {
                        break;
                    }
                    else if (isNew)
                    {
                        break;
                    }
                    else if (i == 49)
                    {
                        throw new ApplicationException($"{this.GetType().Name} unable to create a unique value for property '{item.PropertyType}', All the 50 created values was used before.");
                    }
                }

                try
                {
                    var typeHandler = TypeDescriptor.GetConverter(orderProp.PropertyType);

                    if (typeHandler.CanConvertFrom(value.GetType()))
                    {
                        orderProp.SetValue(order, typeHandler.ConvertFrom(value));
                    }
                    else
                    {
                        if (typeHandler.CanConvertFrom(typeof(string)) && value != null)
                        {
                            value = typeHandler.ConvertFrom(value.ToString());

                            orderProp.SetValue(order, value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error when {this.GetType().Name} trying to set property value of the {order.GetType().Name}. PropName: {orderProp.Name}, PropType:{orderProp.PropertyType.Name}, Value:{value}, ValueNullCheck:{value == null}", ex);
                }
            }

            return order;
        }
    }
}