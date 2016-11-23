using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ScrapR.Models
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this byte[] arr)
        {
            T obj = (T)System.Activator.CreateInstance(typeof(T));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bin = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            ms.Write(arr, 0, arr.Length);
            ms.Position = 0;
            obj = (T)bin.Deserialize(ms);
            return obj;
        }

        public static T ToObject<T>(this XElement xml)
        {
            T obj = System.Activator.CreateInstance<T>();
            var serializer = new XmlSerializer(typeof(T));
            obj = (T)serializer.Deserialize(xml.CreateReader());
            return obj;
        }

        public static string ToJson(this object obj, bool format = false)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, format ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
        }
    }
}
