using System;
using System.Xml;

namespace GarminLaps.Extensions
{
    public static class OptionalXmlValues
    {
        public static Nullable<T> GetOptionalValue<T>(this XmlNode node) where T : struct, IComparable
        {
            if (node == null)
            {
                return null;
            }
            else
            {
                var value = node.InnerXml;
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}