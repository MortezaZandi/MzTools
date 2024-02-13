using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MZCryptoTools
{
    public class XmlDataSerializer
    {

        public static string SerializeToXMLString<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, toSerialize);
                stream.Position = 0;
                writer.Close();
                return Encoding.UTF8.GetString(stream.GetBuffer());
            }
        }

        public static T DeserializeFromXMLString<T>(string value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (Stream reader = (new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""))))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        //public static string Serialize<T>(T data_to_serialize)
        //{
        //    using (Stream stream = new MemoryStream())
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(T));
        //        XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
        //        writer.Formatting = Formatting.Indented;
        //        serializer.Serialize(writer, data_to_serialize);
        //        writer.Close();

        //        TextReader textReader = new StreamReader(stream);

        //        return textReader.ReadToEnd();
        //    }
        //}
        //public static T Deserialize<T>(string data)
        //{
        //    try
        //    {
        //        using (TextReader textReader = new StreamReader(data))
        //        {
        //            XmlSerializer serializer = new XmlSerializer(typeof(T));

        //            return (T)serializer.Deserialize(textReader);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return default(T);
        //    }
        //}
    }
}
