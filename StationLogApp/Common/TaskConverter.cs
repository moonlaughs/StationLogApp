using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StationLogApp.Interfaces;

namespace StationLogApp.Common
{
   public class TaskConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value, typeof(ObservableCollection<Task>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, typeof(ObservableCollection<Task>));
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(ObservableCollection<ITaskFactory>));
        }
    }
}
