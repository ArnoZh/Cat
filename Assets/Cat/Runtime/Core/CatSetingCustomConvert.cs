using Cat.Core;
using Cat.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


namespace Cat.closebeta
{
    public class Test_CatSetingCustomConvert
    {
        void Test()
        {
            mydata instance = new mydata();
            instance.id = 3;
            string writestring = JsonConvert.SerializeObject(instance);
            Debug.Log("序列化结果" + writestring);

            using (FileStream stream = new FileStream(Application.dataPath + "/text.json", FileMode.Create))
            {
                byte[] bytes = null;
                bytes = Encoding.UTF8.GetBytes(writestring);
                stream.Write(bytes, 0, bytes.Length);
                //刷新
                stream.Flush();
            }
            using (StreamReader sr = new StreamReader(Application.dataPath + "/text.json", Encoding.Default))
            {
                string readstring = sr.ReadToEnd();
                instance = JsonConvert.DeserializeObject<mydata>(readstring);
                Debug.Log("反序列化结果" + instance.id);
                instance = null;
            }
        }
    }

    [JsonObject(MemberSerialization.OptOut)]//全部
    [JsonConverter(typeof(CatSetingCustomConvert))]
    public class mydata
    {
        public int id { get; set; }
    }


    class CatSetingCustomConvert : JsonConverter
    {
        //………………写按我这个写，读不按我这个读？？这种不是找虐么？感觉这两开关，整成一个就行了，除非序列化的时候，需要改值，Arno,5.24.2020
        //序列化，true:走CatSetingCustomConvert定义方法，fales走Json.net自己的,
        public override bool CanWrite => true;
        //反序列化，true:走CatSetingCustomConvert定义方法，fales走Json.net自己的
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            //不加策略了，只给CatSeting定义一种特定的解析方式，项目里其他地方还是会用默认的
            return typeof(mydata) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Debug.Log("开始读");
            //Debug.Log(reader.Value);
            var model = new mydata();
            //获取JObject对象，该对象对应着我们要反序列化的json
            var jobj = serializer.Deserialize<JObject>(reader);
            //从JObject对象中获取键位ID的值
            var id = jobj.Value<int>("id");
            Debug.Log(id);
            //根据id值判断，进行赋值操作
            id++;
            model.id = id;
            Debug.Log("读取后" + model.id);
            return model;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //new一个JObject对象,JObject可以像操作对象来操作json
            //CatSeting
            var model = value as mydata;

            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append("\"id\":" + model.id);
            sb.Append("}");
            //jobj.Add("id",model.id);
            //var jsonstr = jobj.ToString(Formatting.None);//Jobject的tostring就是Json文本,源码里面写了indented,不用再格式化了
            //写回流
            writer.WriteValue(sb.ToString());
        }
    }
}