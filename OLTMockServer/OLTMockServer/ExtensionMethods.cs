using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    public static class SerializableExtensionMethods
    {
        public static object Clone(this object serializableObject)
        {
            if (serializableObject == null)
            {
                throw new ArgumentNullException(nameof(serializableObject));
            }

            return Deserialize(Serialize(serializableObject));
        }

        public static byte[] Serialize(this object obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public static object Deserialize(this byte[] objData)
        {
            using (MemoryStream memoryStream = new MemoryStream(objData))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream.Position = 0;
                return binaryFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// Remove millieseconds from TimeSpan object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TimeSpan Round(this TimeSpan value)
        {
            return TimeSpan.FromSeconds((long)value.TotalSeconds);
        }
    }
}
