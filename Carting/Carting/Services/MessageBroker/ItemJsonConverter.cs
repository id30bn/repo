using Carting.Core.Models;
using Carting.Core.Models.Cart;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Carting.Services.MessageBroker
{
    public class ItemJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject jo = JObject.Load(reader);
                var item = jo.ToObject<Item>();
                if (jo["imageUrl"] != null)
                {
                    item.Image = new Image { Url = jo["imageUrl"].Value<string>(), Alt = "product" };
                }

                if (jo["description"] != null)
                {
                    item.FullDescription = jo["description"].Value<string>();
                }

                if (jo["amount"] != null)
                {
                    item.Quantity = jo["amount"].Value<int>();
                }

                return item;
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Item);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
