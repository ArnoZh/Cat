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
    /// ShelfLoc货位配置信息
    /// </summary>
    public class ScadaShelfLocAwakeWorkflow : IAwakeWorkflow
    {
        public event OnFinishedHandle OnFinish;
#pragma warning disable 0067
        public event ProgressHandle Progress;
#pragma warning restore 0642

        public IEnumerator Execute()
        {
            try
            {
                /*
                 * 将货位信息压进配置器
                 */
                string path = Application.streamingAssetsPath + "/Scada/ShelfLoc.xml";
                List<ShelfLoc> locs = new XMLAdpter<ShelfLoc>(path).Resolve();
                Configuration.RegisterShelfLoc(locs);

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
