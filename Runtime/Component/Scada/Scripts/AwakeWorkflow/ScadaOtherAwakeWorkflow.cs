using Cat.Core;
using Cat.Core.AwakeIoc;
using Cat.Logger;
using Cat.Scada.DataModel;
using Cat.XMLExtend;
using com.clwl.data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Cat.Scada
{
    /// <summary>
    /// 其他的配置项
    /// </summary>
    public class ScadaOtherAwakeWorkflow : IAwakeWorkflow
    {
        public event OnFinishedHandle OnFinish;
#pragma warning disable 0067
        public event ProgressHandle Progress;
#pragma warning restore 0642

        public IEnumerator Execute()
        {
            try
            {
                
                string path = Application.streamingAssetsPath + "/Scada/Other.xml";
                /*
                 * 将IP配置信息压进配置器
                 */
                List<IP> ips = new XMLAdpter<IP>(path, "OtherConfigures/Ip").Resolve();
                Configuration.RegisterIps(ips);
                /*
                 * 将预制体配置信息压进配置器
                 */
                List<PrefabPath> prefabs = new XMLAdpter<PrefabPath>(path, "OtherConfigures/PrefabPaths").Resolve();
                Configuration.RegisterPrefabsPath(prefabs);
                
                //Debug.Log(nameof(ScadaShelfLocAwakeWorkflow) +"-->货位信息初始化成功，货位数量 : " + locs.Count);

                OnFinish?.Invoke(this);
            }
            catch (System.Exception e)
            {
                Debug.Log("Scada配置项出错" + e.ToString());

                //这种属于基础信息，如果这出错，程序是不应该继续运行的
                throw;
            }
            yield break;
        }
    }
}
