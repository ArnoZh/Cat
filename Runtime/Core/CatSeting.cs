using Cat.Core.AwakeIoc;
using Cat.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEngine;
namespace Cat.Core
{
    [JsonObject(MemberSerialization.OptOut)]//全部
    //[JsonConverter(typeof(CatSetingCustomConvert))]//等啥时候CatSetingCustomConvert写好了在打开
    public class CatSeting
    {
        /// <summary>
        /// Cat开关2
        /// </summary>
        [DefaultValue(false)]
        public bool CatSwitch = false;
        /// <summary>
        /// switch of log system
        /// </summary>
        [DefaultValue(true)]
        public bool LogSwitch = true;
        /// <summary>
        /// switch of unity debug
        /// </summary>
        [DefaultValue(true)]
        public bool UnityDebugSwitch = true;
        /// <summary>
        /// switch of filelog
        /// </summary>
        [DefaultValue(false)]
        public bool FileLogSwitch = false;
        /// <summary>
        /// filelog's basepath
        /// </summary>
        [DefaultValue("")]
        public string DefaultFileLogPath = "";
        /// <summary>
        /// Whether to create a LogPanel(with Canvas and EventSystem when there is no eventsystem in the activity scene)
        /// </summary>
        [DefaultValue(false)]
        public bool LogPanelUISwitch = false;
        ///// <summary>
        ///// The path of Sqlite relative to 'Application.dataPath' unity API
        ///// </summary>
        public string[] SqlitePaths;
        /// <summary>
        /// IAwakeWorkfows form seting.json
        /// </summary>
        public List<IAwakeWorkflow> AwakeWorkflows = new List<IAwakeWorkflow>() { };
        [JsonIgnore]
        public static readonly Newtonsoft.Json.JsonSerializerSettings DefaultJsonsetting = new Newtonsoft.Json.JsonSerializerSettings() 
        {
            //他自己那个带T的日期格式，我是真的用不来，格式化一下
            DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            //小驼峰命名
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //换下行
            Formatting = Formatting.Indented,
            //空值忽略掉，不管，反正拿了也是空
            NullValueHandling = NullValueHandling.Ignore,
            //type
            TypeNameHandling = TypeNameHandling.Auto,
            //All from json,give up the Static
            ObjectCreationHandling = ObjectCreationHandling.Replace,
        };
        /// <summary>
        /// Singleton
        /// </summary>
        [JsonIgnore]
        public static CatSeting Instance
        {
            get
            {
                if (instance == null)
                {
                    Init();
                }
                return instance;
            }
        }
        /// <summary>
        /// 自定义Json.net的Conver的时，给个Model
        /// </summary>
        public static CatSeting Copy
        {
            get
            {
                return new CatSeting();
            }
        }
        /// <summary>
        /// initialize
        /// </summary>
        private static void Init()
        {
            try
            {
                #region 测试时序列化出来看一下
                //instance = new CatSeting();
                //instance.AwakeWorkflows.Add(new LoadAssetFromSqlite());
                //string s = JsonConvert.SerializeObject(instance, DefaultJsonsetting);
                //Debug.Log("序列化结果" + s);
                //instance = JsonConvert.DeserializeObject<CatSeting>(s, DefaultJsonsetting);
                //Debug.Log("反序列化结果" + instance.Switch);
                //instance = null;
                #endregion

                string _filepath = Application.streamingAssetsPath + "/setings.json";
                if (!File.Exists(_filepath))
                {
                    throw new DirectoryNotFoundException("The Cat configuration file named 'seting.JSON' cannot be located in the path =>"+ _filepath);
                }
                //从配置文件中取值
                instance = JsonConvert.DeserializeObject<CatSeting>
                    (
                        File.ReadAllText(_filepath),
                        DefaultJsonsetting
                    );
            }
            catch (Exception e)
            {
                //Cat配置出错，闪退
                Debug.LogError("Cat is crash! " + e.ToString());
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
        }
        private static CatSeting instance;
        private CatSeting() { }
    }
}